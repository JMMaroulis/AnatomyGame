using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torso : MonoBehaviour, BodyPart
{
    float BodyPart.blood { get => blood; set => blood = value; }
    public float blood;
    private float timeSinceLastPump = 0;

    float BodyPart.bloodPumpRate { get => bloodPumpRate; set => bloodPumpRate = value; }
    public float bloodPumpRate;

    float BodyPart.bloodLossRate { get => bloodLossRate; set => bloodLossRate = value; }
    public float bloodLossRate;
    private float timeSinceLastBloodLoss = 0;


    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    //TODO: add a check for running out of blood
    //only pump blood if there's blood left to pump
    public void PumpBlood(float pumpRate, float timeSinceLastPump)
    {
        foreach (BodyPart connectedBodyPart in connectedBodyParts)
        {
            float proposedBloodOut = pumpRate * timeSinceLastPump;
            float cappedBloodOut = Mathf.Min(blood, proposedBloodOut);
            connectedBodyPart.blood += cappedBloodOut;
            blood -= cappedBloodOut;
        }
    }

    public void LoseBlood(float lossRate, float timeSinceLastLoss)
    {
        float bloodLost = Mathf.Max(lossRate * timeSinceLastLoss, 0);
        blood -= bloodLost;
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
        LoseBlood(bloodLossRate, Time.deltaTime);
        PumpBlood(bloodPumpRate, Time.deltaTime);

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 1.0f)
        {
            Debug.Log(gameObject.name + " Blood: " + blood);
            tempUpdate = 0.0f;

        }
    }
}
