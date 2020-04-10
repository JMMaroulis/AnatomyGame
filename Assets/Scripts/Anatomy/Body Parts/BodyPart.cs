using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
    public bool isTimePassing;
    public float timeScale;
    public bool isFunctioning;

    //blood stuff
    public float bloodRequiredToFunction;
    public float blood;
    public float bloodLossRate;
    public float bloodPumpRate;


    //oxygen stuff
    public float oxygen;
    public float oxygenMax;
    public float oxygenRequired;

    public float damage;
    public float damageMax;
    public float efficiency;

    public List<BodyPart> connectedBodyParts;
    public List<Organ> containedOrgans;

    private bool isConnectedToBrain;
    private float connectedBrainEfficiency;


    //drug stuff
    public float healthPotion;

    public void UpdateBodyPart(float deltaTime)
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

    //Pumps blood, if there is blood left to pump.
    public void PumpBlood(float heartEfficiency, float deltaTime)
    {
        float timeSinceLastPump = deltaTime;

        //pumping blood to contained organs in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<BodyPart> allBodyParts = connectedBodyParts.Concat(containedOrgans).ToList<BodyPart>();
        List<int> bodypartPumpOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartPumpOrder);
        foreach (int bodyPartIndex in bodypartPumpOrder)
        {
            BodyPart bodyPart = allBodyParts[bodyPartIndex];
            float tempBloodPumpRate = Mathf.Max(Mathf.Min(bloodPumpRate * (blood / bodyPart.blood), bloodPumpRate * 5), bloodPumpRate * 0.2f) * heartEfficiency;
            float tempOxygenPumpRate = Mathf.Max(Mathf.Min(bloodPumpRate * (oxygen / bodyPart.oxygen), bloodPumpRate * 5), bloodPumpRate * 0.2f) * heartEfficiency;
            float tempHealthPotionPumpRate = Mathf.Max(Mathf.Min(bloodPumpRate * (healthPotion / bodyPart.healthPotion * 5), bloodPumpRate * 0.2f), bloodPumpRate * 0.01f) * heartEfficiency;

            //transport blood
            float proposedBloodOut = tempBloodPumpRate * timeSinceLastPump;
            float bloodOut = Mathf.Max(Mathf.Min(blood, proposedBloodOut), 0);
            bodyPart.blood += bloodOut;
            blood -= bloodOut;

            //transport oxygen, capped by blood transport
            float proposedOxygenOut = Mathf.Min(tempOxygenPumpRate * timeSinceLastPump, bloodOut);
            float oxygenOut = Mathf.Max(Mathf.Min(oxygen, proposedOxygenOut), 0);
            bodyPart.oxygen += oxygenOut;
            oxygen -= oxygenOut;

            //transport health potion, capped by blood transport
            float proposedHealthPotionOut = Mathf.Min(tempHealthPotionPumpRate * timeSinceLastPump, bloodOut);
            float healthPotionOut = Mathf.Max(Mathf.Min(healthPotion, proposedHealthPotionOut), 0);
            bodyPart.healthPotion += healthPotionOut;
            healthPotion -= healthPotionOut;
        }
    }

    //applies blood loss rate
    public void LoseBlood(float deltaTime)
    {
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
        float timeSinceLastConsumption = deltaTime;
        float oxygenconsumed = oxygenRequired * timeSinceLastConsumption;
        oxygen = Mathf.Max(oxygen - oxygenconsumed, 0);
    }

    public void ApplyDrugs(float deltaTime)
    {
        if (healthPotion > 0.0f)
        {
            if (damage > 0.0f)
            {
                //in 1 second, will process 1/5 unit of health potion
                float healthPotionProcessed = Mathf.Min(healthPotion, deltaTime * 0.5f);
                healthPotion = Mathf.Max(0.0f, healthPotion - healthPotionProcessed);
                damage = Mathf.Max(0.0f, damage - healthPotionProcessed);
            }
            else
            {
                //decays at 1/100th unit per second, if no damage to be healed
                healthPotion = Mathf.Max(0.0f, healthPotion - deltaTime * 0.01f);
            }
        }
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

        if (currentBodyPart is Brain)
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

    public void UpdateEfficiency()
    {
        IsConnectedToBrainStarter();

        float damageRatio = 1 - (damage / damageMax); //1 good, 0 bad
        float oxygenRatio = Mathf.Min((oxygen / oxygenRequired), 1); //1 good, 0 bad
        efficiency = damageRatio * oxygenRatio * connectedBrainEfficiency;

    }

    public void UpdateDamage(float deltaTime)
    {
        float oxygenRatio = 1 - Mathf.Min((oxygen / oxygenRequired), 1); //0 good, 1 bad
        damage = Mathf.Min(damage + (oxygenRatio * deltaTime), damageMax);
    }

    public void SeverConnectionOutgoing(GameObject connectedBodyPart, float inducedBloodLossRate)
    {
        //severing connection to bodypart
        connectedBodyParts.Remove(connectedBodyPart.GetComponent<BodyPart>());

        //severing connection to organ
        containedOrgans.Remove(connectedBodyPart.GetComponent<Organ>());

        bloodLossRate += inducedBloodLossRate;
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
    }

    public void AddContainedOrgan(Organ organToImplant)
    {
        containedOrgans.Add(organToImplant);
    }


    /*
    public string GenerateDescription()
    {
        string description = "";

        #region damage description
        //add damage description
        description += "The " + this.gameObject.name + " is ";
        if (damage <= 0.1f)
        {
            description += "undamaged.";
        }
        else if (damage > 0.1f && damage <= damageMax / 4)
        {
            description += "lightly damaged.";
        }
        else if (damage > damageMax / 4 && damage <= damageMax / 2)
        {
            description += "quite damaged.";
        }
        else if (damage > damageMax / 2 && damage <= (3 * damageMax) / 4)
        {
            description += "heavily damaged.";
        }
        else if (damage > (3 * damageMax) / 4 && damage < damageMax)
        {
            description += "severely damaged.";
        }
        else if (damage == damageMax)
        {
            description += "completely destroyed.";
        }
        else
        {
            description += "INVALID DAMAGE VALUE: INVESTIGATE THIS.";
        }
        description += "\n";
        #endregion

        #region blood description
        //add blood description
        description += "The " + this.gameObject.name + " is ";
        if (blood <= 0.1f)
        {
            description += "completely drained.";
        }
        else if (blood > 0.1f && blood <= 25.0f)
        {
            description += "deathly pale.";
        }
        else if (blood > 25.0f && blood <= 50.0f)
        {
            description += "quite pale.";
        }
        else if (blood > 50.0f && blood <= 100.0f)
        {
            description += "a tad pale.";
        }
        else if (blood > 100.0f)
        {
            description += "nice and pink.";
        }
        else
        {
            description += "INVALID DAMAGE VALUE: INVESTIGATE THIS.";
        }
        description += "\n";
        #endregion

        #region oxygen description
        //add oxygen description
        description += "The " + this.gameObject.name + " is ";
        if (oxygen <= 0.1f)
        {
            description += "deeply blue.";
        }
        else if (oxygen > 0.1f && oxygen <= 25.0f)
        {
            description += "dark blue.";
        }
        else if (oxygen > 25.0f && oxygen <= 50.0f)
        {
            description += "quite blue.";
        }
        else if (oxygen > 50.0f && oxygen <= 100.0f)
        {
            description += "a tad blue.";
        }
        else if (oxygen > 100.0f)
        {
            description += "not blue.";
        }
        else
        {
            description += "INVALID DAMAGE VALUE: INVESTIGATE THIS.";
        }
        description += "\n";
        #endregion

        #region connections description
        //add connections description
        description += "The " + this.gameObject.name + " is connected to: ";
        for (int i = 0; i < connectedBodyParts.Count; i++)
        {
            description += connectedBodyParts[i].transform.name;
            if (i != connectedBodyParts.Count - 1)
            { description += ","; }
        }
        #endregion

        return description;
    }
    */

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
        description += $"Losing {bloodLossRate} of blood per second.\n";
        #endregion

        #region oxygen description
        //add oxygen description
        description += $"Oxygen: {oxygen} / {oxygenMax}, requires {oxygenRequired} per second to function.\n";
        #endregion

        #region drugs description
        //add health potion desctiption
        if (healthPotion > 0.0f)
        {
            description += $"Health Potion: {healthPotion} Units.\n";
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
