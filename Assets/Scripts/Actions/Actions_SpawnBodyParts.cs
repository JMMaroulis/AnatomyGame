using System;
using System.Collections;
using UnityEngine;

public static class Actions_SpawnBodyParts
{
    public static void SpawnBodyPartTemplate(float seconds, int goldCost, string partName, Func<string, BodyPart> spawnMethod)
    {
        StaticCoroutine.Start(SpawnBodyPartCoroutineTemplate(seconds, goldCost, partName, spawnMethod));
    }

    public static IEnumerator SpawnBodyPartCoroutineTemplate(float seconds, int goldCost, string partName, Func<string, BodyPart> spawnMethod)
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
            SpawnBodyPartProcessTemplate(goldCost, partName, spawnMethod);
        }
        else
        {
            clock.actionCancelFlag = false;
        }

        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnBodyPartProcessTemplate(int goldCost, string partName, Func<string, BodyPart> spawnMethod)
    {
        spawnMethod(partName);
        MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
        MonoBehaviour.FindObjectOfType<ButtonActions>().SelectSpawnOrganActionOptions();
    }

    #region organs

    public static void SpawnHeart(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Heart", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnHeart);
    }

    public static void SpawnLeftLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftLung);
    }

    public static void SpawnRightLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightLung);
    }

    public static void SpawnBrain(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Brain", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnBrain);
    }

    public static void SpawnLeftEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftEye);
    }

    public static void SpawnRightEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightEye);
    }

    public static void SpawnLiver(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Liver", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLiver);
    }

    public static void SpawnStomach(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Stomach", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnStomach);
    }

    #endregion

    #region clockworkOrgans

    public static void SpawnClockworkHeart(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Heart", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkHeart);
    }

    public static void SpawnClockworkLeftLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftLung);
    }

    public static void SpawnClockworkRightLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightLung);
    }

    public static void SpawnClockworkBrain(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Brain", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkBrain);
    }

    public static void SpawnClockworkLeftEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftEye);
    }

    public static void SpawnClockworkRightEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightEye);
    }

    public static void SpawnClockworkLiver(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Liver", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLiver);
    }

    public static void SpawnClockworkStomach(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Stomach", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkStomach);
    }

    #endregion

    #region limbs

    public static void SpawnLeftArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftArm);
    }

    public static void SpawnRightArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightArm);
    }

    public static void SpawnLeftLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftLeg);
    }

    public static void SpawnRightLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightLeg);
    }

    public static void SpawnTorso(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Torso", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnTorso);
    }

    public static void SpawnHead(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Head", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnHead);
    }

    #endregion

    #region clockworklimbs

    public static void SpawnClockworkLeftArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftArm);
    }

    public static void SpawnClockworkRightArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightArm);
    }

    public static void SpawnClockworkLeftLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftLeg);
    }

    public static void SpawnClockworkRightLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightLeg);
    }

    public static void SpawnClockworkTorso(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Torso", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkTorso);
    }

    public static void SpawnClockworkHead(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Head", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkHead);
    }

    #endregion


}
