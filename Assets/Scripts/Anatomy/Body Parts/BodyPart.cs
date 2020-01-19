using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BodyPart
{
    bool isFunctioning { get; set; }

    //blood stuff
    float bloodRequiredToFunction { get; set; }
    float blood { get; set; }
    float bloodLossRate { get; set; }
    float bloodPumpRate { get; set; }
    List<BodyPart> connectedBodyParts {get; set;}

    //oxygen stuff
    float oxygen { get; set; }
    float oxygenMax { get; set; }
    float oxygenRequired { get; set; }
    

    List<BodyPart> containedOrgans { get; set; }

    //move blood from bodypart to all connected bodyparts
    void PumpBlood();

    //lose blood from bodypart
    void LoseBlood();

    //use oxygen from supply
    void ConsumeOxygen();

    //update isFunctioning
    void CheckForFunctionality();
}
