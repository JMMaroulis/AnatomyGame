using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_SpawnBodyParts
{
    #region organs

    public static void SpawnHeart(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnHeartCoroutine(seconds));
    }

    public static IEnumerator SpawnHeartCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnHeart("newHeart");
    }

    public static void SpawnLung(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnLungCoroutine(seconds));
    }

    public static IEnumerator SpawnLungCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnLung("newLung");
    }

    public static void SpawnBrain(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnBrainCoroutine(seconds));
    }

    public static IEnumerator SpawnBrainCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnBrain("newBrain");
    }

    #endregion

    #region bodyparts

    public static void SpawnArm(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnArmCoroutine(seconds));
    }

    public static IEnumerator SpawnArmCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnArm("newArm");
    }

    public static void SpawnLeg(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnLegCoroutine(seconds));
    }

    public static IEnumerator SpawnLegCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnLeg("newLeg");
    }

    public static void SpawnTorso(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnTorsoCoroutine(seconds));
    }

    public static IEnumerator SpawnTorsoCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("newTorso");
    }

    public static void SpawnHead(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        StaticCoroutine.Start(SpawnHeadCoroutine(seconds));
    }

    public static IEnumerator SpawnHeadCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("newHead");
    }

    #endregion

}
