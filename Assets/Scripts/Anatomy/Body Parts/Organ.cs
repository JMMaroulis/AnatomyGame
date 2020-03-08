using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Organ : BodyPart
{
    public GameObject parentBodyPartObject;
    public BodyPart parentBodyPart;

    public void RemoveFromBodyPart()
    {
        parentBodyPart.SeverConnectionOutgoing(this.gameObject, 10.0f);

        parentBodyPartObject = null;
        parentBodyPart = null;

        bloodLossRate += 10.0f;
    }
}
