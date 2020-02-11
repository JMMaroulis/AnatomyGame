using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{

    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();

    //TODO: find some way to get this thing working as a public bool in the inspector with properties, rather than just updating every bodypart value every frame
    public bool isTimePassing; // { get { return _isTimePassing; } set { _isTimePassing = value; BodyPartsTimePaassing(); } }
    //private bool _isTimePassing;

    public float globalTimeScalingFactor;
    private float timeUntilClockStops = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = globalTimeScalingFactor;
        isTimePassing = false;
        PopulateBodyPartsList();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = globalTimeScalingFactor;
        PopulateBodyPartsList();

        //checking if it's time for time to stop yet
        timeUntilClockStops = Mathf.Max(timeUntilClockStops - Time.deltaTime, 0.0f);
        if (timeUntilClockStops <= 0.0f)
        {
            isTimePassing = false;
        }

        BodyPartsTimePassing();
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

    //would be great if we could trigger this on isTimePassing changing, rather than invoking it manually every frame
    public void BodyPartsTimePassing()
    {

        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            BodyPart bodyPart = bodyPartObject.GetComponent<BodyPart>();
            bodyPart.isTimePassing = isTimePassing;
        }

    }

    //sets time passing for all bodyparts for [ingameSeconds] of IN-GAME-TIME
    public void StartClockUntil(float ingameSeconds)
    {
        isTimePassing = true;
        timeUntilClockStops = ingameSeconds;
    }

    public static IEnumerator WaitForSecondsStatic(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}
