using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : Organ
{

    public float oxygenAbsorptionRate;

    // Start is called before the first frame update
    void Start()
    {

    }

    void AbsorbOxygen(float deltaTime)
    {
        //apply the same scaling formula used to scale oxygen/blood pumping
        //simulate max blood oxygen saturation of 1:1 by capping oxygen at blood level
        //pretend we're taking oxygen from an external bodypart with oxygen level of maxOxygen/2
        if (isFunctioning)
        {
            float tempOxygenAbsorbRate = Mathf.Max(Mathf.Min(oxygenAbsorptionRate * ((oxygenMax / 4) / oxygen), oxygenAbsorptionRate * 5), oxygenAbsorptionRate * 0.2f) * efficiency;
            oxygen = Mathf.Min(blood, Mathf.Min(oxygenMax, oxygen + (tempOxygenAbsorbRate * deltaTime * efficiency)));
        }
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
                float tempDeltaTime = Mathf.Min(deltaTime, 0.1f);

                AbsorbOxygen(tempDeltaTime);
                UpdateBodyPart(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.1f);
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
