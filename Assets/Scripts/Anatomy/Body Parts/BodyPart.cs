using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface BodyPart
{
    float blood { get; set; }
    float bloodLossRate { get; set; }

    List<BodyPart> connectedBodyParts {get; set ;}

    void PumpBlood();

    void LoseBlood();
}
