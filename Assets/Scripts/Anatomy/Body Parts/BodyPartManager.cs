using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    private Clock clock;
    public List<BodyPart> bodyParts;
    public List<Organ> organs;

    public void Start()
    {
        clock = FindObjectOfType<Clock>();
        bodyParts = FindObjectsOfType<BodyPart>().ToList();
        organs = FindObjectsOfType<Organ>().ToList();
    }

    void Update()
    {
        if (clock.isTimePassing)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

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
            watch.Stop();
            Debug.Log($"Update all bodyparts: {watch.ElapsedMilliseconds}");
        }
    }


}
