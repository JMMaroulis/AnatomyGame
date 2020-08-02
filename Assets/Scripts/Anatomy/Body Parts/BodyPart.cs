﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public bool isTimePassing;
    public float timeScale;
    public bool isFunctioning;
    private Clock clock;

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

    public float damage;
    public float damageMax;
    public float efficiency;

    //connections
    //NB: connectedHearts and connectedOrgans are NOT MUTUALLY EXCLUSIVE LISTS
    public List<BodyPart> connectedBodyParts;
    public List<Organ> containedOrgans;
    public List<Heart> connectedHearts;

    private bool isConnectedToBrain;
    private bool isConnectedToHeart;

    private float connectedBrainEfficiency;

    //expected limb numbers
    public int maxLeftArms;
    public int maxRightArms;
    public int maxLeftLegs;
    public int maxRightLegs;
    public int maxHeads;
    public int maxTorsos;

    public bool isPartOfMainBody;

    //expected organ numbers
    public int maxBrains;
    public int maxEyes;
    public int maxHearts;
    public int maxLivers;
    public int maxLungs;
    public int maxStomachs;

    //drug stuff
    public float healthPotion;
    public float antidote;
    public float slowPoison;
    public float stasisPotion;
    public float coagulantPotion;
    public float hastePotion;

    public void Start()
    {
        clock = FindObjectOfType<Clock>();
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
            ApplyDrugs(deltaTime);
            UpdateDamage(deltaTime);
            CheckForFunctionality();
            UpdateEfficiency();
            LoseBlood(deltaTime);
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
        List<BodyPart> allBodyParts = connectedBodyParts.Concat(containedOrgans).ToList<BodyPart>();
        List<int> bodypartPumpOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartPumpOrder);
        foreach (int bodyPartIndex in bodypartPumpOrder)
        {
            BodyPart bodyPart = allBodyParts[bodyPartIndex];
            //cap blood and oxygen transfer rate at between 0.2 and 5 times the blood pump rate
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
    public void LoseBlood(float deltaTime)
    {
        deltaTime *= timeScale;

        float timeSinceLastLoss = deltaTime;
        float bloodLost = bloodLossRate * timeSinceLastLoss;

        //if bodypart doesn't have enough blood to lose, inflict the remaining loss on neighbouring parts instead
        //otherwise, lose it from this bodypart
        if (bloodLost > blood && connectedBodyParts.Count > 0)
        {
            float bloodLostRemainder = bloodLost - blood;
            blood = Mathf.Max(blood - bloodLost, 0);
            connectedBodyParts[Random.Range(0, connectedBodyParts.Count)].LoseBloodChain(bloodLostRemainder, 0);
        }
        else
        {
            blood = Mathf.Max(blood - bloodLost, 0);
        }
    }

    //searches random attached bodyparts for the remaining blood to lose
    //TODO: this really is a terrible, terrible thing, but it works for now.
    //Probably won't hold up on bodies with more parts
    public void LoseBloodChain(float bloodLost, int stepCount)
    {
        stepCount += 1;
        if (stepCount > 3) { return; }

        //if bodypart doesn't have enough blood to lose, inflict the remaining loss on neighbouring parts instead
        //otherwise, lose it from this bodypart
        if (bloodLost > blood)
        {
            float bloodLostRemainder = bloodLost - blood;
            blood = Mathf.Max(blood - bloodLost, 0);
            connectedBodyParts[Random.Range(0, connectedBodyParts.Count)].LoseBloodChain(bloodLostRemainder, stepCount);
        }
        else
        {
            blood = Mathf.Max(blood - bloodLost, 0);
        }
    }

    public void ConsumeOxygen(float deltaTime)
    {
        deltaTime *= timeScale;
        float oxygenconsumed = oxygenRequired * deltaTime;
        oxygen = Mathf.Max(oxygen - oxygenconsumed, 0);
    }

    //TODO: currently using a units processed per second model
    //doesn't allow for emergent properties of overdosing to happen, just makes higher doses last longer
    //consider scaling effects of drugs with amount of drug present
    public void ApplyDrugs(float deltaTime)
    {

        deltaTime *= timeScale;

        //health potion
        if (healthPotion > 0.0f)
        {
            if (damage > 0.0f)
            {
                //in 1 second, will process 1/5 unit of health potion, curing 1/5 units of damage
                float healthPotionProcessed = Mathf.Min(healthPotion, deltaTime * 0.5f);
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
                //in 1 second, will neutralise 1 unit of poison
                float antidoteProcessed = Mathf.Min(antidote, deltaTime * 1.0f);
                antidote = Mathf.Max(0.0f, antidote - antidoteProcessed);
                slowPoison = Mathf.Max(0.0f, slowPoison - antidoteProcessed);
            }
            //decays at 1/100th unit per second, if no poison to neutralise
            slowPoison = Mathf.Max(0.0f, slowPoison - (deltaTime * 0.01f));
        }

        //slow poison
        //0.001% of poison amount as damage per second
        //processes at 0.001 units per seconds
        if (slowPoison > 0.0f)
        {
            float slowPoisonProcessed = Mathf.Min(slowPoison, deltaTime * 0.001f);
            //slowPoison = Mathf.Max(0.0f, slowPoison - slowPoisonProcessed);
            //damage = Mathf.Min(damageMax, damage + (slowPoisonProcessed*100));            
            damage = Mathf.Max(0.0f, damage + slowPoison * deltaTime * 0.001f);
        }

        //coagulant potion
        if (coagulantPotion > 0.0f)
        {
            if (bloodLossRate > 0.0f)
            {
                float coagulantPotionProcessed = Mathf.Min(coagulantPotion, (deltaTime / timeScale) * 0.5f);
                bloodLossRate = Mathf.Max(0, bloodLossRate - coagulantPotionProcessed);
                coagulantPotion = Mathf.Max(0.0f, coagulantPotion - coagulantPotionProcessed);
            }
            else
            {
                //decays at 1/100th unit per second, if no bloodlossrate to be healed
                coagulantPotion = Mathf.Max(0.0f, coagulantPotion - (deltaTime * 0.01f));
            }
        }

        //stasis potion
        //TODO: needs to stick around long enough for an organ to be removed/replaced, figure out the numbers
        // currently setting the timeScale = -(1/50) * potion + 1
        //IMPORTANT NOTE: the decay rate of the stasis potion is independant of its own effect on the timescale,
        //otherwise we end up in weird reverse temporal singularities from which there is no escape. Which is cool and all,
        //but there being no way out of them is no bueno
        if (stasisPotion > 0.0f)
        {
            float stasisPotionProcessed = Mathf.Min(stasisPotion, (deltaTime/timeScale) * 0.01f);
            stasisPotion = Mathf.Max(0.0f, stasisPotion - stasisPotionProcessed);
        }
        if (hastePotion > 0.0f)
        {
            float hastePotionProcessed = Mathf.Min(hastePotion, (deltaTime / timeScale) * 0.01f);
            hastePotion = Mathf.Max(0.0f, hastePotion - hastePotionProcessed);
        }
        timeScale = (-(1.0f / 60.0f) * stasisPotion) + ((1.0f / 60.0f) * hastePotion) +1.0f;



    }

    public void CheckForFunctionality()
    {
        if (blood < bloodRequiredToFunction)
        { isFunctioning = false; }
        if (oxygen <= 0)
        { isFunctioning = false; }

        else
        {
            isFunctioning = true;
        }

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
            return (connectedBodyParts.OfType<Brain>().Count() < maxBrains);
        }

        if (organ is Eye)
        {
            return (connectedBodyParts.OfType<Eye>().Count() < maxEyes);
        }

        if (organ is Heart)
        {
            return (connectedBodyParts.OfType<Heart>().Count() < maxHearts);
        }

        if (organ is Liver)
        {
            return (connectedBodyParts.OfType<Liver>().Count() < maxLivers);
        }

        if (organ is Lung)
        {
            return (connectedBodyParts.OfType<Lung>().Count() < maxLungs);
        }

        if (organ is Stomach)
        {
            return (connectedBodyParts.OfType<Stomach>().Count() < maxStomachs);
        }

        return false;
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
        deltaTime *= timeScale;

        float oxygenRatio = 1 - Mathf.Min((oxygen / oxygenRequired), 1); //0 good, 1 bad
        damage = Mathf.Min(damage + (oxygenRatio * deltaTime * 0.2f), damageMax);
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
    public void SeverAllConnections()
    {
        //sever *that* connection to *this*
        foreach (BodyPart connectedBodyPart in connectedBodyParts)
        {
            connectedBodyPart.SeverConnectionOutgoing(this.gameObject, 20);
        }

        //sever *this* connection to *those*
        connectedBodyParts = new List<BodyPart>();
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

        #region damage description
        //add damage description
        description += $"Damage: {damage} / {damageMax}\n";
        #endregion

        #region blood description
        //add blood description
        description += $"Blood: {blood} units, requires {bloodRequiredToFunction} to function.\n";
        #endregion

        #region blood loss description
        //add blood description
        description += $"Losing {bloodLossRate} units of blood per second.\n";
        #endregion

        #region oxygen description
        //add oxygen description
        description += $"Oxygen: {oxygen} / {oxygenMax}, requires {oxygenRequired} per second to function.\n";
        #endregion

        #region drugs description
        //add health potion description
        if (healthPotion > 0.0f)
        {
            description += $"Health Potion: {healthPotion} Units.\n";
        }

        //add antidote description
        if (antidote > 0.0f)
        {
            description += $"Antidote: {antidote} Units.\n";
        }

        //add slow poison description
        if (slowPoison > 0.0f)
        {
            description += $"Slow Poison: {slowPoison} Units.\n";
        }

        //add stasis potion description
        if (stasisPotion > 0.0f)
        {
            description += $"Stasis Potion: {stasisPotion} Units.\n";
        }

        //add stasis potion description
        if (hastePotion > 0.0f)
        {
            description += $"Haste Potion: {hastePotion} Units.\n";
        }

        //add coagulant potion description
        if (coagulantPotion > 0.0f)
        {
            description += $"Coagulant Postion: {coagulantPotion} Units.\n";
        }
        #endregion

        #region charms description
        //add heart charm description
        if (this.GetComponent<HeartCharm>() != null)
        {
            HeartCharm heartCharm = this.GetComponent<HeartCharm>();
            description += $"Heart Charm: {heartCharm.expiryTime - heartCharm.timeElapsed} seconds remaining.\n";
        }

        //add lung charm description
        if (this.GetComponent<LungCharm>() != null)
        {
            LungCharm lungCharm = this.GetComponent<LungCharm>();
            description += $"Lung Charm: {lungCharm.expiryTime - lungCharm.timeElapsed} seconds remaining.\n";
        }

        //add petrification charm description
        if (this.GetComponent<PetrificationCharm>() != null)
        {
            PetrificationCharm petrificationCharm = this.GetComponent<PetrificationCharm>();
            description += $"Petrification Charm: {petrificationCharm.expiryTime - petrificationCharm.timeElapsed} seconds remaining.\n";
        }

        #endregion

        #region connections description
        //add connections description
        description += "Connected to: ";
        for (int i = 0; i < connectedBodyParts.Count; i++)
        {
            description += connectedBodyParts[i].transform.name;
            if (i != connectedBodyParts.Count - 1)
            { description += ", "; }
        }
        #endregion

        return description;


    }
}
