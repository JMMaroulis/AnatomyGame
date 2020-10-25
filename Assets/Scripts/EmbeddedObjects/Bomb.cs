using System.Collections.Generic;
using UnityEngine;

public class Bomb : EmbeddedObject
{

    public float timeElapsed;
    public float expiryTime;
    private bool hasDetonated;

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
    }
    
    public void Update()
    {
        if (clock.isTimePassing)
        {
            float deltaTime = Time.deltaTime * clock.globalTimeScalingFactor;
            //capping deltatime at 100ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

                BombTimer(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }

        }
    }

    private void BombTimer(float seconds)
    {
        timeElapsed += seconds;
        if (timeElapsed >= expiryTime)
        {
            Explode();
            Destroy(this);
        }
    }

    public void Explode()
    {
        if (!hasDetonated)
        {
            hasDetonated = true;
            if (!(parentBodyPart == null))
            {
                FindObjectOfType<TextLog>().NewLogEntry($"A bomb just exploded, taking the {parentBodyPart.name} with it!");
                parentBodyPart.SeverAllConnections(30);
                //I can't shake the feeling that invoking surgery to do this is not going to end particulary well
                //but, it has all the setup and cleanup required to make it work
                Actions_Surgery.DeleteBodyPart(parentBodyPart, 0, 0);
                Destroy(this);
                return;
            }

            FindObjectOfType<TextLog>().NewLogEntry("A bomb just exploded!");
            Destroy(this);
        }
    }

    public override string GenerateDescriptionObjectSpecific()
    {
        return $"\nTime until detonation: {expiryTime - timeElapsed} seconds.";
    }

}
