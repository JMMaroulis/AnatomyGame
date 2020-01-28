using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();
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

        secondCounter += Time.deltaTime;
        if (secondCounter >= 1.0f)
        {
            UpdateDamageText();
            secondCounter -= 1.0f;

        }
        PopulateBodyPartsList();

    }

    void PopulateBodyPartsList()
    {

        bodyPartObjects = new List<GameObject>();

        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyPartObjects.Add(body.transform.GetChild(i).gameObject);

            //get organs from bodypart
            //NOTE: THIS INTRODUCES THE ASSUMPTION THAT ORGANS WILL ALWAYS BE CHILDREN OF THEIR CONTAINING BODYPARTS
            //AS WELL AS CONTAINED IN THE containedOrgans LISTS. IT WILL DO FOR NOW.
            for (int j = 0; j < body.transform.GetChild(i).childCount; j++)
            {
                bodyPartObjects.Add(body.transform.GetChild(i).GetChild(j).gameObject);
            }
        }

    }

    void UpdateDamageText()
    {
        string damageTextNew = "Damage Levels:\n";
        float damageSum = 0;
        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            damageTextNew += bodyPartObject.name;
            damageTextNew += ": ";
            damageTextNew += Mathf.Round(bodyPartObject.GetComponent<BodyPart>().damage);
            damageTextNew += "\n";
            damageSum += bodyPartObject.GetComponent<BodyPart>().damage;
        }
        damageTextNew += "Sum Damage: " + damageSum;

        damageText.text = damageTextNew;

    }
}
