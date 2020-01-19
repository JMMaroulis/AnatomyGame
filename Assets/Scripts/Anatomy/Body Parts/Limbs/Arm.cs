using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Arm : MonoBehaviour, BodyPart
{
    bool BodyPart.isFunctioning { get => isFunctioning; set => isFunctioning = value; }
    public bool isFunctioning = true;

    float BodyPart.blood { get => blood; set => blood = value; }
    public float blood;

    float BodyPart.bloodRequiredToFunction { get => bloodRequiredToFunction; set => bloodRequiredToFunction = value; }
    public float bloodRequiredToFunction;

    float BodyPart.bloodPumpRate { get => bloodPumpRate; set => bloodPumpRate = value; }
    public float bloodPumpRate;

    float BodyPart.bloodLossRate { get => bloodLossRate; set => bloodLossRate = value; }
    public float bloodLossRate;


    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    List<BodyPart> BodyPart.containedOrgans { get => containedOrgans; set => containedOrgans = value; }
    private List<BodyPart> containedOrgans = new List<BodyPart>();
    public List<GameObject> containedOrgansGameObjects;

    //oxygen stuff
    float BodyPart.oxygen { get => oxygen; set => oxygen = value; }
    public float oxygen;

    float BodyPart.oxygenMax { get => oxygenMax; set => oxygenMax = value; }
    public float oxygenMax;

    float BodyPart.oxygenRequired { get => oxygenRequired; set => oxygenRequired = value; }
    public float oxygenRequired;

    public void PumpBlood()
    {
        BodyPartsStatic.PumpBlood(bloodPumpRate, Time.deltaTime, ref blood, ref connectedBodyParts, ref containedOrgans);
    }

    public void LoseBlood()
    {
        BodyPartsStatic.LoseBlood(bloodLossRate, Time.deltaTime, ref blood);
    }

    public void CheckForFunctionality()
    {
        isFunctioning = BodyPartsStatic.CheckForFunctionality(blood, bloodRequiredToFunction);
    }

    // Start is called before the first frame update
    void Start()
    {
        //connect body parts
        foreach (GameObject connectedBodyPartGameObject in connectedBodyPartsGameObjects)
        {
            connectedBodyParts.Add(connectedBodyPartGameObject.GetComponent<BodyPart>());
        }

        //connect organs
        foreach (GameObject connectedOrganGameObject in containedOrgansGameObjects)
        {
            connectedBodyParts.Add(connectedOrganGameObject.GetComponent<BodyPart>());
        }
    }

    public float tempUpdate = 0;

    void Update()
    {
        LoseBlood();
        //PumpBlood();
        CheckForFunctionality();

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 1.0f)
        {
            Debug.Log(gameObject.name + " Blood: " + blood);
            tempUpdate = 0.0f;
        }
    }
}
