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

        //randomising pump order, to prevent odd effects from fixed loops
        List<BodyPart> allBodyParts = bodyPartManager.bodyParts;
        List<int> bodypartOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartOrder);

        //for all bodyparts,
        //if this heart is connected to that bodypart,
        //pump blood
        foreach (int bodypartIndex in bodypartOrder)
        {
            if(allBodyParts[bodypartIndex].connectedHearts.Contains(this))
            {
                allBodyParts[bodypartIndex].PumpBlood(efficiency, deltaTime);
            }
        }
    }

    // Update is called once per frame
    public override void UpdateBodyPart(float deltaTime)
    {
        float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);
        PumpBloodMaster(tempDeltaTime);
        UpdateSelf(tempDeltaTime);
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
