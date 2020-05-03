using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Medicine
{

    public static void InjectHealthPotion(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectHealthPotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectHealthPotionCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.healthPotion += 50.0f;
    }

    public static void InjectAntidote(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectAntidoteCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectAntidoteCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.antidote += 50.0f;
    }

    public static void InjectSlowPoison(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectSlowPoisonCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectSlowPoisonCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.slowPoison += 50.0f;
    }

    public static void InjectStasisPotion(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectStasisPotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectStasisPotionCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.stasisPotion += 50.0f;
    }

}
