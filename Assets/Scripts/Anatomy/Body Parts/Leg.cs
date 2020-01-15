using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leg : MonoBehaviour, BodyPart
{
    float BodyPart.blood { get => blood; set => blood = value; }
    public float blood;

    float BodyPart.bloodPumpRate { get => bloodPumpRate; set => bloodPumpRate = value; }
    public float bloodPumpRate;

    float BodyPart.bloodLossRate { get => bloodLossRate; set => bloodLossRate = value; }
    public float bloodLossRate;


    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    //TODO: add a check for running out of blood
    //only pump blood if there's blood left to pump
    public void PumpBlood(float pumpRate, float timeSinceLastPump)
    {
        //pumping blood to body parts in a random order, to prevent loops forming between pairs of bodyparts, trapping blood between them
        List<int> pumpOrder = Enumerable.Range(0, connectedBodyParts.Count()).ToList<int>();
        IListExtensions.Shuffle<int>(pumpOrder);
        foreach (int bodyPartIndex in pumpOrder)
        {
            if (connectedBodyParts[bodyPartIndex].blood <= blood)
            {
                float proposedBloodOut = pumpRate * timeSinceLastPump;
                float bloodOut = Mathf.Max(Mathf.Min(blood, proposedBloodOut), 0);
                connectedBodyParts[bodyPartIndex].blood += bloodOut;
                blood -= bloodOut;
            }
        }
    }

    public void LoseBlood(float lossRate, float timeSinceLastLoss)
    {
        float bloodLost = lossRate * timeSinceLastLoss;
        blood = Mathf.Max(blood - bloodLost, 0);
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

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 1.0f)
        {
            Debug.Log(gameObject.name + " Blood: " + blood);
            tempUpdate = 0.0f;

        }
    }
}
