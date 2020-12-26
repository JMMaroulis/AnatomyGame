using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public bool isTimePassing;
    public float timeScale;
    public bool isFunctioning;
    public bool requiresReplacing;
    public bool requiresAmputation;

    //blood stuff
    public float bloodRequiredToFunction;
    public float blood;
    public float bloodLossRate;
    public float bloodPumpRate;
    public float bloodMax;

    //oxygen stuff
    public float oxygen;
    public float oxygenMax;
    public float oxygenRequired;
    public float oxygenDamageRate;

    public float damage;
    public float damageMax;
    public float efficiency;

    //connections
    //NB: connectedHearts and connectedOrgans are NOT MUTUALLY EXCLUSIVE LISTS
    public List<BodyPart> connectedBodyParts;
    public List<Organ> containedOrgans;
    public List<Heart> connectedHearts;
    public List<EmbeddedObject> embeddedObjects;

    private float connectedBrainEfficiency;

    public bool isPartOfMainBody;
    public bool isConnectedToBrain;

    //expected limb numbers
    public int maxLeftArms;
    public int maxRightArms;
    public int maxLeftLegs;
    public int maxRightLegs;
    public int maxHeads;
    public int maxTorsos;

    //expected organ numbers
    public int maxBrains;
    public int maxLeftEyes;
    public int maxRightEyes;
    public int maxHearts;
    public int maxLivers;
    public int maxLeftLungs;
    public int maxRightLungs;
    public int maxStomachs;

    //drug stuff
    public float healthPotion;
    public float antidote;
    public float slowPoison;
    public float stasisPotion;
    public float coagulantPotion;
    public float hastePotion;

    public float healthPotionProcessRate;
    public float antidoteProcessRate;
    public float slowPoisonProcessRate;
    public float coagulantPotionProcessRate;
    public float stasisPotionProcessRate;
    public float hastePotionProcessRate;

    public float slowPoisonDamageRate;

    public BodyPartManager bodyPartManager;

    public void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        UpdateHeartConnections();

    }

    public virtual void UpdateBodyPart(float deltaTime)
    {
        UpdateSelf(deltaTime);
    }

    public void UpdateSelf(float deltaTime)
    {
        if (isTimePassing)
        {
            deltaTime *= timeScale;

            ApplyDrugs(deltaTime);
            UpdateDamage(deltaTime);
            CheckForFunctionality();
            UpdateEfficiency();
            LoseBloodTime(deltaTime);
            ConsumeOxygen(deltaTime);
        }
    }

    public void UpdateHeartConnections()
    {
        //check if connected to heart
        List<Heart> hearts = FindObjectsOfType<Heart>().ToList();
        foreach (Heart heart in hearts)
        {
            if (IsConnectedToBodyPartStarter(heart))
            {
                connectedHearts.Add(heart);
            }
            else
            {
                connectedHearts.Remove(heart);
            }
        }
    }

    //Pumps blood, if there is blood left to pump.
    public void PumpBlood(float heartEfficiency, float deltaTime)
    {
        deltaTime *= timeScale;

        //pumping blood to contained organs in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<BodyPart> allBodyParts = connectedBodyParts.Concat(containedOrgans).ToList();
        IListExtensions.Shuffle(allBodyParts);

        foreach (BodyPart bodyPart in allBodyParts)
        {
            //cap blood and oxygen transfer rate at between 0.2 and 5 times the blood pump rate, plus slowdown factor for more localised substances
            float tempBloodPumpRate =           Mathf.Max(Mathf.Min( (blood / bodyPart.blood),                     5), 0.2f) * bloodPumpRate * heartEfficiency;
            float tempOxygenPumpRate =          Mathf.Max(Mathf.Min( (oxygen / bodyPart.oxygen),                   5), 0.2f) * bloodPumpRate * heartEfficiency;
            float tempHealthPotionPumpRate =    Mathf.Max(Mathf.Min( (healthPotion / bodyPart.healthPotion),       5), 0.2f) * bloodPumpRate * heartEfficiency * 0.001f;
            float tempAntidotePumpRate =        Mathf.Max(Mathf.Min( (antidote / bodyPart.antidote),               5), 0.2f) * bloodPumpRate * heartEfficiency * 0.001f;
            float tempSlowPoisonPumpRate =      Mathf.Max(Mathf.Min( (slowPoison / bodyPart.slowPoison),           5), 0.2f) * bloodPumpRate * heartEfficiency * 0.0001f;
            float tempStasisPotionPumpRate =    Mathf.Max(Mathf.Min( (stasisPotion / bodyPart.stasisPotion),       5), 0.2f) * bloodPumpRate * heartEfficiency * 0.001f;
            float tempCoagulantPotionPumpRate = Mathf.Max(Mathf.Min( (coagulantPotion / bodyPart.coagulantPotion), 5), 0.2f) * bloodPumpRate * heartEfficiency * 0.001f;
            float tempHastePotionPumpRate =     Mathf.Max(Mathf.Min( (hastePotion / bodyPart.hastePotion),         5), 0.2f) * bloodPumpRate * heartEfficiency * 0.001f;

            //transport blood
            float proposedBloodOut = tempBloodPumpRate * deltaTime;
            float bloodOut = Mathf.Min( Mathf.Min(blood, proposedBloodOut), (bodyPart.bloodMax - bodyPart.blood));
            bodyPart.blood += bloodOut;
            blood -= bloodOut;

            //transport oxygen
            float proposedOxygenOut = tempOxygenPumpRate * deltaTime;
            float oxygenOut = Mathf.Max(Mathf.Min(oxygen, proposedOxygenOut), 0);
            bodyPart.oxygen += oxygenOut;
            oxygen -= oxygenOut;

            if (healthPotion > 0.0f)
            {
                //transport health potion
                float proposedHealthPotionOut = tempHealthPotionPumpRate * deltaTime;
                float healthPotionOut = Mathf.Min(healthPotion, proposedHealthPotionOut);
                bodyPart.healthPotion += healthPotionOut;
                healthPotion -= healthPotionOut;
            }

            if (antidote > 0.0f)
            {
                //transport antidote
                float proposedAntidoteOut = tempAntidotePumpRate * deltaTime;
                float antidoteOut = Mathf.Min(antidote, proposedAntidoteOut);
                bodyPart.antidote += antidoteOut;
                antidote -= antidoteOut;
            }

            if (slowPoison > 0.0f)
            {
                //transport slow poison
                float proposedSlowPoisonOut = tempSlowPoisonPumpRate * deltaTime;
                float slowPoisonOut = Mathf.Min(slowPoison, proposedSlowPoisonOut);
                bodyPart.slowPoison += slowPoisonOut;
                slowPoison -= slowPoisonOut;
            }

            if (stasisPotion > 0.0f)
            {
                //transport stasis potion
                float proposedStasisPotionOut = tempStasisPotionPumpRate * deltaTime;
                float stasisPotionOut = Mathf.Min(stasisPotion, proposedStasisPotionOut);
                bodyPart.stasisPotion += stasisPotionOut;
                stasisPotion -= stasisPotionOut;
            }

            if (coagulantPotion > 0.0f)
            {
                //transport stasis potion
                float proposedCoagulantPotionOut = tempCoagulantPotionPumpRate * deltaTime;
                float coagulantPotionOut = Mathf.Min(coagulantPotion, proposedCoagulantPotionOut);
                bodyPart.coagulantPotion += coagulantPotionOut;
                coagulantPotion -= coagulantPotionOut;
            }

            if (hastePotion > 0.0f)
            {
                //transport stasis potion
                float proposedHastePotionOut = tempHastePotionPumpRate * deltaTime;
                float hastePotionOut = Mathf.Min(hastePotion, proposedHastePotionOut);
                bodyPart.hastePotion += hastePotionOut;
                hastePotion -= hastePotionOut;
            }

        }
    }

    //applies blood loss rate
    public void LoseBloodTime(float deltaTime)
    {
        float bloodLost = bloodLossRate * deltaTime;
        LoseBloodAmount(bloodLost);
    }

    public void LoseBloodAmount(float bloodLost, int stepcount = 0)
    {
        float bloodLostRemainder = bloodLost - blood;
        float bloodLossProportion = Mathf.Min(bloodLost / (blood), 1);

        blood = Mathf.Max(blood - bloodLost, 0);
        oxygen *= 1 - bloodLossProportion;
        healthPotion *= 1 - bloodLossProportion;
        antidote *= 1 - bloodLossProportion;
        slowPoison *= 1 - bloodLossProportion;
        stasisPotion *= 1 - bloodLossProportion;
        hastePotion *= 1 - bloodLossProportion;
        coagulantPotion *= 1 - bloodLossProportion;

        //if bodypart doesn't have enough blood to lose, inflict the remaining loss on neighbouring parts instead
        if (bloodLostRemainder > 0 && connectedBodyParts.Count > 0 && stepcount < 3)
        {
            connectedBodyParts[UnityEngine.Random.Range(0, connectedBodyParts.Count)].LoseBloodAmount(bloodLostRemainder, stepcount + 1);
        }

        //then start draining organs
        if (bloodLostRemainder > 0 && containedOrgans.Count > 0 && stepcount < 3)
        {
            containedOrgans[UnityEngine.Random.Range(0, containedOrgans.Count)].LoseBloodAmount(bloodLostRemainder, stepcount + 1);
        }
    }

    public void ConsumeOxygen(float deltaTime)
    {
        float oxygenconsumed = oxygenRequired * deltaTime;
        oxygen = Mathf.Max(oxygen - oxygenconsumed, 0);
    }

    //TODO: currently using a units processed per second model
    //doesn't allow for emergent properties of overdosing to happen, just makes higher doses last longer
    //consider scaling effects of drugs with amount of drug present
    public void ApplyDrugs(float deltaTime)
    {
        //stasis potion
        //TODO: needs to stick around long enough for an organ to be removed/replaced, figure out the numbers
        // currently setting the timeScale = -(1/50) * potion + 1
        //IMPORTANT NOTE: the decay rate of the stasis potion is independant of its own effect on the timescale,
        //otherwise we end up in weird reverse temporal singularities from which there is no escape. Which is cool and all,
        //but there being no way out of them is no bueno
        if (stasisPotion > 0.0f)
        {
            float stasisPotionProcessed = Mathf.Min(stasisPotion, deltaTime * stasisPotionProcessRate);
            stasisPotion = Mathf.Max(0.0f, stasisPotion - stasisPotionProcessed);
        }
        if (hastePotion > 0.0f)
        {
            float hastePotionProcessed = Mathf.Min(hastePotion, deltaTime * hastePotionProcessRate);
            hastePotion = Mathf.Max(0.0f, hastePotion - hastePotionProcessed);
        }

        timeScale = (-(1.0f / 60.0f) * stasisPotion) + ((1.0f / 60.0f) * hastePotion) + 1.0f;


        //health potion
        if (healthPotion > 0.0f)
        {
            if (damage > 0.0f)
            {
                //in 1 second, will process 1/5 unit of health potion, curing 1/5 units of damage
                float healthPotionProcessed = Mathf.Min(healthPotion, deltaTime * healthPotionProcessRate);
                healthPotion = Mathf.Max(0.0f, healthPotion - healthPotionProcessed);
                damage = Mathf.Max(0.0f, damage - healthPotionProcessed);
            }
            else
            {
                //decays at 1/100th unit per second, if no damage to be healed
                healthPotion = Mathf.Max(0.0f, healthPotion - (deltaTime * 0.01f));
            }
        }

        //antidote
        //neutralises poison at 1 unit per second
        //decays at 1 unit per second regardless of poison
        if (antidote > 0.0f)
        {
            if (slowPoison > 0.0f)
            {
                //in 1 second, will neutralise 2 units of poison
                float antidoteProcessed = Mathf.Min(antidote, deltaTime * antidoteProcessRate);
                antidote = Mathf.Max(0.0f, antidote - antidoteProcessed);
                slowPoison = Mathf.Max(0.0f, slowPoison - 2.0f * antidoteProcessed);
            }
            //decays at 1/100th unit per second, if no poison to neutralise
            antidote = Mathf.Max(0.0f, antidote - (deltaTime * 0.001f));
        }

        //slow poison
        //0.001% of poison amount as damage per second
        //processes at 0.001 units per seconds
        if (slowPoison > 0.0f)
        {
            float slowPoisonProcessed = Mathf.Min(slowPoison, deltaTime * slowPoisonProcessRate);
            slowPoison = Mathf.Max(0.0f, slowPoison - slowPoisonProcessed);
            damage = Mathf.Max(0.0f, damage + slowPoison * deltaTime * slowPoisonDamageRate);
        }

        //coagulant potion
        if (coagulantPotion > 0.0f)
        {
            if (bloodLossRate > 0.0f)
            {
                float coagulantPotionProcessed = Mathf.Min(coagulantPotion, deltaTime * coagulantPotionProcessRate);
                bloodLossRate = Mathf.Max(0, bloodLossRate - coagulantPotionProcessed);
                coagulantPotion = Mathf.Max(0.0f, coagulantPotion - coagulantPotionProcessed);
            }
            else
            {
                //decays at 1/100th unit per second, if no bloodlossrate to be healed
                coagulantPotion = Mathf.Max(0.0f, coagulantPotion - (deltaTime * 0.01f));
            }
        }




    }

    public void CheckForFunctionality()
    {
        isFunctioning = (blood >= bloodRequiredToFunction) && (oxygen >= oxygenRequired);
    }

    //kicks off recursive process for finding brain, and efficiency thereof
    public void IsConnectedToBrainStarter()
    {
        isConnectedToBrain = false;
        connectedBrainEfficiency = 0.0f;
        IsConnectedToBrain(this, new List<BodyPart>());
    }

    //recursive iteration
    private void IsConnectedToBrain(BodyPart currentBodyPart, List<BodyPart> alreadyChecked)
    {

        if (currentBodyPart is Brain && currentBodyPart.enabled)
        {
            isConnectedToBrain = true;
            connectedBrainEfficiency = currentBodyPart.efficiency;
        }
        alreadyChecked.Add(currentBodyPart);

        List<BodyPart> allBodyParts = currentBodyPart.connectedBodyParts.Concat(currentBodyPart.containedOrgans).ToList();

        foreach (BodyPart bodyPart in allBodyParts)
        {
            if (alreadyChecked.Contains(bodyPart) == false)
            {
                IsConnectedToBrain(bodyPart, alreadyChecked);
            }
        }

    }

    //kicks off recursive process for finding target bodypart
    public bool IsConnectedToBodyPartStarter(BodyPart targetBodypart)
    {
        return IsConnectedToBodyPart(this, targetBodypart, new List<BodyPart>());
    }

    //recursive iteration
    private bool IsConnectedToBodyPart(BodyPart currentBodyPart, BodyPart targetBodyPart, List<BodyPart> alreadyChecked)
    {

        if (currentBodyPart == targetBodyPart)
        {
            return true;
        }
        alreadyChecked.Add(currentBodyPart);

        List<BodyPart> allBodyParts = currentBodyPart.connectedBodyParts.Concat(currentBodyPart.containedOrgans).ToList();


        foreach (BodyPart bodyPart in allBodyParts)
        {
            if (alreadyChecked.Contains(bodyPart) == false)
            {
                if (IsConnectedToBodyPart(bodyPart, targetBodyPart, alreadyChecked))
                {
                    return true;
                }
            }
        }

        return false;

    }

    public bool CheckConnectionValidity(BodyPart bodyPart)
    {
        //don't get me wrong, this is a horrifying piece of code, that's going to need expanding every time we add a bodypart/organ type
        //but for now, it works, and that'll have to do
        if (bodyPart is LeftArm)
        {
            return (connectedBodyParts.OfType<LeftArm>().Count() < maxLeftArms);
        }

        if (bodyPart is RightArm)
        {
            return (connectedBodyParts.OfType<RightArm>().Count() < maxRightArms);
        }

        if (bodyPart is Head)
        {
            return (connectedBodyParts.OfType<Head>().Count() < maxHeads);
        }

        if (bodyPart is LeftLeg)
        {
            return (connectedBodyParts.OfType<LeftLeg>().Count() < maxLeftLegs);
        }

        if (bodyPart is RightLeg)
        {
            return (connectedBodyParts.OfType<RightLeg>().Count() < maxRightLegs);
        }

        if (bodyPart is Torso)
        {
            return (connectedBodyParts.OfType<Torso>().Count() < maxTorsos);
        }

        return false;
    }

    public bool CheckImplantValidity(Organ organ)
    {
        if (organ is Brain)
        {
            return (containedOrgans.OfType<Brain>().Count() < maxBrains);
        }

        if (organ is LeftEye)
        {
            return (containedOrgans.OfType<LeftEye>().Count() < maxLeftEyes);
        }

        if (organ is RightEye)
        {
            return (containedOrgans.OfType<RightEye>().Count() < maxRightEyes);
        }

        if (organ is Heart)
        {
            return (containedOrgans.OfType<Heart>().Count() < maxHearts);
        }

        if (organ is Liver)
        {
            return (containedOrgans.OfType<Liver>().Count() < maxLivers);
        }

        if (organ is LeftLung)
        {
            return (containedOrgans.OfType<LeftLung>().Count() < maxLeftLungs);
        }

        if (organ is RightLung)
        {
            return (containedOrgans.OfType<RightLung>().Count() < maxRightLungs);
        }

        if (organ is Stomach)
        {
            return (containedOrgans.OfType<Stomach>().Count() < maxStomachs);
        }

        return false;
    }

    public bool CheckEmbeddedObjectValidity(EmbeddedObject embeddedObject)
    {
        return true;
    }

    public void UpdateEfficiency()
    {
        IsConnectedToBrainStarter();

        float damageRatio = 1 - (damage / damageMax); //1 good, 0 bad
        float oxygenRatio = Mathf.Min((oxygen / oxygenRequired), 1); //1 good, 0 bad
        if (this.gameObject.GetComponent<Brain>() != null)
        {
            efficiency = damageRatio * oxygenRatio;
        }
        else
        {
            efficiency = damageRatio * oxygenRatio * connectedBrainEfficiency;
        }

    }

    public void UpdateDamage(float deltaTime)
    {
        float oxygenRatio = 1 - Mathf.Min((oxygen / oxygenRequired), 1); //0 good, 1 bad
        float newDamage = oxygenRatio * deltaTime * oxygenDamageRate;
        damage = Mathf.Clamp(damage + newDamage, 0, damageMax);
    }

    public void SeverConnectionOutgoing(GameObject connectedBodyPart, float inducedBloodLossRate)
    {
        //severing connection to bodypart
        connectedBodyParts.Remove(connectedBodyPart.GetComponent<BodyPart>());

        //severing connection to organ
        containedOrgans.Remove(connectedBodyPart.GetComponent<Organ>());

        bloodLossRate += inducedBloodLossRate;
        connectedBodyPart.GetComponent<BodyPart>().bloodLossRate += inducedBloodLossRate;
    }

    //severs all connections between self and connecting bodyparts (BOTH WAYS)
    public void SeverAllConnections(float inducedBleedingPerSeverance = 0)
    {
        //sever *that* connection to *this*
        foreach (BodyPart connectedBodyPart in connectedBodyParts)
        {
            connectedBodyPart.SeverConnectionOutgoing(this.gameObject, inducedBleedingPerSeverance);
        }

        //sever *this* connection to *those*
        connectedBodyParts = new List<BodyPart>();
        isPartOfMainBody = false;
    }

    public void CreateConnection(BodyPart bodyPartToConnect)
    {
        connectedBodyParts.Add(bodyPartToConnect);
        if (isPartOfMainBody || bodyPartToConnect.isPartOfMainBody)
        {
            isPartOfMainBody = true;
            bodyPartToConnect.isPartOfMainBody = true;
        }
    }

    public void AddContainedOrgan(Organ organToImplant)
    {
        containedOrgans.Add(organToImplant);
    }

    public string GenerateDescription()
    {
        string description = "";

        description += $"Examining {this.gameObject.name}:\n";

        description += $"Damage: {Math.Round(damage, 2)} / {damageMax}\n";

        description += $"Blood: {Math.Round(blood, 2)} units, requires {bloodRequiredToFunction} to function.\n";

        if (bloodLossRate > 0.0f)
        {
            description += $"Losing {Math.Round(bloodLossRate, 2)} units of blood per second.\n";
        }

        description += $"Oxygen: {Math.Round(oxygen, 2)} / {oxygenMax}, requires {oxygenRequired} per second to function.\n";

        if (requiresReplacing)
        {
            description += $"Requires replacing.\n";
        }

        if (requiresAmputation)
        {
            description += $"Requires amputating.\n";
        }

        if (healthPotion > 0.0f)
        {
            description += $"Health Potion: {Math.Round(healthPotion, 2)} Units.\n";
        }

        if (antidote > 0.0f)
        {
            description += $"Antidote: {Math.Round(antidote, 2)} Units.\n";
        }

        if (slowPoison > 0.0f)
        {
            description += $"Slow Poison: {Math.Round(slowPoison, 2)} Units.\n";
        }

        if (stasisPotion > 0.0f)
        {
            description += $"Stasis Potion: {Math.Round(stasisPotion, 2)} Units.\n";
        }

        if (hastePotion > 0.0f)
        {
            description += $"Haste Potion: {Math.Round(hastePotion, 2)} Units.\n";
        }

        if (coagulantPotion > 0.0f)
        {
            description += $"Coagulant Potion: {Math.Round(coagulantPotion, 2)} Units.\n";
        }

        if (this.GetComponent<HeartCharm>() != null)
        {
            HeartCharm heartCharm = this.GetComponent<HeartCharm>();
            description += $"Heart Charm: {Math.Round(heartCharm.expiryTime - heartCharm.timeElapsed, 2)} seconds remaining.\n";
        }

        if (this.GetComponent<LungCharm>() != null)
        {
            LungCharm lungCharm = this.GetComponent<LungCharm>();
            description += $"Lung Charm: {Math.Round(lungCharm.expiryTime - lungCharm.timeElapsed, 2)} seconds remaining.\n";
        }

        if (this.GetComponent<PetrificationCharm>() != null)
        {
            PetrificationCharm petrificationCharm = this.GetComponent<PetrificationCharm>();
            description += $"Petrification Charm: {Math.Round(petrificationCharm.expiryTime - petrificationCharm.timeElapsed, 2)} seconds remaining.\n";
        }

        description += "Connected to: ";
        for (int i = 0; i < connectedBodyParts.Count; i++)
        {
            description += connectedBodyParts[i].transform.name;
            if (i != connectedBodyParts.Count - 1)
            { description += ", "; }
        }

        return description;


    }
}
