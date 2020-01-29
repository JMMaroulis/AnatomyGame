using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Arm : MonoBehaviour, BodyPart
{
    bool BodyPart.isTimePassing { get => isTimePassing; set => isTimePassing = value; }
    public bool isTimePassing = true;

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

    public void SeverConnection(GameObject connectedBodyPart)
    {
        BodyPartsStatic.SeverConnection(connectedBodyPart, ref connectedBodyPartsGameObjects, ref connectedBodyParts, ref bloodLossRate, 20);
    }

    public void SeverAllConnections()
    {
        BodyPartsStatic.SeverAllIncomingConnections(this.transform.gameObject, connectedBodyPartsGameObjects);
    }

    public void CreateConnection(GameObject bodyPartToConnect)
    {
        if (bodyPartToConnect != this)
        {
            BodyPartsStatic.CreateConnection(bodyPartToConnect, ref connectedBodyPartsGameObjects, ref connectedBodyParts);
        }
    }

    public void UpdateConnectedBodyParts()
    {
        connectedBodyParts = BodyPartsStatic.UpdateConnectedBodyParts(ref connectedBodyPartsGameObjects, ref connectedBodyParts);
    }

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

    public void UpdateEfficiency()
    {
        efficiency = BodyPartsStatic.UpdateEfficiency(damage, damageMax, oxygen, oxygenRequired);
    }

    public void CheckForFunctionality()
    {
        isFunctioning = BodyPartsStatic.CheckForFunctionality(blood, bloodRequiredToFunction, oxygen, oxygenRequired);
    }

    public void UpdateDamage()
    {
        damage = BodyPartsStatic.UpdateDamage(damage, damageMax, oxygen, oxygenRequired);
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

    public float tempUpdate = 0;

    void Update()
    {
        UpdateConnectedBodyParts();
        UpdateDamage();
        CheckForFunctionality();
        UpdateEfficiency();

        if (isTimePassing)
        {

            LoseBlood();
            ConsumeOxygen();
        }

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 1.0f)
        {
            Debug.Log(gameObject.name + " Blood: " + blood);
            tempUpdate = 0.0f;
        }
    }
}
