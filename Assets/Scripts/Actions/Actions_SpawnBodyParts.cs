﻿using System.Collections;
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
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnHeart("newHeart");
    }

    public static void SpawnLung(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLungCoroutine(seconds));
    }

    public static IEnumerator SpawnLungCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnLung("newLung");
    }

    public static void SpawnBrain(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnBrainCoroutine(seconds));
    }

    public static IEnumerator SpawnBrainCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnBrain("newBrain");
    }

    public static void SpawnEye(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnEyeCoroutine(seconds));
    }

    public static IEnumerator SpawnEyeCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnEye("newEye");
    }

    public static void SpawnLiver(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLiverCoroutine(seconds));
    }

    public static IEnumerator SpawnLiverCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnLiver("newLiver");
    }

    public static void SpawnStomach(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnStomachCoroutine(seconds));
    }

    public static IEnumerator SpawnStomachCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnStomach("newStomach");
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
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnArm("newArm");
    }

    public static void SpawnLeg(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnLegCoroutine(seconds));
    }

    public static IEnumerator SpawnLegCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnLeg("newLeg");
    }

    public static void SpawnTorso(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnTorsoCoroutine(seconds));
    }

    public static IEnumerator SpawnTorsoCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("newTorso");
    }

    public static void SpawnHead(float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(SpawnHeadCoroutine(seconds));
    }

    public static IEnumerator SpawnHeadCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnHead("newHead");
    }

    #endregion

}
