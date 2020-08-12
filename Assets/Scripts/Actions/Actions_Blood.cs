using System.Collections;
using UnityEngine;

public static class Actions_Blood
{

    //apply bandages to selected bodypart
    public static void Bandages(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(BandagesCoroutine(bodyPart, seconds, goldCost));
    } 
    
    private static IEnumerator BandagesCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().bloodLossRate - 10);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_bandages += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void Bloodletting(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(BloodlettingCoroutine(bodyPart, seconds, goldCost));
    }

    private static IEnumerator BloodlettingCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().bloodLossRate += 10;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_lettings += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void AddBlood(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(AddBloodCoroutine(bodyPart, seconds, goldCost));
    }

    private static IEnumerator AddBloodCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().blood += 100;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_injected += 100;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void RemoveBlood(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(RemoveBloodCoroutine(bodyPart, seconds, goldCost));
    }

    private static IEnumerator RemoveBloodCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().blood = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().blood - 100);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_extracted += 100;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
