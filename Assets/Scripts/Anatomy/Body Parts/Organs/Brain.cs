using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : BodyPart
{

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
