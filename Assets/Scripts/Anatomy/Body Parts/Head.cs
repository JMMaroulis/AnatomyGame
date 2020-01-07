using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour, BodyPart
{
    int BodyPart.blood { get => blood; set => blood = value; }
    private int blood = 500;

    int BodyPart.bloodLossRate { get => bloodLossRate; set => bloodLossRate = value; }
    private int bloodLossRate = 0;

    //TODO: populate List<BodyPart> from a List<GameObject> as defined in the scene on start()
    List<BodyPart> BodyPart.connectedBodyParts { get => connectedBodyParts; set => connectedBodyParts = value; }
    private List<BodyPart> connectedBodyParts = new List<BodyPart>();
    public List<GameObject> connectedBodyPartsGameObjects;

    //TODO: add a check for running out of blood
    void BodyPart.pumpBlood()
    {
        foreach (BodyPart connectedBodyPart in connectedBodyParts)
        {
            blood -= 1;
            connectedBodyPart.blood += 1;
        }
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
