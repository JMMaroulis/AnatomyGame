using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leg : BodyPart
{

    // Start is called before the first frame update
    void Start()
    {
        //connect body parts
        foreach (GameObject connectedBodyPartGameObject in connectedBodyPartsGameObjects)
        {
            connectedBodyParts.Add(connectedBodyPartGameObject.GetComponent<BodyPart>());
        }

        //connect organs
        foreach (GameObject connectedOrganGameObject in containedOrgansGameObjects)
        {
            containedOrgans.Add(connectedOrganGameObject.GetComponent<BodyPart>());
        }
    }

    public float tempUpdate = 0;



    void Update()
    {
        UpdateConnectedBodyParts();

        if (isTimePassing)
        {
            UpdateDamage();
            CheckForFunctionality();
            UpdateEfficiency();
            LoseBlood();
            ConsumeOxygen();
        }

    }

}
