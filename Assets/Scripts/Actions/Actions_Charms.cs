using System.Collections;
using UnityEngine;

public static class Actions_Charms
{

    public static void ApplyHeartCharm(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(ApplyHeartCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyHeartCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.gameObject.AddComponent<HeartCharm>();
    }

    public static void ApplyLungCharm(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(ApplyLungCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyLungCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.gameObject.AddComponent<LungCharm>();
    }

    public static void ApplyPetrificationCharm(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(ApplyPetrificationCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyPetrificationCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.gameObject.AddComponent<PetrificationCharm>();
    }

    public static void ApplyBloodRegenCharm(BodyPart bodyPartObject, float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(ApplyBloodRegenCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyBloodRegenCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        bodyPartObject.gameObject.AddComponent<BloodRegenCharm>();
    }

}
