using System.Collections;
using UnityEngine;

public static class Actions_Blood
{

    //apply bandages to selected bodypart
    public static void Bandages(BodyPart bodyPart, float seconds, int goldCost, float bloodLossReduction)
    {
        StaticCoroutine.Start(BandagesCoroutine(bodyPart, seconds, goldCost, bloodLossReduction));
    } 
    
    private static IEnumerator BandagesCoroutine(BodyPart bodyPart, float seconds, int goldCost, float bloodLossReduction)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().bloodLossRate = Mathf.Max(0, bodyPart.GetComponent<BodyPart>().bloodLossRate - bloodLossReduction);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_bandages += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectBloodAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void Bloodletting(BodyPart bodyPart, float seconds, int goldCost, float bloodLossInduction)
    {
        StaticCoroutine.Start(BloodlettingCoroutine(bodyPart, seconds, goldCost, bloodLossInduction));
    }

    private static IEnumerator BloodlettingCoroutine(BodyPart bodyPart, float seconds, int goldCost, float bloodLossInduction)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().bloodLossRate += bloodLossInduction;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_lettings += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectBloodAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void AddBlood(BodyPart bodyPart, float seconds, int goldCost, int blood)
    {
        StaticCoroutine.Start(AddBloodCoroutine(bodyPart, seconds, goldCost, blood));
    }

    private static IEnumerator AddBloodCoroutine(BodyPart bodyPart, float seconds, int goldCost, int blood)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().blood += blood;
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_injected += blood;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectBloodAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void RemoveBlood(BodyPart bodyPart, float seconds, int goldCost, float blood)
    {
        StaticCoroutine.Start(RemoveBloodCoroutine(bodyPart, seconds, goldCost, blood));
    }

    private static IEnumerator RemoveBloodCoroutine(BodyPart bodyPart, float seconds, int goldCost, float blood)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        if (!clock.actionCancelFlag)
        {
            bodyPart.GetComponent<BodyPart>().LoseBloodAmount(blood);
            MonoBehaviour.FindObjectOfType<ActionTracker>().blood_extracted += blood;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectBloodAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

}
