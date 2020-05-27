using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Stomach : Organ
{
    public List<Object> stomachContents; //TODO: Implement

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
                UpdateBodyPart(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }

        }
    }

    public void ProcessContents()
    {
        //TODO: Implement
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
