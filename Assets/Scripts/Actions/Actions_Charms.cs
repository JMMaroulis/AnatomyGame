using System.Collections;
using UnityEngine;

public static class Actions_Charms
{

    public static void ApplyHeartCharm(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyHeartCharmCoroutine(bodyPart, seconds, goldCost));
    }

    public static IEnumerator ApplyHeartCharmCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        
        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPart.gameObject.AddComponent<HeartCharm>();
            MonoBehaviour.FindObjectOfType<ActionTracker>().charm_heart += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectCharmAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyLungCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyLungCharmCoroutine(bodyPartObject, seconds, goldCost));
    }

    public static IEnumerator ApplyLungCharmCoroutine(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPartObject.gameObject.AddComponent<LungCharm>();
            MonoBehaviour.FindObjectOfType<ActionTracker>().charm_lung += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectCharmAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyPetrificationCharm(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyPetrificationCharmCoroutine(bodyPart, seconds, goldCost));
    }

    public static IEnumerator ApplyPetrificationCharmCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            ApplyPetrificationCharmProcess(bodyPart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectCharmAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyPetrificationCharmProcess(BodyPart bodyPart)
    {
        bodyPart.gameObject.AddComponent<PetrificationCharm>();
        MonoBehaviour.FindObjectOfType<ActionTracker>().charm_petrification += 1;
    }

    public static void ApplyBloodRegenCharm(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyBloodRegenCharmCoroutine(bodyPart, seconds, goldCost));
    }

    public static IEnumerator ApplyBloodRegenCharmCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPart.gameObject.AddComponent<BloodRegenCharm>();
            MonoBehaviour.FindObjectOfType<ActionTracker>().charm_petrification += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
