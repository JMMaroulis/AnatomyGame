using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour, BodyPart
{
    public float blood { get; set; } = 500f;
    private float timeSinceLastPump = 0;

    public float bloodLossRate { get; set; } = 0f;
    private float timeSinceLastBloodLoss = 0;

    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    //TODO: add a check for running out of blood
    //only pump blood if there's blood left to pump
    public void PumpBlood()
    {
        foreach (BodyPart connectedBodyPart in connectedBodyParts)
        {
            if (blood >= 0.0f)
            {
                float newBlood = Mathf.Max(blood - 2f, 0f);
                connectedBodyPart.blood += (blood - newBlood);
                blood = newBlood;
            }
        }
    }

    public void LoseBlood()
    {
        blood = blood - bloodLossRate;
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

    // Update is called once per frame
    void Update()
    {
        //pumps blood once per second
        timeSinceLastPump += Time.deltaTime;
        if (timeSinceLastPump >= 1.0f)
        {
            LoseBlood();
            PumpBlood();
            timeSinceLastPump = 0.0f;
        }

        tempUpdate += Time.deltaTime;
        if (tempUpdate >= 3.0f)
        {
            Debug.Log("Arm Blood: " + blood);
            tempUpdate = 0.0f;
        }
    }

}
