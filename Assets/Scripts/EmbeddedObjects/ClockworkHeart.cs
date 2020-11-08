using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClockworkHeart : EmbeddedObject
{

    private BodyPartManager bodyPartManager;

    public void Awake()
    {
        /*
        //default to being allowed in anything; to be overriden in inheriting classes if necessary.
        allowedParentBodyParts = new Dictionary<string, bool>{
            { "LeftArm"   , true },
            { "RightArm"  , true },
            { "LeftLeg"   , true },
            { "RightLeg"  , true },
            { "Head"      , true },
            { "Torso"     , true },
            { "Brain"     , true },
            { "LeftEye"   , true },
            { "RightEye"  , true },
            { "Heart"     , true },
            { "Liver"     , true },
            { "LeftLung"  , true },
            { "RightLung" , true },
            { "Stomach"   , true }
        };
        */

        bodyPartManager = FindObjectOfType<BodyPartManager>();
    }

    public void Update()
    {
        if (parentBodyPart != null)
        {
            if (parentBodyPart.isTimePassing)
            {
                float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
                //capping deltatime at 100ms to stop inaccuracies
                while (deltaTime > 0.0f)
                {
                    float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

                    PumpBloodMaster(tempDeltaTime);

                    deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
                }
            }
        }

    }

    public override string GenerateDescriptionObjectSpecific()
    {
        return "";
        //return $"\nTime until detonation: {expiryTime - timeElapsed} seconds.";
    }

    //stolen from Heart.cs, modified slightly to work as an embedded object inside a bodypart rather than a bodypart in and of itself
    //forcing efficiency at 0.7f, ignoring whether or not the bodypart is functioning
    void PumpBloodMaster(float deltaTime)
    {
        //starting from a random bodypart that is connected to this charm:
        List<BodyPart> allBodyParts = bodyPartManager.bodyParts;
        List<int> bodypartOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartOrder);

        foreach (int bodypartIndex in bodypartOrder)
        {
            if (allBodyParts[bodypartIndex].IsConnectedToBodyPartStarter(parentBodyPart))
            {
                allBodyParts[bodypartIndex].PumpBlood(0.7f, deltaTime);
            }
        }
    }

}
