using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : BodyPart
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

        foreach (BodyPart bodyPart in currentBodyPart.containedOrgans)
        {
            if (alreadyPumped.Contains(bodyPart) == false)
            {
                PumpBloodRecursive(bodyPart, alreadyPumped);
            }
        }
        foreach (BodyPart bodyPart in currentBodyPart.connectedBodyParts)
        {
            if (alreadyPumped.Contains(bodyPart) == false)
            {
                PumpBloodRecursive(bodyPart, alreadyPumped);
            }
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //connect body parts
        foreach (GameObject connectedBodyPartGameObject in connectedBodyPartsGameObjects)
        {
            //connectedBodyParts.Add(connectedBodyPartGameObject.GetComponent<BodyPart>());
        }

        //connect organs
        foreach (GameObject connectedOrganGameObject in containedOrgansGameObjects)
        {
            //containedOrgans.Add(connectedOrganGameObject.GetComponent<BodyPart>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateConnectedBodyParts();


        if (isTimePassing && this.transform.parent.GetComponent<BodyPart>().isTimePassing)
        {
            CheckForFunctionality();
            UpdateEfficiency();
            UpdateDamage();
            LoseBlood();
            ConsumeOxygen();
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
