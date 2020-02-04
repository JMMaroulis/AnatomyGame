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

    public void BodyPartsTimePassing()
    {

        foreach (GameObject bodyPartObject in bodyPartObjects)
        {
            BodyPart bodyPart = bodyPartObject.GetComponent<BodyPart>();
            bodyPart.isTimePassing = isTimePassing;
        }

    }

}
