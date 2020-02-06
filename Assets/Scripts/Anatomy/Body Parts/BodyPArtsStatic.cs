using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Methods to be implemented by classes inheriting BodyPart.cs have a standard version centralised here.
public static class BodyPartsStatic
{

    //Pumps blood, if there is blood left to pump.
    public static void PumpBlood(float efficiency, float pumpRate, float timeSinceLastPump, ref float blood, ref float oxygen, ref List<BodyPart> connectedBodyParts, ref List<BodyPart> containedOrgans)
    {
        //pumping blood to contained organs in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<int> organPumpOrder = Enumerable.Range(0, containedOrgans.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(organPumpOrder);
        foreach (int organIndex in organPumpOrder)
        {
            BodyPart organ = containedOrgans[organIndex];
            float tempBloodPumpRate = Mathf.Max(Mathf.Min(pumpRate * (blood / organ.blood), pumpRate * 5), pumpRate * 0.2f) * efficiency;
            float tempOxygenPumpRate = Mathf.Max(Mathf.Min(pumpRate * (oxygen / organ.oxygen), pumpRate * 5), pumpRate * 0.2f) * efficiency;

            //transport blood
            float proposedBloodOut = tempBloodPumpRate * timeSinceLastPump;
            float bloodOut = Mathf.Max(Mathf.Min(blood, proposedBloodOut), 0);
            organ.blood += bloodOut;
            blood -= bloodOut;

            //transport oxygen, capped by blood transport
            float proposedOxygenOut = Mathf.Min(tempOxygenPumpRate * timeSinceLastPump, bloodOut);
            float oxygenOut = Mathf.Max(Mathf.Min(oxygen, proposedOxygenOut), 0);
            oxygenOut = Mathf.Min(organ.oxygenMax - organ.oxygen, oxygenOut );
            organ.oxygen += oxygenOut;
            oxygen -= oxygenOut;
        }

        //pumping blood to body parts in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<int> limbPumpOrder = Enumerable.Range(0, connectedBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(limbPumpOrder);
        foreach (int bodyPartIndex in limbPumpOrder)
        {
            BodyPart bodyPart = connectedBodyParts[bodyPartIndex];
            float tempBloodPumpRate = Mathf.Max(Mathf.Min(pumpRate * (blood / bodyPart.blood), pumpRate * 5), pumpRate * 0.2f) * efficiency;
            float tempOxygenPumpRate = Mathf.Max(Mathf.Min(pumpRate * (oxygen / bodyPart.oxygen), pumpRate * 5), pumpRate * 0.2f) * efficiency;

            //transport blood
            float proposedBloodOut = tempBloodPumpRate * timeSinceLastPump;
            float bloodOut = Mathf.Max(Mathf.Min(blood, proposedBloodOut), 0);
            bodyPart.blood += bloodOut;
            blood -= bloodOut;

            //transport oxygen, capped by blood transport
            float proposedOxygenOut = Mathf.Min(tempOxygenPumpRate * timeSinceLastPump, bloodOut);
            float oxygenOut = Mathf.Max(Mathf.Min(oxygen, proposedOxygenOut), 0);
            oxygenOut = Mathf.Min(bodyPart.oxygenMax - bodyPart.oxygen, oxygenOut);
            bodyPart.oxygen += oxygenOut;
            oxygen -= oxygenOut;
        }
    }

    //applies blood loss rate
    public static void LoseBlood(float lossRate, float timeSinceLastLoss, ref float blood)
    {
        float bloodLost = lossRate * timeSinceLastLoss;
        blood = Mathf.Max(blood - bloodLost, 0);
    }

    public static void ConsumeOxygen(float consumptionRate, float timesinceLastConsumption, ref float oxygen)
    {
        float oxygenconsumed = consumptionRate * timesinceLastConsumption;
        oxygen = Mathf.Max(oxygen - oxygenconsumed, 0);
    }

    public static bool CheckForFunctionality(float blood, float bloodRequiredToFunction, float oxygen, float oxygenRequiredToFunction)
    {
        if (blood < bloodRequiredToFunction)
        { return false; }
        if (oxygen == 0)
        { return false; }

        else
        {
            return true;
        }

    }

    public static float UpdateEfficiency(float damage, float damageMax, float oxygen, float oxygenRequired)
    {
        float damageRatio = 1 - (damage / damageMax); //1 good, 0 bad
        float oxygenRatio = Mathf.Min((oxygen / oxygenRequired),1); //1 good, 0 bad
        float efficiency = damageRatio * oxygenRatio;

        return efficiency;
    }

