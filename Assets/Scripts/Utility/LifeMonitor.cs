using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMonitor : MonoBehaviour
{

    public bool isTimePassing;
    public float victoryDuration;
    public float victoryDurationLimit;

    public GameObject body;
    private List<GameObject> bodyPartObjects;

    // Start is called before the first frame update
    void Start()
    {
        victoryDuration = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        PopulateBodyPartsList();

        if (isTimePassing)
        {
            if (VictoryCheck())
            {
                victoryDuration += Time.deltaTime;
            }
        }


        if (victoryDuration >= victoryDurationLimit)
        {
            Debug.Log("Congration: you done it");
        }
    }

    private bool VictoryCheck()
    {
        bool victory = true;

        //check the vital statistics of every bodypart in the body
        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            BodyPart bodyPart = bodyPartObject.GetComponent<BodyPart>();

            victory = victory && bodyPart.blood >= bodyPart.bloodRequiredToFunction;
            victory = victory && bodyPart.oxygen >= bodyPart.oxygenRequired;
            victory = victory && bodyPart.damage <= (bodyPart.damageMax / 2.0f);
        }

        return victory;
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
}
