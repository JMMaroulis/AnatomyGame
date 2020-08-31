using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMonitor : MonoBehaviour
{

    public bool isTimePassing;
    public bool hasPlayerWon;
    public TextLog textLog;

    public GameObject body;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;
    private Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayerWon = false;
        clock = FindObjectOfType<Clock>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VictoryCheck(float secondsToWait)
    {
        StaticCoroutine.Start(VictoryCheckCoroutine(secondsToWait));
    }

    public IEnumerator VictoryCheckCoroutine(float secondsToWait)
    {
        textLog.NewLogEntry("Checking patient condition:");
        bool victory = true;

        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        float timer = 0.0f;
        while (timer < secondsToWait)
        {
            timer += Time.deltaTime * clock.globalTimeScalingFactor;
            yield return null;
        }

        //check the vital statistics of every bodypart in the body
        PopulateBodyPartsList();
        foreach (BodyPart bodyPart in bodyParts)
        {
            if (bodyPart.blood < bodyPart.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough blood.\n");
                victory = false;
            }
            if (bodyPart.oxygen < bodyPart.oxygenRequired)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough oxygen.\n");
                victory = false;
            }
            if (bodyPart.damage > (bodyPart.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{bodyPart.name} is above 20% damage.\n");
                victory = false;
            }
        }
        foreach (Organ organ in organs)
        {
            if (organ.blood < organ.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough blood.\n");
                victory = false;
            }
            if (organ.oxygen < organ.oxygenRequired)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough oxygen.\n");
                victory = false;
            }
            if (organ.damage > (organ.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{organ.name} is above 20% damage.\n");
                victory = false;
            }
        }

        //TODO: rejig this to deal with the bodypart-specific definitions of what should be connected
        //check there's an appropriate number of each organ and limb
        Dictionary<System.Type, List<int>> typeCount = new Dictionary<System.Type, List<int>>{
            //{number the body has, number the body needs}
            { new LeftArm().GetType(),   new List<int> {0,1}},
            { new RightArm().GetType(),   new List<int> {0,1}},
            { new LeftLeg().GetType(),   new List<int> {0,1}},
            { new RightLeg().GetType(),   new List<int> {0,1}},
            { new Head().GetType(),  new List<int> {0,1}},
            { new Torso().GetType(), new List<int> {0,1}},
            { new LeftLung().GetType(),  new List<int> {0,1}},
            { new RightLung().GetType(),  new List<int> {0,1}},
            { new Heart().GetType(), new List<int> {0,1}},
            { new Brain().GetType(), new List<int> {0,1}},
            { new LeftEye().GetType(),  new List<int> {0,1}},
            { new RightEye().GetType(),  new List<int> {0,1}},
            { new Liver().GetType(), new List<int> {0,1}},
            { new Stomach().GetType(), new List<int> {0,1}}
        };

        foreach (BodyPart bodyPart in bodyParts)
        {
            Debug.Log(bodyPart.GetType());
            typeCount[bodyPart.GetType()][0] += 1;
        }

        foreach (Organ organ in organs)
        {
            Debug.Log(organ.GetType());
            typeCount[organ.GetType()][0] += 1;
        }


        foreach (System.Type key in typeCount.Keys)
        {
            if (typeCount[key][0] != typeCount[key][1])
            {
                textLog.NewLogEntry($"Patient should have {typeCount[key][1]} {key.Name}s, has {typeCount[key][0]}.\n");
                victory = false;
            }
        }


        //checking amounts of various drugs in the body
        foreach (BodyPart bodyPart in bodyParts)
        {
            if (bodyPart.slowPoison > 0)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still poisoned.\n");
                victory = false;
            }
        }
        foreach (Organ organ in organs)
        {
            if (organ.slowPoison > 0)
            {
                textLog.NewLogEntry($"{organ.name} is still poisoned.\n");
                victory = false;
            }
        }


        //currently, all charms are set to expire after 30 minutes anyway, but best to check just in case:
        foreach (BodyPart bodyPart in bodyParts)
        {
            if (bodyPart.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still charmed.\n");
                victory = false;
            }
        }
        foreach (Organ organ in organs)
        {
            if (organ.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{organ.name} is still charmed.\n");
                victory = false;
            }
        }

        hasPlayerWon = victory;
        if (victory)
        {
            textLog.NewLogEntry("Congratulations, he'll live!\nYour payment is 500 gold.\n Transfer in 5 seconds...");
            GameObject.FindObjectOfType<GoldTracker>().goldAccumulated += 500;

            yield return new WaitForSeconds(5 * Time.timeScale);
            UnityEngine.SceneManagement.SceneManager.LoadScene("UnlockScreen");

        }

    }



    void PopulateBodyPartsList()
    {
        bodyParts = new List<BodyPart>();
        organs = new List<Organ>();

        //get bodyparts from body, get organs from bodyparts in body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            BodyPart bodyPart = body.transform.GetChild(i).GetComponent<BodyPart>();
            bodyParts.Add(bodyPart);

            foreach(Organ organ in bodyPart.containedOrgans)
            {
                organs.Add(organ);
            }

        }

    }
}
