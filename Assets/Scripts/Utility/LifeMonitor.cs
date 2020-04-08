using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeMonitor : MonoBehaviour
{

    public bool isTimePassing;
    public Clock clock;
    public bool hasPlayerWon;
    public float requiredVictoryWait;
    public Text messageBox;

    public GameObject body;
    private List<GameObject> bodyPartObjects;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayerWon = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (hasPlayerWon)
        {
            messageBox.text = "Congration, you done it!";
        }
        
    }

    public void VictoryCheck()
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(requiredVictoryWait);
        StaticCoroutine.Start(VictoryCheckCoroutine(requiredVictoryWait));
    }

    public IEnumerator VictoryCheckCoroutine(float seconds)
    {
        //clock.globalTimeScalingFactor = 60.0f;
        clock.StartClockUntil(seconds);
        yield return new WaitForSeconds(seconds);

        bool victory = true;

        //check the vital statistics of every bodypart in the body
        PopulateBodyPartsList();
        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            BodyPart bodyPart = bodyPartObject.GetComponent<BodyPart>();

            victory = victory && bodyPart.blood >= bodyPart.bloodRequiredToFunction;
            victory = victory && bodyPart.oxygen >= bodyPart.oxygenRequired;
            victory = victory && bodyPart.damage <= ( bodyPart.damageMax / 5.0f);
        }

        hasPlayerWon = victory;
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
