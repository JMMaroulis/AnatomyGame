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

}
