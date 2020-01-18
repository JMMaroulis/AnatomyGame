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

    public void PumpBlood(float pumpRate, float timeSinceLastPump)
    {
        BodyPartsStatic.PumpBlood(bloodPumpRate, Time.deltaTime, ref blood, ref connectedBodyParts);
    }

    public void LoseBlood(float lossRate, float timeSinceLastLoss)
    {
        BodyPartsStatic.LoseBlood(lossRate, timeSinceLastLoss, ref blood);
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
    }

    public float tempUpdate = 0;

    void Update()
    {
        LoseBlood(bloodLossRate / 2f, Time.deltaTime);
        PumpBlood(bloodPumpRate, Time.deltaTime);
        LoseBlood(bloodLossRate / 2f, Time.deltaTime);
        CheckForFunctionality();

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 1.0f)
        {
            Debug.Log(gameObject.name + " Blood: " + blood);
            tempUpdate = 0.0f;
        }
    }
}
