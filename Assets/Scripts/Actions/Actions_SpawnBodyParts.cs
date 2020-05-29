using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_SpawnBodyParts
{
    #region organs

    public static void SpawnHeart(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnHeartCoroutine(seconds));
    }

    public static IEnumerator SpawnHeartCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHeart("newHeart");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLung(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLungCoroutine(seconds));
    }

    public static IEnumerator SpawnLungCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLung("newLung");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnBrain(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnBrainCoroutine(seconds));
    }

    public static IEnumerator SpawnBrainCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnBrain("newBrain");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnEye(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnEyeCoroutine(seconds));
    }

    public static IEnumerator SpawnEyeCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnEye("newEye");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLiver(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLiverCoroutine(seconds));
    }

    public static IEnumerator SpawnLiverCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLiver("newLiver");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnStomach(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnStomachCoroutine(seconds));
    }

    public static IEnumerator SpawnStomachCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnStomach("newStomach");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    #endregion

    #region bodyparts

    public static void SpawnArm(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnArmCoroutine(seconds));
    }

    public static IEnumerator SpawnArmCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnArm("newArm");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLeg(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLegCoroutine(seconds));
    }

    public static IEnumerator SpawnLegCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeg("newLeg");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnTorso(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnTorsoCoroutine(seconds));
    }

    public static IEnumerator SpawnTorsoCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("newTorso");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnHead(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnHeadCoroutine(seconds));
    }

    public static IEnumerator SpawnHeadCoroutine(float seconds)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHead("newHead");
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    #endregion

}
