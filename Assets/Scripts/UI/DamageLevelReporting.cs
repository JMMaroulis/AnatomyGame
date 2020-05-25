using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class DamageLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private float secondCounter;
    private Text damageText;

    // Start is called before the first frame update
    void Start()
    {
        damageText = gameObject.GetComponent<Text>();
        PopulateBodyPartsList();
        UpdateDamageText();
    }

    // Update is called once per frame
    void Update()
    {
        PopulateBodyPartsList();
        secondCounter += Time.unscaledDeltaTime;
        if (secondCounter >= 0.1f)
        {
            UpdateDamageText();
            secondCounter = 0.0f;
        }
    }

    void PopulateBodyPartsList()
    {
        bodyParts = FindObjectsOfType<BodyPart>().ToList();
    }

    void UpdateDamageText()
    {
        string damageTextNew = "Damage Levels:\n";
        float damageSum = 0;
        foreach (BodyPart bodyPart in bodyParts)
        {
            damageTextNew += bodyPart.name;
            damageTextNew += ": ";
            damageTextNew += Mathf.Round(bodyPart.damage);
            damageTextNew += "\n";
            damageSum += bodyPart.damage;
        }
        damageTextNew += "Sum Damage: " + damageSum;

        damageText.text = damageTextNew;

    }
}
