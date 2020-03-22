using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Blood
{
    //apply bandages to selected bodypart
    public static void Bandages(BodyPart bodyPart, float seconds)
    {
        Debug.Log("Applying bandages to" + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(BanadagesCoroutine(bodyPart, seconds));
    }
    
    
    private static IEnumerator BanadagesCoroutine(BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPart.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().bloodLossRate - 10);
    }
    

    public static void Bloodletting(BodyPart bodyPart, float seconds)
    {
        Debug.Log("Cutting " + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(BloodlettingCoroutine(bodyPart, seconds));
    }

    private static IEnumerator BloodlettingCoroutine(BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPart.GetComponent<BodyPart>().bloodLossRate += 10;
    }

    public static void AddBlood(BodyPart bodyPart, float seconds)
    {
        Debug.Log("Adding blood to " + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(AddBloodCoroutine(bodyPart, seconds));
    }

    private static IEnumerator AddBloodCoroutine(BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPart.GetComponent<BodyPart>().blood += 100;
    }

    public static void RemoveBlood(BodyPart bodyPart, float seconds)
    {
        Debug.Log("Removing blood from" + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(RemoveBloodCoroutine(bodyPart, seconds));
    }

    private static IEnumerator RemoveBloodCoroutine(BodyPart bodyPart, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPart.GetComponent<BodyPart>().blood = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().blood - 100);
    }

}
