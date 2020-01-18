using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BodyPart
{
    bool isFunctioning { get; set; }
    float bloodRequiredToFunction { get; set; }
    float blood { get; set; }
    float bloodLossRate { get; set; }
    float bloodPumpRate { get; set; }
    List<BodyPart> connectedBodyParts {get; set ;}

    //move blood from bodypart to all connected bodyparts
    void PumpBlood(float pumpRate, float timeSinceLastPump);

    //lose blood from bodypart
    void LoseBlood(float lossRate, float timeSinceLastLoss);

    //update isFunctioning
    void CheckForFunctionality();
}
