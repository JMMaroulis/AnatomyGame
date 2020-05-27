using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Heart : Organ
{

    //responsible for controlling blood pumping in all bodyparts
    //TODO: the amount of blood moving around should be capped by the amount of blood actually *in* the heart
    void PumpBloodMaster(float deltaTime)
    {
        if (isFunctioning == false)
        {
            return;
        }
        //get parts bodypart (most likely the torso)
        //BodyPart parentBodyPart = this.transform.parent.GetComponent<BodyPart>();

        //starting from a random bodypart that is connected to this heart:
        BodyPart[] allBodyParts = FindObjectsOfType<BodyPart>();
        List<int> bodypartOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartOrder);

        //TODO: remove having to do the full graph search to find this heart for every organ somehow?
        //a bool flat that updates on connection/severing of a part could work
        //but that wouldn't really be compatible with having multiple hearts
        foreach (int bodypartIndex in bodypartOrder)
        {
            //if (allBodyParts[bodypartIndex].IsConnectedToBodyPartStarter(this))
            if(allBodyParts[bodypartIndex].connectedHearts.Contains(this))
            {
                //PumpBloodRecursive(allBodyParts[bodypartIndex], new List<BodyPart>(), deltaTime);
                //break;
                allBodyParts[bodypartIndex].PumpBlood(efficiency, deltaTime);
            }
        }
    }

    void PumpBloodRecursive(BodyPart currentBodyPart, List<BodyPart> alreadyPumped, float deltaTime)
    {
        //pump blood for current bodypart, add to list to avoid repetition
        float heartEfficiency = efficiency;
        currentBodyPart.PumpBlood(heartEfficiency, deltaTime);
        alreadyPumped.Add(currentBodyPart);

        //shuffle to prevent weird behaviour from ordered looping
        List<BodyPart> allBodyParts = currentBodyPart.connectedBodyParts.Concat(currentBodyPart.containedOrgans).ToList<BodyPart>();
        List<int> bodypartPumpOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartPumpOrder);

        foreach (int bodypartPumpIndex in bodypartPumpOrder)
        {
            if (alreadyPumped.Contains(allBodyParts[bodypartPumpIndex]) == false)
            {
                PumpBloodRecursive(allBodyParts[bodypartPumpIndex], alreadyPumped, deltaTime);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimePassing)
        {

            float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
            //capping deltatime at 100ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);
                PumpBloodMaster(tempDeltaTime);
                UpdateBodyPart(tempDeltaTime);

                deltaTime -= tempDeltaTime;
            }

        }
    }

    public void SeverConnection(GameObject connectedBodyPart)
    {
        throw new System.NotImplementedException();
    }

    public void SeverAllConnections()
    {
        throw new System.NotImplementedException();
    }

}
