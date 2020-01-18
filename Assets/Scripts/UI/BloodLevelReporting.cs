﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();
    private float secondCounter;
    private Text bloodText;

    // Start is called before the first frame update
    void Start()
    {
        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyPartObjects.Add(body.transform.GetChild(i).gameObject);
        }

        bloodText = gameObject.GetComponent<Text>();
        UpdateBloodText();
    }

    // Update is called once per frame
    void Update()
    {

        secondCounter += Time.deltaTime;
        if (secondCounter >= 1.0f)
        {
            UpdateBloodText();
            secondCounter -= 1.0f;

        }


    }

    void UpdateBloodText()
    {
        string bloodTextNew = "Blood Levels:\n";
        float bloodSum = 0;
        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            bloodTextNew += bodyPartObject.name;
            bloodTextNew += ": ";
            bloodTextNew += Mathf.Round(bodyPartObject.GetComponent<BodyPart>().blood);
            bloodTextNew += "\n";
            bloodSum += bodyPartObject.GetComponent<BodyPart>().blood;
        }
        bloodTextNew += "Sum Blood: " + bloodSum;

        bloodText.text = bloodTextNew;

    }
}