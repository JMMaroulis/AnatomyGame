using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Actions_Wait
{

    public static void WaitSeconds(float seconds)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);

        StaticCoroutine.Start(WaitSecondsCoroutine(seconds));
    }

    public static IEnumerator WaitSecondsCoroutine(float seconds)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        
        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();

        clock.actionCancelFlag = false;
    }

    public static void WaitOneHour()
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(3600);

        StaticCoroutine.Start(WaitOneHourCoroutine());
    }

    public static IEnumerator WaitOneHourCoroutine()
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();


        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            GameObject.FindObjectOfType<LifeMonitor>().VictoryCheck();
        }

        clock.actionCancelFlag = false;
    }

}
