using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Blood
{
    //apply bandages to selected bodypart
    public static void Bandages(GameObject bodyPartObject)
    {
        Debug.Log("Applying bandages to" + bodyPartObject.name);
        bodyPartObject.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPartObject.GetComponent<BodyPart>().bloodLossRate - 10);
    }

    public static void Bloodletting(GameObject bodyPartObject)
    {
        Debug.Log("Cutting " + bodyPartObject.name);
        bodyPartObject.GetComponent<BodyPart>().bloodLossRate += 10;
    }

    public static void AddBlood(GameObject bodyPartObject)
    {
        Debug.Log("Adding blood to " + bodyPartObject.name);
        bodyPartObject.GetComponent<BodyPart>().blood += 100;
    }

    public static void RemoveBlood(GameObject bodyPartObject)
    {
        Debug.Log("Removing blood from" + bodyPartObject.name);
        bodyPartObject.GetComponent<BodyPart>().blood = Mathf.Max(0, bodyPartObject.GetComponent<BodyPart>().blood - 100);
    }

}