    public static float UpdateDamage(float damage, float damageMax, float oxygen, float oxygenRequired)
    {
        float oxygenRatio = 1 - Mathf.Min((oxygen / oxygenRequired), 1); //0 good, 1 bad

        damage = Mathf.Min(damage + (oxygenRatio * Time.deltaTime), damageMax);
        return damage;
    }

    //severs connection between self and chosen body part (ONE WAY ONLY)
    public static void SeverConnection(GameObject connectedBodyPart, ref List<GameObject> connectedBodyPartGameObjects, ref List<BodyPart> connectedBodyParts, ref float bloodLossRate, float inducedBloodLossRate)
    {
        connectedBodyPartGameObjects.Remove(connectedBodyPart);
        connectedBodyParts.Remove(connectedBodyPart.GetComponent<BodyPart>());
        bloodLossRate += inducedBloodLossRate;
    }

    //severs all connections between self and connecting bodyparts (BOTH WAYS)
    public static void SeverAllIncomingConnections(GameObject bodyPartObject, List<GameObject> connectedBodyPartGameObjects)
    {
        //ask every connected bodypart to sever *this* bodypart from themselves
        //then sever *that* bodypart from self
        foreach (GameObject connectedBodyPartObject in connectedBodyPartGameObjects)
        {
            connectedBodyPartObject.GetComponent<BodyPart>().SeverConnection(bodyPartObject);
        }

        for (int i = (connectedBodyPartGameObjects.Count-1); i >= 0; i--)
        {
            bodyPartObject.GetComponent<BodyPart>().SeverConnection(connectedBodyPartGameObjects[i]);
        }

    }

    public static void CreateConnection(GameObject bodyPartObjectToConnect, ref List<GameObject> connectedBodyPartObjects, ref List<BodyPart> connectedBodyParts)
    {
        connectedBodyPartObjects.Add(bodyPartObjectToConnect);
        connectedBodyParts.Add(bodyPartObjectToConnect.GetComponent<BodyPart>());
    }

    //Adds any bodyparts not in the bodypart list to it, returning the proper list.
    //NOTE: This method *DOES NOT* remove bodyparts that shouldn't be in the list. Make sure to sever bodypart connection properly, kids.
    public static List<BodyPart> UpdateConnectedBodyParts(ref List<GameObject> connectedBodyPartObjects, ref List<BodyPart> connectedBodyParts)
    {
        foreach (GameObject connectedBodyPartObject in connectedBodyPartObjects)
        {
            //if isn't in bodypart list, add it
            BodyPart connectedBodyPart = connectedBodyPartObject.GetComponent<BodyPart>();
            if (!connectedBodyParts.Contains(connectedBodyPart))
            {
                connectedBodyParts.Add(connectedBodyPart);
            }            
        }

        return connectedBodyParts;
    }

    public static string GenerateDescription(BodyPart bodyPart, List<GameObject> connectedBodyPartObjects, string bodyPartName)
    {
        string description = "";

        #region damage description
        //add damage description
        float damageMax = bodyPart.damageMax;
        float damage = bodyPart.damage;
        description += "The " + bodyPartName + " is ";
        if (damage <= 0.1f) {
            description += "undamaged.";
        }
        else if(damage > 0.1f && damage <= damageMax/4)
        {
            description += "lightly damaged.";
        }
        else if(damage > damageMax / 4 && damage <= damageMax / 2)
        {
            description += "quite damaged.";
        }
        else if(damage> damageMax/2 && damage<= (3 * damageMax) / 4)
        {
            description += "heavily damaged.";
        }
        else if(damage > (3 * damageMax) / 4 && damage < damageMax)
        {
            description += "severely damaged.";
        }
        else if(damage == damageMax)
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
        float blood = bodyPart.blood;
        description += "The " + bodyPartName + " is ";
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
        float oxygen = bodyPart.oxygen;
        description += "The " + bodyPartName + " is ";
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
        description += "The " + bodyPartName + " is connected to: ";
        for (int i = 0; i < connectedBodyPartObjects.Count; i++)
        {
            description += connectedBodyPartObjects[i].transform.name;
            if (i != connectedBodyPartObjects.Count-1)
            {   description += ",";  }
        }
        #endregion

        return description;
    }

}
