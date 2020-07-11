using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BodyPartSpawner : MonoBehaviour
{
    //organ prefabs
    public GameObject heartPrefab;
    public GameObject lungPrefab;
    public GameObject brainPrefab;
    public GameObject eyePrefab;
    public GameObject liverPrefab;
    public GameObject stomachPrefab;

    //limb prefabs
    public GameObject armPrefab;
    public GameObject legPrefab;
    public GameObject torsoPrefab;
    public GameObject headPrefab;

    //selector prefab
    public GameObject selectorPrefab;

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

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLung(string name)
    {
        GameObject bodyPart = Instantiate(lungPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnBrain(string name)
    {
        GameObject bodyPart = Instantiate(brainPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnEye(string name)
    {
        GameObject bodyPart = Instantiate(eyePrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLiver(string name)
    {
        GameObject bodyPart = Instantiate(liverPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnStomach(string name)
    {
        GameObject bodyPart = Instantiate(stomachPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        //GameObject selector = Instantiate(selectorPrefab, GameObject.FindGameObjectWithTag("BodySelectors").transform);
        //selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart.GetComponent<BodyPart>();

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnArm(string name)
    {
        GameObject bodyPart = Instantiate(armPrefab);
        bodyPart.name = name;
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());

        GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeg(string name)
    {
        GameObject bodyPart = Instantiate(legPrefab);
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
