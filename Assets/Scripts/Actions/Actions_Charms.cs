using System.Collections;
using UnityEngine;

public static class Actions_Charms
{

    public static void ApplyHeartCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ApplyHeartCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyHeartCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPartObject.gameObject.AddComponent<HeartCharm>();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyLungCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ApplyLungCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyLungCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPartObject.gameObject.AddComponent<LungCharm>();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyPetrificationCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ApplyPetrificationCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyPetrificationCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPartObject.gameObject.AddComponent<PetrificationCharm>();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyBloodRegenCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(ApplyBloodRegenCharmCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator ApplyBloodRegenCharmCoroutine(BodyPart bodyPartObject, float seconds)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPartObject.gameObject.AddComponent<BloodRegenCharm>();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
