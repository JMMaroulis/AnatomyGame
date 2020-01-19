using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lung : MonoBehaviour, BodyPart
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

    public float oxygenAbsorptionRate;

    float BodyPart.oxygenMax { get => oxygenMax; set => oxygenMax = value; }
    public float oxygenMax;

    float BodyPart.oxygenRequired { get => oxygenRequired; set => oxygenRequired = value; }
    public float oxygenRequired;

   
    public void PumpBlood()
    {
        BodyPartsStatic.PumpBlood(bloodPumpRate, Time.deltaTime, ref blood, ref oxygen, ref connectedBodyParts, ref containedOrgans);
    }

    public void LoseBlood()
    {
        BodyPartsStatic.LoseBlood(bloodLossRate, Time.deltaTime, ref blood);
    }

    public void ConsumeOxygen()
    {
        BodyPartsStatic.ConsumeOxygen(oxygenRequired, Time.deltaTime, ref oxygen);
    }

    public void CheckForFunctionality()
    {
        isFunctioning = BodyPartsStatic.CheckForFunctionality(blood, bloodRequiredToFunction, oxygen, oxygenRequired);
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
            containedOrgans.Add(connectedOrganGameObject.GetComponent<BodyPart>());
        }
    }

    void AbsorbOxygen()
    {
        if (isFunctioning)
        {
            oxygen = Mathf.Min(oxygenMax, oxygen + oxygenAbsorptionRate * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LoseBlood();
        AbsorbOxygen();
        CheckForFunctionality();
    }


}
