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

        PumpBloodRecursive(this, new List<BodyPart>(), deltaTime);
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTimePassing)
        {

            float deltaTime = Time.deltaTime;
            //capping deltatime at 100ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.1f);
                PumpBloodMaster(tempDeltaTime);
                UpdateBodyPart(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.1f);
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
