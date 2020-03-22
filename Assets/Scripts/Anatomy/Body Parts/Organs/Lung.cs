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

    void AbsorbOxygen()
    {
        //apply the same scaling formula used to scale oxygen/blood pumping
        //pretend we're taking oxygen from an external bodypart with oxygen level of maxOxygen/2
        if (isFunctioning)
        {
            float tempOxygenAbsorbRate = Mathf.Max(Mathf.Min(oxygenAbsorptionRate * ((oxygenMax/4) / oxygen), oxygenAbsorptionRate * 5), oxygenAbsorptionRate * 0.2f) * efficiency;
            oxygen = Mathf.Min(oxygenMax, oxygen + (tempOxygenAbsorbRate * Time.deltaTime * timeScale * efficiency));
        }
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
            AbsorbOxygen();
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
