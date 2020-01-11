using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface BodyPart
{
    float blood { get; set; }
    float bloodLossRate { get; set; }
    float bloodPumpRate { get; set; }
    List<BodyPart> connectedBodyParts {get; set ;}

    void PumpBlood(float pumpRate, float timeSinceLastPump);

    void LoseBlood(float lossRate, float timeSinceLastLoss);
}
