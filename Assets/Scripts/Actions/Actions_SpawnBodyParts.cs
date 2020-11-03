using System.Collections;
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

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHeart("Heart");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }

        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnLeftLung(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLeftLungCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLeftLungCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeftLung("Left Lung");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }

        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnRightLung(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnRightLungCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnRightLungCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnRightLung("Right Lung");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
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
        
        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnBrain("Brain");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnLeftEye(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLeftEyeCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLeftEyeCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeftEye("Left Eye");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnRightEye(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnRightEyeCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnRightEyeCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnRightEye("Right Eye");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
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

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLiver("Liver");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
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

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnStomach("Stomach");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnOrganActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    #endregion

    #region bodyparts

    public static void SpawnLeftArm(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLeftArmCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLeftArmCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeftArm("Left Arm");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnRightArm(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnRightArmCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnRightArmCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnRightArm("Right Arm");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }


    public static void SpawnLeftLeg(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnLeftLegCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnLeftLegCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnLeftLeg("Left Leg");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    public static void SpawnRightLeg(float seconds, int goldCost)
    {
        StaticCoroutine.Start(SpawnRightLegCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnRightLegCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<BodyPartSpawner>().SpawnRightLeg("Right Leg");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
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

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnTorso("Torso");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
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

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            Object.FindObjectOfType<BodyPartSpawner>().SpawnHead("Head");
            MonoBehaviour.FindObjectOfType<ActionTracker>().spawn_spawned += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSpawnBodyPartActionOptions();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
        MonoBehaviour.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    #endregion

}
