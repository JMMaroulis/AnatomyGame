using UnityEngine;

public class BodyPartSpawner : MonoBehaviour
{
    //organ prefabs
    public GameObject heartPrefab;
    public GameObject leftLungPrefab;
    public GameObject rightLungPrefab;
    public GameObject brainPrefab;
    public GameObject leftEyePrefab;
    public GameObject rightEyePrefab;
    public GameObject liverPrefab;
    public GameObject stomachPrefab;

    //limb prefabs
    public GameObject leftArmPrefab;
    public GameObject rightArmPrefab;
    public GameObject leftLegPrefab;
    public GameObject rightLegPrefab;
    public GameObject torsoPrefab;
    public GameObject headPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public BodyPart SpawnHeart(string name)
    {
        GameObject bodyPart = Instantiate(heartPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftLung(string name)
    {
        GameObject bodyPart = Instantiate(leftLungPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightLung(string name)
    {
        GameObject bodyPart = Instantiate(rightLungPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnBrain(string name)
    {
        GameObject bodyPart = Instantiate(brainPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftEye(string name)
    {
        GameObject bodyPart = Instantiate(leftEyePrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightEye(string name)
    {
        GameObject bodyPart = Instantiate(rightEyePrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLiver(string name)
    {
        GameObject bodyPart = Instantiate(liverPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnStomach(string name)
    {
        GameObject bodyPart = Instantiate(stomachPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan((Organ)bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftArm(string name)
    {
        GameObject bodyPart = Instantiate(leftArmPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightArm(string name)
    {
        GameObject bodyPart = Instantiate(rightArmPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftLeg(string name)
    {
        GameObject bodyPart = Instantiate(leftLegPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightLeg(string name)
    {
        GameObject bodyPart = Instantiate(rightLegPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnTorso(string name)
    {
        GameObject bodyPart = Instantiate(torsoPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());
        foreach (Organ organ in bodyPart.GetComponent<BodyPart>().containedOrgans)
        {
            FindObjectOfType<BodyPartStatusManager>().AddStatus(organ.GetComponent<BodyPart>());
        }

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnHead(string name)
    {
        GameObject bodyPart = Instantiate(headPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());
        foreach (Organ organ in bodyPart.GetComponent<BodyPart>().containedOrgans)
        {
            FindObjectOfType<BodyPartStatusManager>().AddStatus(organ.GetComponent<BodyPart>());
        }

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

}
