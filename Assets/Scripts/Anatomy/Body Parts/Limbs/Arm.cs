using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Arm : BodyPart
{

    // Start is called before the first frame update
    void Start()
    {
        clock = FindObjectOfType<Clock>();
    }

    void Update()
    {

        float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
        //capping deltatime at 1ms to stop inaccuracies
        while (deltaTime > 0.0f)
        {
            float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

            UpdateBodyPart(tempDeltaTime);

            deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
        }
    }

}
