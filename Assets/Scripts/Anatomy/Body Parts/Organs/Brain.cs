using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : Organ
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isTimePassing)
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
