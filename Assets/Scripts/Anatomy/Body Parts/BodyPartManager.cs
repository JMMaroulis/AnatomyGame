using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    private Clock clock;
    public void Start()
    {
        clock = FindObjectOfType<Clock>();
    }

    void Update()
    {
        List<BodyPart> bodyParts = FindObjectsOfType<BodyPart>().ToList();
        if (clock.isTimePassing)
        {
            float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);
                foreach (BodyPart bodyPart in bodyParts)
                {
                    bodyPart.UpdateBodyPart(tempDeltaTime);
                }
                deltaTime -= tempDeltaTime;
            }
        }
    }


}
