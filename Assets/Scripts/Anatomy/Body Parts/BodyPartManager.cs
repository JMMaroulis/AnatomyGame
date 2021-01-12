using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    private Clock clock;
    public List<BodyPart> bodyParts;
    public List<Organ> organs;

    public void Awake()
    {
        clock = FindObjectOfType<Clock>();
        bodyParts = FindObjectsOfType<BodyPart>().ToList();
        organs = FindObjectsOfType<Organ>().ToList();
    }

    public void OrderLists()
    {
        List<BodyPart> x = bodyParts.OrderBy(bodyPart => bodyPart.index).ToList();
        List<Organ> y = organs.OrderBy(organ => organ.index).ToList();
        bodyParts = x;
        organs = y;
    }

    void Update()
    {
        if (clock.isTimePassing)
        {
            float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);
                foreach (BodyPart bodyPart in bodyParts)
                {
                    if (bodyPart.enabled)
                    {
                        bodyPart.UpdateBodyPart(tempDeltaTime);
                    }
                }
                deltaTime -= tempDeltaTime;
            }
        }
    }


}
