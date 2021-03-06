﻿using System.Collections.Generic;
using UnityEngine;

public class EmbeddedObject : MonoBehaviour
{
    public BodyPart parentBodyPart;
    public Clock clock;

    //default to being allowed in anything; to be overriden in inheriting classes if necessary.
    public bool allowedInAllBodyParts = true;
    public Dictionary<string, bool> allowedParentBodyParts = new Dictionary<string, bool>{
        { "LeftArm"   , true },
        { "RightArm"  , true },
        { "LeftLeg"   , true },
        { "RightLeg"  , true },
        { "Head"      , true },
        { "Torso"     , true },
        { "Brain"     , true },
        { "LeftEye"   , true },
        { "RightEye"  , true },
        { "Heart"     , true },
        { "Liver"     , true },
        { "LeftLung"  , true },
        { "RightLung" , true },
        { "Stomach"   , true }
    };

    // Start is called before the first frame update
    void Start()
    {
        clock = FindObjectOfType<Clock>();
    }

    public bool Embed(BodyPart bodypart)
    {
        if (EmbedValidity(bodypart))
        {
            parentBodyPart = bodypart;
            bodypart.embeddedObjects.Add(this);
            this.gameObject.transform.SetParent(bodypart.transform);
            return true;
        }

        return false;
    }

    public bool EmbedValidity(BodyPart bodypart)
    {
        return allowedInAllBodyParts || allowedParentBodyParts[bodypart.GetType().ToString()];
    }

    public void Remove()
    {
        parentBodyPart.embeddedObjects.Remove(this);
        parentBodyPart = null;
    }

    public string GenerateDescription()
    {
        string description = "";

        description += $"Examining {this.gameObject.name}:";

        if (parentBodyPart is null)
        {
            description += "\nLocation: External";
        }
        else
        {
            description += $"\nLocation: Embedded in {parentBodyPart.gameObject.name}";
        }

        description += GenerateDescriptionObjectSpecific();

        return description;
    }

    public virtual string GenerateDescriptionObjectSpecific()
    {
        return "";
    }

}
