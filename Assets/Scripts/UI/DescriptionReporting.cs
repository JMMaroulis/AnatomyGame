using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionReporting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FetchDescription(BodyPart bodyPart)
    {
        string description = bodyPart.GenerateDescription();
        gameObject.GetComponent<Text>().text = description;
    }
}
