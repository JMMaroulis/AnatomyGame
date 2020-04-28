using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class OxygenLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private float secondCounter;
    private Text oxygenText;

    // Start is called before the first frame update
    void Start()
    {
        oxygenText = gameObject.GetComponent<Text>();
        PopulateBodyPartsList();
        UpdateOxygenText();
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
            UpdateOxygenText();
            secondCounter = 0.0f;
        }

        PopulateBodyPartsList();

    }

    void UpdateOxygenText()
    {
        string oxygenTextNew = "Oxygen Levels:\n";
        float oxygenSum = 0;
        foreach (BodyPart bodyPart in bodyParts)
        {
            oxygenTextNew += bodyPart.name;
            oxygenTextNew += ": ";
            oxygenTextNew += Mathf.Round(bodyPart.GetComponent<BodyPart>().oxygen);
            oxygenTextNew += "\n";
            oxygenSum += bodyPart.oxygen;
        }
        oxygenTextNew += "Sum Oxygen: " + oxygenSum;

        oxygenText.text = oxygenTextNew;

    }
}
