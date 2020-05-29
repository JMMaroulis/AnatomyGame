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

        
        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();

        clock.actionCancelFlag = false;
    }


}
