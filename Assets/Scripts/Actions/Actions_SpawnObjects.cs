using System.Collections;
using UnityEngine;

public static class Actions_SpawnObjects
{

    public static void SpawnClockworkHeart(float seconds, int goldCost)
    {

        StaticCoroutine.Start(SpawnClockworkHeartCoroutine(seconds, goldCost));
    }

    public static IEnumerator SpawnClockworkHeartCoroutine(float seconds, int goldCost)
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
            Object.FindObjectOfType<ObjectSpawner>().SpawnClockworkHeart("Clockwork Heart");
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

}