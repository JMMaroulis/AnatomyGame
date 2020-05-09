using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BloodRegenCharm : Charm
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

                RegenBlood(tempDeltaTime);
                CharmTimer(tempDeltaTime);

                deltaTime = Mathf.Max(0.0f, deltaTime - 0.2f);
            }

        }
    }

    //stolen from Lung.cs, modified slightly to work as an attachment to a bodypart rather than a bodypart in and of itself
    void RegenBlood(float deltaTime)
    {
        BodyPart bodyPart = this.gameObject.GetComponent<BodyPart>();
        bodyPart.blood = Mathf.Min(bodyPart.bloodMax, bodyPart.blood + deltaTime * 30.0f);
    }

    new void CharmTimer(float seconds)
    {
        timeElapsed += seconds;
        if (timeElapsed >= expiryTime)
        {
            this.gameObject.GetComponent<BodyPart>().enabled = true;
            Destroy(this);
        }
    }


}
