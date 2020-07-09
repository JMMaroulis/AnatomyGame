﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_SpawnBodyParts
{
    #region organs

    public static void SpawnHeart(float seconds, int goldCost)
    {

        StaticCoroutine.Start(SpawnHeartCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnHeartCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHeart("newHeart");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLung(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLungCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLungCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLung("newLung");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnBrain(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnBrainCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnBrainCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnBrain("newBrain");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnEye(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnEyeCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnEyeCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnEye("newEye");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLiver(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLiverCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLiverCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLiver("newLiver");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnStomach(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnStomachCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnStomachCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnStomach("newStomach");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
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
        StaticCoroutine.Start(SpawnArmCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnArmCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnArm("newArm");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnLeg(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLegCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLegCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeg("newLeg");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnTorso(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnTorsoCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnTorsoCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("newTorso");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void SpawnHead(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnHeadCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnHeadCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHead("newHead");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    #endregion

}
