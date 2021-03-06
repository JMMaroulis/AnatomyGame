﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalProcedureGenerator : MonoBehaviour
{
    public GameObject body;
    public TextLog textLog;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;
    private UnlockTracker unlockTracker;

    private BodyPartManager bodyPartManager;

    //requested procedure logs
    public bool ClockworkHeartRequest;

    // Start is called before the first frame update
    void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
        PopulateBodyPartsList();
        PopulateOrgansList();
    }

    public void RequestedProcedures(int numberOfProcedures)
    {
        int i = 0;
        while(i < numberOfProcedures)
        {
            int procedureNumber = Random.Range(0, 4);

            switch (procedureNumber)
            {
                case 0:
                    if (ClockworkHeartReplacement()) { i += 1; }
                    break;

                case 1:
                    if (RandomOrganReplacement()) { i += 1; }
                    break;

                case 2:
                    if (RandomLimbReplacement()) { i += 1; }
                    break;

                case 3:
                    if (RandomLimbAmputation()) { i += 1; }
                    break;

                default:
                    break;
            }

        }

    }

    private bool ClockworkHeartReplacement()
    {
        if ((!unlockTracker.spawn && !unlockTracker.spawn_clock) || (!unlockTracker.charms_heart) || ClockworkHeartRequest)
        {
            return false;
        }
        Debug.Log("Hearts require replacing with clockwork");
        textLog.NewLogEntry("The patient requests that their heart(s) be clockwork, not biological.");
        ClockworkHeartRequest = true;
        return true;
    }

    private bool RandomOrganReplacement()
    {
        int n = 0;
        Organ organ = RandomOrgan();
        bool x = false;
        x = x || (organ is Brain && (!unlockTracker.charms_heart || !unlockTracker.charms_lung));
        x = x || (organ is Heart && !unlockTracker.charms_heart);
        x = x || (organ is Lung && !unlockTracker.charms_lung);
        x = x || organ.requiresReplacing;
        while (x && n < 5)
        {
            organ = RandomOrgan();
            n += 1;
        }
        if (n > 5 || (!unlockTracker.spawn && !unlockTracker.spawn_clock) || organ.requiresReplacing)
        {
            return false;
        }
        Debug.Log($"{organ.name} replacement");
        textLog.NewLogEntry($"The patient requires a {organ.name} replacement.");
        organ.requiresReplacing = true;
        return true;
    }

    private bool RandomLimbReplacement()
    {
        int n = 0;
        BodyPart bodyPart = RandomLimb();
        bool x = false;
        x = x || bodyPart.requiresReplacing;
        x = x || bodyPart.requiresAmputation;
        x = x || bodyPart is Head;
        x = x || bodyPart is Torso;
        while (x && n < 5)
        {
            bodyPart = RandomLimb();
            n += 1;
        }
        if (n > 5 || (!unlockTracker.spawn && !unlockTracker.spawn_clock) || bodyPart.requiresReplacing || bodyPart.requiresAmputation || bodyPart is Head || bodyPart is Torso)
        {
            return false;
        }
        Debug.Log($"{bodyPart.name} replacement");
        textLog.NewLogEntry($"The patient requires a {bodyPart.name} replacement.");
        bodyPart.requiresReplacing = true;
        return true;
    }

    private bool RandomLimbAmputation()
    {
        int n = 0;
        BodyPart bodyPart = RandomLimb();
        bool x = false;
        x = x || bodyPart.requiresReplacing;
        x = x || bodyPart.requiresAmputation;
        x = x || bodyPart is Head;
        x = x || bodyPart is Torso;
        while (x && n < 5)
        {
            bodyPart = RandomLimb();
            n += 1;
        }
        if (n > 5 || bodyPart.requiresReplacing || bodyPart.requiresAmputation || bodyPart is Head || bodyPart is Torso)
        {
            return false;
        }
        Debug.Log($"{bodyPart.name} amputation");
        textLog.NewLogEntry($"The patient requires a {bodyPart.name} amputation.");

        bodyPart.requiresAmputation = true;

        //this bit is *horrifyingly* long-winded,
        //but I don/t know how to do it less explicitly
        if (bodyPart is LeftArm)
        {
            foreach(BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
            {
                connectedBodyPart.maxLeftArms -= 1;
            }
        }
        if (bodyPart is RightArm)
        {
            foreach (BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
            {
                connectedBodyPart.maxRightArms -= 1;
            }
        }
        if (bodyPart is LeftLeg)
        {
            foreach (BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
            {
                connectedBodyPart.maxLeftLegs -= 1;
            }
        }
        if (bodyPart is RightLeg)
        {
            foreach (BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
            {
                connectedBodyPart.maxRightLegs -= 1;
            }
        }

        return true;
    }

    public Organ RandomOrgan()
    {
        Organ organ = bodyPartManager.organs[Random.Range(0, bodyPartManager.organs.Count)];
        return organ;
    }

    public BodyPart RandomLimb()
    {
        BodyPart limb = bodyPartManager.bodyParts[Random.Range(0, bodyPartManager.bodyParts.Count)];
        while (limb is Organ)
        {
            limb = bodyPartManager.bodyParts[Random.Range(0, bodyPartManager.bodyParts.Count)];
        }
        return limb;
    }


    void PopulateBodyPartsList()
    {

        bodyParts = new List<BodyPart>();

        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyParts.Add(body.transform.GetChild(i).GetComponent<BodyPart>());
        }

    }

    void PopulateOrgansList()
    {
        organs = new List<Organ>();

        //get organs from bodyparts list
        foreach(BodyPart bodyPart in bodyParts)
        {
            foreach (Organ organ in bodyPart.containedOrgans)
            {
                organs.Add(organ);
            }
        }

    }
}
