using System.Collections;
using UnityEngine;

public static class Actions_Charms
{

    public static void ApplyHeartCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyHeartCharmCoroutine(bodyPartObject, seconds, goldCost));
    }

    public static IEnumerator ApplyHeartCharmCoroutine(BodyPart bodyPartObject, float seconds, int goldCost)
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
            bodyPartObject.gameObject.AddComponent<HeartCharm>();
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

    public static void ApplyPetrificationCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyPetrificationCharmCoroutine(bodyPartObject, seconds, goldCost));
    }

    public static IEnumerator ApplyPetrificationCharmCoroutine(BodyPart bodyPartObject, float seconds, int goldCost)
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
            bodyPartObject.gameObject.AddComponent<PetrificationCharm>();
            MonoBehaviour.FindObjectOfType<ActionTracker>().charm_petrification += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectCharmAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void ApplyBloodRegenCharm(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ApplyBloodRegenCharmCoroutine(bodyPartObject, seconds, goldCost));
    }

    public static IEnumerator ApplyBloodRegenCharmCoroutine(BodyPart bodyPartObject, float seconds, int goldCost)
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
            bodyPartObject.gameObject.AddComponent<BloodRegenCharm>();
            MonoBehaviour.FindObjectOfType<ActionTracker>().charm_petrification += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
