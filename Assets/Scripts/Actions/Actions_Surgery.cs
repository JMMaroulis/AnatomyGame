﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Actions_Surgery
{

    public static void RemoveBodyPart(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(RemoveBodyPartCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator RemoveBodyPartCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().SeverAllConnections();
        UpdateAllBodyPartHeartConnections();
    }

    public static void RemoveOrgan(Organ organObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(RemoveOrganCoroutine(organObject, seconds));
    }

    public static IEnumerator RemoveOrganCoroutine(Organ organObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //disconnect
        organObject.GetComponent<Organ>().SeverAllConnections();
        UpdateAllBodyPartHeartConnections();

        //remove from being child of bodypart
        organObject.transform.SetParent(organObject.transform.parent.parent);

    }

    public static void ImplantOrgan(Organ organ, BodyPart bodyPart, float seconds, int goldCost)
    {

        if (organ.connectedBodyParts.Count != 0)
        {
            Debug.Log("That organ is already inside something! Don't do that!");
            return;
        }

        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ImplantOrganCoroutine(organ, bodyPart, seconds));
    }

    public static IEnumerator ImplantOrganCoroutine(Organ organ, BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        //connect
        organ.CreateConnection(bodyPart);
        bodyPart.AddContainedOrgan(organ);
        UpdateAllBodyPartHeartConnections();

        //make organ child of bodypart
        organ.transform.SetParent(bodyPart.transform);

    }


    public static void ConnectBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ConnectBodyPartCoroutine(bodyPart1, bodyPart2, seconds));
    }

    public static IEnumerator ConnectBodyPartCoroutine(BodyPart bodyPart1, BodyPart bodyPart2, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPart1.CreateConnection(bodyPart2);
        bodyPart2.CreateConnection(bodyPart1);
        UpdateAllBodyPartHeartConnections();
    }

    public static void DeleteBodyPart(BodyPart bodyPart, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(DeleteBodyPartCoroutine(bodyPart, seconds));
    }

    public static IEnumerator DeleteBodyPartCoroutine(BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        foreach(Organ organ in bodyPart.containedOrgans)
        {
            GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(organ);
            GameObject.Destroy(organ.gameObject);
        }
        GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(bodyPart);
        GameObject.Destroy(bodyPart.gameObject);
        UpdateAllBodyPartHeartConnections();
    }

    private static void UpdateAllBodyPartHeartConnections()
    {
        //List<BodyPart> bodyParts = FindObjectsOfType<BodyPart>().ToList();
        //foreach(BodyPart bodyPart in bodyParts)
        //{
        //    bodyPart.UpdateHeartConnections();
        //}
    }

}
