using System;
using System.Collections;
using UnityEngine;

public static class Actions_SpawnBodyParts
{
    public static void SpawnBodyPartTemplate(float seconds, int goldCost, string partName, Func<string, BodyPart> spawnMethod, int menuReset)
    {
        StaticCoroutine.Start(SpawnBodyPartCoroutineTemplate(seconds, goldCost, partName, spawnMethod, menuReset));
    }

    public static IEnumerator SpawnBodyPartCoroutineTemplate(float seconds, int goldCost, string partName, Func<string, BodyPart> spawnMethod, int menuReset)
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
            SpawnBodyPartProcessTemplate(goldCost, partName, spawnMethod, menuReset);
        }
        else
        {
            clock.actionCancelFlag = false;
        }

        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnBodyPartProcessTemplate(int goldCost, string partName, Func<string, BodyPart> spawnMethod, int menuReset)
    {
        spawnMethod(partName);
        MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();

        switch (menuReset)
        {
            case 0:
                MonoBehaviour.FindObjectOfType<ButtonActions>().SelectSpawnOrganActionOptions();
                break;

            case 1:
                MonoBehaviour.FindObjectOfType<ButtonActions>().SelectSpawnBodyPartActionOptions();
                break;

            case 2:
                MonoBehaviour.FindObjectOfType<ButtonActions>().SelectSpawnClockworkOrganActionOptions();
                break;

            case 3:
                MonoBehaviour.FindObjectOfType<ButtonActions>().SelectSpawnClockworkLimbActionOptions();
                break;

            default:
                break;
        }
    }

    #region organs

    public static void SpawnHeart(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Heart", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnHeart, 0);
    }

    public static void SpawnLeftLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftLung, 0);
    }

    public static void SpawnRightLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightLung, 0);
    }

    public static void SpawnBrain(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Brain", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnBrain, 0);
    }

    public static void SpawnLeftEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftEye, 0);
    }

    public static void SpawnRightEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightEye, 0);
    }

    public static void SpawnLiver(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Liver", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLiver, 0);
    }

    public static void SpawnStomach(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Stomach", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnStomach, 0);
    }

    #endregion

    #region clockworkOrgans

    public static void SpawnClockworkHeart(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Heart", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkHeart, 2);
    }

    public static void SpawnClockworkLeftLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftLung, 2);
    }

    public static void SpawnClockworkRightLung(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Lung", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightLung, 2);
    }

    public static void SpawnClockworkBrain(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Brain", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkBrain, 2);
    }

    public static void SpawnClockworkLeftEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftEye, 2);
    }

    public static void SpawnClockworkRightEye(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Eye", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightEye, 2);
    }

    public static void SpawnClockworkLiver(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Liver", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLiver, 2);
    }

    public static void SpawnClockworkStomach(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Stomach", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkStomach, 2);
    }

    #endregion

    #region limbs

    public static void SpawnLeftArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftArm, 1);
    }

    public static void SpawnRightArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightArm, 1);
    }

    public static void SpawnLeftLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Left Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnLeftLeg, 1);
    }

    public static void SpawnRightLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Right Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnRightLeg, 1);
    }

    public static void SpawnTorso(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Torso", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnTorso, 1);
    }

    public static void SpawnHead(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Head", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnHead, 1);
    }

    #endregion

    #region clockworklimbs

    public static void SpawnClockworkLeftArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftArm, 3);
    }

    public static void SpawnClockworkRightArm(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Arm", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightArm, 3);
    }

    public static void SpawnClockworkLeftLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Left Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkLeftLeg, 3);
    }

    public static void SpawnClockworkRightLeg(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Right Leg", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkRightLeg, 3);
    }

    public static void SpawnClockworkTorso(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Torso", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkTorso, 3);
    }

    public static void SpawnClockworkHead(float seconds, int goldCost)
    {
        SpawnBodyPartTemplate(seconds, goldCost, "Clockwork Head", MonoBehaviour.FindObjectOfType<BodyPartSpawner>().SpawnClockworkHead, 3);
    }

    #endregion


}
