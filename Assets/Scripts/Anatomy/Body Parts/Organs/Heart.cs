using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, BodyPart
{
    bool BodyPart.isFunctioning { get => isFunctioning; set => isFunctioning = value; }
    public bool isFunctioning = true;

    //blood stuff
    float BodyPart.blood { get => blood; set => blood = value; }
    public float blood;

    float BodyPart.bloodRequiredToFunction { get => bloodRequiredToFunction; set => bloodRequiredToFunction = value; }
    public float bloodRequiredToFunction;

    float BodyPart.bloodPumpRate { get => bloodPumpRate; set => bloodPumpRate = value; }
    public float bloodPumpRate;

    float BodyPart.bloodLossRate { get => bloodLossRate; set => bloodLossRate = value; }
    public float bloodLossRate;

    //oxygen stuff
    float BodyPart.oxygen { get => oxygen; set => oxygen = value; }
    public float oxygen;

    float BodyPart.oxygenMax { get => oxygenMax; set => oxygenMax = value; }
    public float oxygenMax;

    float BodyPart.oxygenRequired { get => oxygenRequired; set => oxygenRequired = value; }
    public float oxygenRequired;

    //damage stuff
    float BodyPart.damage { get => damage; set => damage = value; }
    public float damage;

    float BodyPart.damageMax { get => damageMax; set => damageMax = value; }
    public float damageMax;

    float BodyPart.efficiency { get => efficiency; set => efficiency = value; }
    public float efficiency;

    //other body parts
    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    List<BodyPart> BodyPart.containedOrgans { get => containedOrgans; set => containedOrgans = value; }
    private List<BodyPart> containedOrgans = new List<BodyPart>();
    public List<GameObject> containedOrgansGameObjects;


    public void PumpBlood(float efficiency)
    {
        BodyPartsStatic.PumpBlood(efficiency, bloodPumpRate, Time.deltaTime, ref blood, ref oxygen, ref connectedBodyParts, ref containedOrgans);
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

    public void UpdateEfficiency()
    {
        efficiency = BodyPartsStatic.UpdateEfficiency(damage, damageMax, oxygen, oxygenRequired);
    }

    public void UpdateDamage()
    {
        damage = BodyPartsStatic.UpdateDamage(damage, damageMax, oxygen, oxygenRequired);
    }


    //responsible for controlling blood pumping in all bodyparts
    //TODO: the amount of blood moving around should be capped by the amount of blood actually *in* the heart
    void PumpBloodMaster()
    {
        if (isFunctioning == false)
        {
            return;
        }
        //get parts bodypart (most likely the torso)
        //BodyPart parentBodyPart = this.transform.parent.GetComponent<BodyPart>();

        PumpBloodRecursive(this, new List<BodyPart>());
    }

    void PumpBloodRecursive(BodyPart currentBodyPart, List<BodyPart> alreadyPumped)
    {
        currentBodyPart.PumpBlood(efficiency);
        alreadyPumped.Add(currentBodyPart);
        foreach (BodyPart bodyPart in currentBodyPart.containedOrgans)
        {
            if (alreadyPumped.Contains(bodyPart) == false)
            {
                PumpBloodRecursive(bodyPart, alreadyPumped);
            }
        }
        foreach (BodyPart bodyPart in currentBodyPart.connectedBodyParts)
        {
            if (alreadyPumped.Contains(bodyPart) == false)
            {
                PumpBloodRecursive(bodyPart, alreadyPumped);
            }
        }

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

    // Update is called once per frame
    void Update()
    {
        CheckForFunctionality();
        UpdateEfficiency();
        UpdateDamage();
        LoseBlood();
        ConsumeOxygen();
        PumpBloodMaster();
    }

    public void SeverConnection(GameObject connectedBodyPart)
    {
        throw new System.NotImplementedException();
    }

    public void SeverAllConnections()
    {
        throw new System.NotImplementedException();
    }

    public void CreateConnection(GameObject bodyPartToConnect)
    {
        if (bodyPartToConnect != this)
        {
            BodyPartsStatic.CreateConnection(bodyPartToConnect, ref connectedBodyPartsGameObjects, ref connectedBodyParts);
        }
    }
}
