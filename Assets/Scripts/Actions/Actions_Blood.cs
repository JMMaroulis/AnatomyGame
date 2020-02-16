using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Blood
{
    //apply bandages to selected bodypart
    public static void Bandages(GameObject bodyPartObject, float seconds)
    {
        Debug.Log("Applying bandages to" + bodyPartObject.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(BanadagesCoroutine(bodyPartObject, seconds));
    }
    
    
    private static IEnumerator BanadagesCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPartObject.GetComponent<BodyPart>().bloodLossRate - 10);
    }
    

    public static void Bloodletting(GameObject bodyPartObject, float seconds)
    {
        Debug.Log("Cutting " + bodyPartObject.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(BloodlettingCoroutine(bodyPartObject, seconds));
    }

    private static IEnumerator BloodlettingCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().bloodLossRate += 10;
    }

    public static void AddBlood(GameObject bodyPartObject, float seconds)
    {
        Debug.Log("Adding blood to " + bodyPartObject.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(AddBloodCoroutine(bodyPartObject, seconds));
    }

    private static IEnumerator AddBloodCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().blood += 100;
    }

    public static void RemoveBlood(GameObject bodyPartObject, float seconds)
    {
        Debug.Log("Removing blood from" + bodyPartObject.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(RemoveBloodCoroutine(bodyPartObject, seconds));
    }

    private static IEnumerator RemoveBloodCoroutine(GameObject bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.GetComponent<BodyPart>().blood = Mathf.Max(0, bodyPartObject.GetComponent<BodyPart>().blood - 100);
    }

}
