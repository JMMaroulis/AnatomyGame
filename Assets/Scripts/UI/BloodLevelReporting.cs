using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BloodLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private float secondCounter;
    private Text bloodText;

    // Start is called before the first frame update
    void Start()
    {
        bloodText = gameObject.GetComponent<Text>();
        PopulateBodyPartsList();
        UpdateBloodText();
    }

    void PopulateBodyPartsList()
    {
        bodyParts = FindObjectsOfType<BodyPart>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        secondCounter += Time.unscaledDeltaTime;
        if (secondCounter >= 0.1f)
        {
            UpdateBloodText();
            secondCounter = 0.0f;
        }

        PopulateBodyPartsList();
    }

    void UpdateBloodText()
    {
        string bloodTextNew = "Blood Levels:\n";
        float bloodSum = 0;
        foreach (BodyPart bodyPart in bodyParts)
        {
            bloodTextNew += bodyPart.name;
            bloodTextNew += ": ";
            bloodTextNew += Mathf.Round(bodyPart.blood);
            bloodTextNew += "\n";
            bloodSum += bodyPart.blood;
        }
        bloodTextNew += "Sum Blood: " + bloodSum;

        bloodText.text = bloodTextNew;

    }
}
