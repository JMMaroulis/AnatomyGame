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

        float deltaTime = Time.deltaTime;
        //capping deltatime at 1ms to stop inaccuracies
        while (deltaTime > 0.0f)
        {
            float tempDeltaTime = Mathf.Min(deltaTime, 0.2f);

            //overriding bodypart update, to avoid brain damage spiralling the efficiency
            if (isTimePassing)
            {

                ApplyDrugs(tempDeltaTime);
                UpdateDamage(tempDeltaTime);
                CheckForFunctionality();
                UpdateEfficiency();
                LoseBlood(tempDeltaTime);
                ConsumeOxygen(tempDeltaTime);

            }

            deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
        }

    }

    public void SeverConnection(GameObject connectedBodyPart)
    {
        throw new System.NotImplementedException();
    }

    //brain needs a version of this that doesn't multiply by it's own efficiency, else it'ss spiral to 0 as soon as it goes down from 1 at all
    new public void UpdateEfficiency()
    {
        float damageRatio = 1 - (damage / damageMax); //1 good, 0 bad
        float oxygenRatio = Mathf.Min((oxygen / oxygenRequired), 1); //1 good, 0 bad
        efficiency = damageRatio * oxygenRatio;
    }

    new public void SeverAllConnections()
    {
        throw new System.NotImplementedException();
    }

}
