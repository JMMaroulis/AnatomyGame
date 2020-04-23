using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LungCharm : Charm
{

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<BodyPart>().isTimePassing)
        {
            float deltaTime = Time.deltaTime;
            //capping deltatime at 100ms to stop inaccuracies
            while (deltaTime > 0.0f)
            {
                float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

                AbsorbOxygen(tempDeltaTime);
                CharmTimer(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }

        }
    }

    //stolen from Lung.cs, modified slightly to work as an attachment to a bodypart rather than a bodypart in and of itself
    void AbsorbOxygen(float deltaTime)
    {
        BodyPart parentBodyPart = this.gameObject.GetComponent<BodyPart>();

        deltaTime *= parentBodyPart.timeScale;
        float oxygenAbsorptionRate = 150.0f;
        float oxygenMax = parentBodyPart.oxygenMax;
        float oxygen = parentBodyPart.oxygen;
        float efficiency = parentBodyPart.efficiency;
        float blood = parentBodyPart.blood;

        //apply the same scaling formula used to scale oxygen/blood pumping
        //simulate max blood oxygen saturation of 1:1 by capping oxygen at blood level
        //pretend we're taking oxygen from an external bodypart with oxygen level of maxOxygen/2
        if (parentBodyPart.isFunctioning)
        {
            float tempOxygenAbsorbRate = Mathf.Max(Mathf.Min(oxygenAbsorptionRate * ((oxygenMax / 4) / oxygen), oxygenAbsorptionRate * 5), oxygenAbsorptionRate * 0.2f) * efficiency;
            parentBodyPart.oxygen = Mathf.Min(blood, Mathf.Min(oxygenMax, oxygen + (tempOxygenAbsorbRate * deltaTime * efficiency)));
        }
    }
}
