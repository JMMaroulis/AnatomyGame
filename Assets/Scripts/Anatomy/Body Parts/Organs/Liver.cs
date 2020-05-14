﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liver : Organ
{

    public float bloodProcessingRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    void BloodProcessing(float deltaTime)
    {
        this.healthPotion     -= bloodProcessingRate * deltaTime;
        this.antidote         -= bloodProcessingRate * deltaTime;
        this.slowPoison       -= bloodProcessingRate * deltaTime;
        this.stasisPotion     -= bloodProcessingRate * deltaTime;
        this.coagulantPotion  -= bloodProcessingRate * deltaTime;
        this.hastePotion      -= bloodProcessingRate * deltaTime;
    }


    // Update is called once per frame
    void Update()
    {
        if (isTimePassing)
        {
            float deltaTime = Time.deltaTime;
            //capping deltatime at 1ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

                BloodProcessing(tempDeltaTime);
                UpdateBodyPart(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }
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