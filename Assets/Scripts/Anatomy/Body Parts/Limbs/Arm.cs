using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Arm : BodyPart
{

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {

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
