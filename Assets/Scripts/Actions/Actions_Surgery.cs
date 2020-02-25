﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Surgery
{

    public static void RemoveBodyPart(GameObject bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(RemoveBodyPartCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator RemoveBodyPartCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().SeverAllConnections();
    }

    public static void ConnectBodyParts(GameObject bodyPartObject1, GameObject bodyPartObject2, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(20.0f * 60.0f);
        StaticCoroutine.Start(ConnectBodyPartCoroutine(bodyPartObject1, bodyPartObject2, seconds));
    }

    public static IEnumerator ConnectBodyPartCoroutine(GameObject bodyPartObject1, GameObject bodyPartObject2, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject1.GetComponent<BodyPart>().CreateConnection(bodyPartObject2);
        bodyPartObject2.GetComponent<BodyPart>().CreateConnection(bodyPartObject1);
    }

    public static void DeleteBodyPart(GameObject bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(DeleteBodyPartCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator DeleteBodyPartCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return RemoveBodyPartCoroutine(bodyPartObject, seconds);
        GameObject.Destroy(bodyPartObject);
    }

}