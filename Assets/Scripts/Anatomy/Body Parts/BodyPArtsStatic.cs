using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Methods to be implemented by classes inheriting BodyPart.cs have a standard version centralised here.
public static class BodyPartsStatic
{

    //Pumps blood, if there is blood left to pump.
    public static void PumpBlood(float pumpRate, float timeSinceLastPump, ref float blood, ref float oxygen, ref List<BodyPart> connectedBodyParts, ref List<BodyPart> containedOrgans)
    {
        //pumping blood to contained organs in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<int> organPumpOrder = Enumerable.Range(0, containedOrgans.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(organPumpOrder);
        foreach (int organIndex in organPumpOrder)
        {
            BodyPart organ = containedOrgans[organIndex];
            float tempBloodPumpRate = Mathf.Max(Mathf.Min(pumpRate * (blood / organ.blood), pumpRate * 5), pumpRate * 0.2f);
            float tempOxygenPumpRate = Mathf.Max(Mathf.Min(pumpRate * (oxygen / organ.oxygen), pumpRate * 5), pumpRate * 0.2f);

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
            float tempBloodPumpRate = Mathf.Max(Mathf.Min(pumpRate * (blood / bodyPart.blood), pumpRate * 5), pumpRate * 0.2f);
            float tempOxygenPumpRate = Mathf.Max(Mathf.Min(pumpRate * (oxygen / bodyPart.oxygen), pumpRate * 5), pumpRate * 0.2f);

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
        if (oxygen < oxygenRequiredToFunction)
        { return false; }

        else
        {
            return true;
        }

    }

}
