using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeartCharm : Charm
{

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<BodyPart>().isTimePassing)
        {
            float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
            //capping deltatime at 100ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

                PumpBloodMaster(tempDeltaTime);
                CharmTimer(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }

        }
    }

    //stolen from Heart.cs, modified slightly to work as an attachment to a bodypart rather than a bodypart in and of itself
    //forcing efficiency at 1.0f, ignoring whether or not the bodypart is functioning
    void PumpBloodMaster(float deltaTime)
    {
        //starting from a random bodypart that is connected to this charm:
        BodyPart[] allBodyParts = FindObjectsOfType<BodyPart>();
        List<int> bodypartOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartOrder);

        foreach (int bodypartIndex in bodypartOrder)
        {
            if (allBodyParts[bodypartIndex].IsConnectedToBodyPartStarter(this.gameObject.GetComponent<BodyPart>()))
            {
                allBodyParts[bodypartIndex].PumpBlood(1.0f, deltaTime);
            }
        }
    }
}
