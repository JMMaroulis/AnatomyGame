using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Medicine
{

    public static void InjectHealthPotion(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(InjectHealthPotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectHealthPotionCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.healthPotion += 50.0f;
    }

    public static void InjectAntidote(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(InjectAntidoteCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectAntidoteCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.antidote += 50.0f;
    }

    public static void InjectSlowPoison(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(InjectSlowPoisonCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectSlowPoisonCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.slowPoison += 50.0f;
    }

}
