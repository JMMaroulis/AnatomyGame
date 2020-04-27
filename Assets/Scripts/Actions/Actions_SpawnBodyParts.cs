using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_SpawnBodyParts
{

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

}
