using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface BodyPart
{
    int blood { get; set; }
    int bloodLossRate { get; set; }

    List<BodyPart> connectedBodyParts {get; set ;}

    void pumpBlood();
}
