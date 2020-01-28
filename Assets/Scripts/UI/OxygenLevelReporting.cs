using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenLevelReporting : MonoBehaviour
{
    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();
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

    // Update is called once per frame
    void Update()
    {

        secondCounter += Time.deltaTime;
        if (secondCounter >= 1.0f)
        {
            UpdateOxygenText();
            secondCounter -= 1.0f;
        }

        PopulateBodyPartsList();

    }

    void UpdateOxygenText()
    {
        string oxygenTextNew = "Oxygen Levels:\n";
        float oxygenSum = 0;
        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            oxygenTextNew += bodyPartObject.name;
            oxygenTextNew += ": ";
            oxygenTextNew += Mathf.Round(bodyPartObject.GetComponent<BodyPart>().oxygen);
            oxygenTextNew += "\n";
            oxygenSum += bodyPartObject.GetComponent<BodyPart>().oxygen;
        }
        oxygenTextNew += "Sum Oxygen: " + oxygenSum;

        oxygenText.text = oxygenTextNew;

    }
}
