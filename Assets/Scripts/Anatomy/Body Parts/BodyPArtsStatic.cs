using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Methods to be implemented by classes inheriting BodyPart.cs have a standard version centralised here.
public static class BodyPartsStatic
{

    //Pumps blood, if there is blood left to pump.
    public static void PumpBlood(float pumpRate, float timeSinceLastPump, ref float blood, ref List<BodyPart> connectedBodyParts)
    {
        //pumping blood to body parts in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between themf
        List<int> pumpOrder = Enumerable.Range(0, connectedBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(pumpOrder);
        foreach (int bodyPartIndex in pumpOrder)
        {
            if (connectedBodyParts[bodyPartIndex].blood <= blood)
            {
                float proposedBloodOut = pumpRate * timeSinceLastPump;
                float bloodOut = Mathf.Max(Mathf.Min(blood, proposedBloodOut), 0);
                connectedBodyParts[bodyPartIndex].blood += bloodOut;
                blood -= bloodOut;
            }
        }
    }

    //applies blood loss rate
    public static void LoseBlood(float lossRate, float timeSinceLastLoss, ref float blood)
    {
        float bloodLost = lossRate * timeSinceLastLoss;
        blood = Mathf.Max(blood - bloodLost, 0);
    }

    public static bool CheckForFunctionality(float blood, float bloodRequiredToFunction)
    {
        if (blood < bloodRequiredToFunction)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

}
