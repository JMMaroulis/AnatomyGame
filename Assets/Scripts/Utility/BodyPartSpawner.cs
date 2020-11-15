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

    //clockwork organ prefabs
    public GameObject clockworkheartPrefab;
    public GameObject clockworkleftLungPrefab;
    public GameObject clockworkrightLungPrefab;
    public GameObject clockworkbrainPrefab;
    public GameObject clockworkleftEyePrefab;
    public GameObject clockworkrightEyePrefab;
    public GameObject clockworkliverPrefab;
    public GameObject clockworkstomachPrefab;

    //limb prefabs
    public GameObject leftArmPrefab;
    public GameObject rightArmPrefab;
    public GameObject leftLegPrefab;
    public GameObject rightLegPrefab;
    public GameObject torsoPrefab;
    public GameObject headPrefab;

    //clockwork limb prefabs
    public GameObject clockworkleftArmPrefab;
    public GameObject clockworkrightArmPrefab;
    public GameObject clockworkleftLegPrefab;
    public GameObject clockworkrightLegPrefab;
    public GameObject clockworktorsoPrefab;
    public GameObject clockworkheadPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region organs

    public BodyPart SpawnHeart(string name)
    {
        GameObject bodyPart = Instantiate(heartPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftLung(string name)
    {
        GameObject bodyPart = Instantiate(leftLungPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightLung(string name)
    {
        GameObject bodyPart = Instantiate(rightLungPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnBrain(string name)
    {
        GameObject bodyPart = Instantiate(brainPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftEye(string name)
    {
        GameObject bodyPart = Instantiate(leftEyePrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightEye(string name)
    {
        GameObject bodyPart = Instantiate(rightEyePrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLiver(string name)
    {
        GameObject bodyPart = Instantiate(liverPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnStomach(string name)
    {
        GameObject bodyPart = Instantiate(stomachPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    #endregion

    #region clockworkorgans

    public BodyPart SpawnClockworkHeart(string name)
    {
        GameObject bodyPart = Instantiate(clockworkheartPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkLeftLung(string name)
    {
        GameObject bodyPart = Instantiate(clockworkleftLungPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkRightLung(string name)
    {
        GameObject bodyPart = Instantiate(clockworkrightLungPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkBrain(string name)
    {
        GameObject bodyPart = Instantiate(clockworkbrainPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkLeftEye(string name)
    {
        GameObject bodyPart = Instantiate(clockworkleftEyePrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkRightEye(string name)
    {
        GameObject bodyPart = Instantiate(clockworkrightEyePrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkLiver(string name)
    {
        GameObject bodyPart = Instantiate(clockworkliverPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkStomach(string name)
    {
        GameObject bodyPart = Instantiate(clockworkstomachPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    #endregion

    #region limbs

    public BodyPart SpawnLeftArm(string name)
    {
        GameObject bodyPart = Instantiate(leftArmPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightArm(string name)
    {
        GameObject bodyPart = Instantiate(rightArmPrefab);
        bodyPart.name = name;
        Faff(bodyPart);
        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnLeftLeg(string name)
    {
        GameObject bodyPart = Instantiate(leftLegPrefab);
        bodyPart.name = name;
        Faff(bodyPart);
        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnRightLeg(string name)
    {
        GameObject bodyPart = Instantiate(rightLegPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnTorso(string name)
    {
        GameObject bodyPart = Instantiate(torsoPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnHead(string name)
    {
        GameObject bodyPart = Instantiate(headPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    #endregion

    #region clockworklimbs

    public BodyPart SpawnClockworkLeftArm(string name)
    {
        GameObject bodyPart = Instantiate(clockworkleftArmPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkRightArm(string name)
    {
        GameObject bodyPart = Instantiate(clockworkrightArmPrefab);
        bodyPart.name = name;
        Faff(bodyPart);
        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkLeftLeg(string name)
    {
        GameObject bodyPart = Instantiate(clockworkleftLegPrefab);
        bodyPart.name = name;
        Faff(bodyPart);
        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkRightLeg(string name)
    {
        GameObject bodyPart = Instantiate(clockworkrightLegPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkTorso(string name)
    {
        GameObject bodyPart = Instantiate(clockworktorsoPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    public BodyPart SpawnClockworkHead(string name)
    {
        GameObject bodyPart = Instantiate(clockworkheadPrefab);
        bodyPart.name = name;
        Faff(bodyPart);

        return bodyPart.GetComponent<BodyPart>();
    }

    #endregion


    private void Faff(GameObject bodyPart)
    {
        //add bodypart to tracking lists
        FindObjectOfType<BodyPartStatusManager>().AddStatus(bodyPart.GetComponent<BodyPart>());
        FindObjectOfType<BodyPartManager>().bodyParts.Add(bodyPart.GetComponent<BodyPart>());


        if (bodyPart.GetComponent<Organ>() != null)
        {
            FindObjectOfType<BodyPartManager>().organs.Add(bodyPart.GetComponent<Organ>());
            GameObject.FindObjectOfType<BodyPartSelectorManager>().NewOrgan(bodyPart.GetComponent<Organ>());
        }

        else
        {
            foreach (Organ organ in bodyPart.GetComponent<BodyPart>().containedOrgans)
            {
                FindObjectOfType<BodyPartStatusManager>().AddStatus(organ.GetComponent<BodyPart>());
            }
            GameObject.FindObjectOfType<BodyPartSelectorManager>().NewBodyPart(bodyPart.GetComponent<BodyPart>());
        }

    }

}
