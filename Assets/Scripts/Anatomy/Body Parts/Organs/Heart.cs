using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Heart : Organ
{

    //responsible for controlling blood pumping in all bodyparts
    //TODO: the amount of blood moving around should be capped by the amount of blood actually *in* the heart
    void PumpBloodMaster()
    {
        if (isFunctioning == false)
        {
            return;
        }
        //get parts bodypart (most likely the torso)
        //BodyPart parentBodyPart = this.transform.parent.GetComponent<BodyPart>();

        PumpBloodRecursive(this, new List<BodyPart>());
    }

    void PumpBloodRecursive(BodyPart currentBodyPart, List<BodyPart> alreadyPumped)
    {
        //pump blood for current bodypart, add to list to avoid repetition
        float heartEfficiency = efficiency;
        currentBodyPart.PumpBlood(heartEfficiency);
        alreadyPumped.Add(currentBodyPart);

        //shuffle to prevent weird behaviour from ordered looping
        List<BodyPart> allBodyParts = currentBodyPart.connectedBodyParts.Concat(currentBodyPart.containedOrgans).ToList<BodyPart>();
        List<int> bodypartPumpOrder = Enumerable.Range(0, allBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(bodypartPumpOrder);

        foreach (int bodypartPumpIndex in bodypartPumpOrder)
        {
            if (alreadyPumped.Contains(allBodyParts[bodypartPumpIndex]) == false)
            {
                PumpBloodRecursive(allBodyParts[bodypartPumpIndex], alreadyPumped);
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
            UpdateBodyPart();
            PumpBloodMaster();
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
