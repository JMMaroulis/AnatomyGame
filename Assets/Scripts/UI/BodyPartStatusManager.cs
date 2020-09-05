using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BodyPartStatusManager : MonoBehaviour
{
    public GameObject statusPrefab;
    public GameObject statusPanel;
    private BodyPartManager bodyPartManager;

    // Start is called before the first frame update
    void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        UpdateStatusCollection();
    }

    void UpdateStatusCollection()
    {
        //kill all children of this object
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //add everything in a sensible order
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart is Head)
            {
                AddStatusIncludingContainedOrgans(bodyPart);
            }
        }
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart is Torso)
            {
                AddStatusIncludingContainedOrgans(bodyPart);
            }
        }
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart is LeftArm || bodyPart is RightArm)
            {
                AddStatusIncludingContainedOrgans(bodyPart);
            }
        }
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart is LeftLeg || bodyPart is RightLeg)
            {
                AddStatusIncludingContainedOrgans(bodyPart);
            }
        }
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if ((bodyPart is Organ) && bodyPart.connectedBodyParts.Count == 0)
            {
                AddStatus(bodyPart);
            }
        }

    }

    public void AddStatusIncludingContainedOrgans(BodyPart bodyPart)
    {
        AddStatus(bodyPart);
        foreach (BodyPart organ in bodyPart.containedOrgans)
        {
            AddStatus(organ);
        }
    }

    public void AddStatus(BodyPart bodyPart)
    {
        GameObject statusGameObject = Instantiate(statusPrefab, statusPanel.transform);
        BodyPartStatus status = statusGameObject.GetComponent<BodyPartStatus>();
        status.name = bodyPart.name;

        if (bodyPart is Organ)
        {
             status.partName.text = "  " + bodyPart.name;
        }
        else
        {
            status.partName.text = bodyPart.name;
        }

        BloodBar bloodBar = status.bloodBar;
        OxygenBar oxygenBar = status.oxygenBar;
        HealthBar healthBar = status.healthBar;

        bloodBar.bodyPart = bodyPart;
        bloodBar.MinMax();
        oxygenBar.bodyPart = bodyPart;
        oxygenBar.MinMax();
        healthBar.bodyPart = bodyPart;
        healthBar.MinMax();
    }

    public void RemoveStatus(BodyPart bodyPart)
    {
        foreach (Transform child in transform)
        {
            if (child.name == bodyPart.name)
            {
                GameObject.Destroy(child.gameObject);
                return;
            }
        }
    }
}
