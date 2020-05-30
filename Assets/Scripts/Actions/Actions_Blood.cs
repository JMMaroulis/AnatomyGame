using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Blood
{

    //apply bandages to selected bodypart
    public static void Bandages(BodyPart bodyPart, float seconds, int goldCost)
    {
        Debug.Log("Applying bandages to" + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(BanadagesCoroutine(bodyPart, seconds));
    } 
    
    private static IEnumerator BanadagesCoroutine(BodyPart bodyPart, float seconds)
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
            bodyPart.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().bloodLossRate - 10);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_bandages += 1;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void Bloodletting(BodyPart bodyPart, float seconds, int goldCost)
    {
        Debug.Log("Cutting " + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(BloodlettingCoroutine(bodyPart, seconds));
    }

    private static IEnumerator BloodlettingCoroutine(BodyPart bodyPart, float seconds)
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
            bodyPart.GetComponent<BodyPart>().bloodLossRate += 10;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_lettings += 1;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void AddBlood(BodyPart bodyPart, float seconds, int goldCost)
    {
        Debug.Log("Adding blood to " + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(AddBloodCoroutine(bodyPart, seconds));
    }

    private static IEnumerator AddBloodCoroutine(BodyPart bodyPart, float seconds)
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
            bodyPart.GetComponent<BodyPart>().blood += 100;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_injected += 100;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void RemoveBlood(BodyPart bodyPart, float seconds, int goldCost)
    {
        Debug.Log("Removing blood from" + bodyPart.name);
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(RemoveBloodCoroutine(bodyPart, seconds));
    }

    private static IEnumerator RemoveBloodCoroutine(BodyPart bodyPart, float seconds)
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
            bodyPart.GetComponent<BodyPart>().blood = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().blood - 100);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_extracted += 100;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
