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
            bodyPartObject.healthPotion += 100.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_health += 100;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectAntidote(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectAntidoteCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectAntidoteCoroutine(BodyPart bodyPartObject, float seconds)
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
            bodyPartObject.antidote += 50.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_antidote += 50;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectSlowPoison(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectSlowPoisonCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectSlowPoisonCoroutine(BodyPart bodyPartObject, float seconds)
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
            bodyPartObject.slowPoison += 50.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_slowpoison += 50;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectStasisPotion(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectStasisPotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectStasisPotionCoroutine(BodyPart bodyPartObject, float seconds)
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
            bodyPartObject.stasisPotion += 50.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_stasis += 50;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectHastePotion(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectHastePotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectHastePotionCoroutine(BodyPart bodyPartObject, float seconds)
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
            bodyPartObject.hastePotion += 50.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_haste += 50;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectCoagulantPotion(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(InjectCoagulantPotionCoroutine(bodyPartObject, seconds));
    }

    public static IEnumerator InjectCoagulantPotionCoroutine(BodyPart bodyPartObject, float seconds)
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
            bodyPartObject.coagulantPotion += 50.0f;
            MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_coagulant += 50;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }
}
