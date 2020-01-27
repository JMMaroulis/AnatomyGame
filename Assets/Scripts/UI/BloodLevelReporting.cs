using System.Collections;
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
        PopulateBodyPartsList();
        UpdateBloodText();
    }

    void PopulateBodyPartsList()
    {
        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            if (!bodyPartObjects.Contains(body.transform.GetChild(i).gameObject))
            {
                bodyPartObjects.Add(body.transform.GetChild(i).gameObject);
            }

            //get organs from bodypart
            //NOTE: THIS INTRODUCES THE ASSUMPTION THAT ORGANS WILL ALWAYS BE CHILDREN OF THEIR CONTAINING BODYPARTS
            //AS WELL AS CONTAINED IN THE containedOrgans LISTS. IT WILL DO FOR NOW.
            for (int j = 0; j < body.transform.GetChild(i).childCount; j++)
            {
                if (!bodyPartObjects.Contains(body.transform.GetChild(i).GetChild(j).gameObject))
                {
                    bodyPartObjects.Add(body.transform.GetChild(i).GetChild(j).gameObject);
                }
            }
        }

        bloodText = gameObject.GetComponent<Text>();
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

        PopulateBodyPartsList();
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
