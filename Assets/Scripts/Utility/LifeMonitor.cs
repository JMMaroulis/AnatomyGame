using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LifeMonitor : MonoBehaviour
{

    public bool isTimePassing;
    public bool hasPlayerWon;
    public TextLog textLog;

    public GameObject body;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayerWon = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void VictoryCheck()
    {
        StaticCoroutine.Start(VictoryCheckCoroutine());
    }

    public IEnumerator VictoryCheckCoroutine()
    {
        textLog.NewLogEntry("Checking patient condition:");
        bool victory = true;

        //check the vital statistics of every bodypart in the body
        BodyPartManager bodyPartManager = FindObjectOfType<BodyPartManager>();
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart.blood < bodyPart.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough blood.");
                victory = false;
            }
            if (bodyPart.oxygen < bodyPart.oxygenRequired)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough oxygen.");
                victory = false;
            }
            if (bodyPart.damage > (bodyPart.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{bodyPart.name} is above 20% damage.");
                victory = false;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (organ.blood < organ.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough blood.");
                victory = false;
            }
            if (organ.oxygen < organ.oxygenRequired)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough oxygen.");
                victory = false;
            }
            if (organ.damage > (organ.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{organ.name} is above 20% damage.");
                victory = false;
            }
        }

        //check there's an appropriate number of each organ and limb
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            //limbs
            int numLeftArms = bodyPart.connectedBodyParts.Count(element => element is LeftArm);
            if (bodyPart.maxLeftArms != numLeftArms)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftArms} left arms, has {numLeftArms}.");
                victory = false;
            }

            int numRightArms = bodyPart.connectedBodyParts.Count(element => element is RightArm);
            if (bodyPart.maxRightArms != numRightArms)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightArms} right arms, has {numRightArms}.");
                victory = false;
            }

            int numLeftLegs = bodyPart.connectedBodyParts.Count(element => element is LeftLeg);
            if (bodyPart.maxLeftLegs != numLeftLegs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftLegs} left legs, has {numLeftLegs}.");
                victory = false;
            }

            int numRightLegs = bodyPart.connectedBodyParts.Count(element => element is RightLeg);
            if (bodyPart.maxRightLegs != numRightLegs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightLegs} right legs, has {numRightLegs}.");
                victory = false;
            }

            int numHeads = bodyPart.connectedBodyParts.Count(element => element is Head);
            if (bodyPart.maxHeads != numHeads)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxHeads} heads, has {numHeads}.");
                victory = false;
            }

            int numTorsos = bodyPart.connectedBodyParts.Count(element => element is Torso);
            if (bodyPart.maxTorsos != numTorsos)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxTorsos} torsos, has {numTorsos}.");
                victory = false;
            }

            //organs
            int numBrains = bodyPart.containedOrgans.Count(element => element is Brain);
            if (bodyPart.maxBrains != numBrains)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxBrains} brains, has {numBrains}.");
                victory = false;
            }

            int numLeftEyes = bodyPart.containedOrgans.Count(element => element is LeftEye);
            if (bodyPart.maxLeftEyes != numLeftEyes)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftEyes} left eyes, has {numLeftEyes}.");
                victory = false;
            }

            int numRightEyes = bodyPart.containedOrgans.Count(element => element is RightEye);
            if (bodyPart.maxRightEyes != numRightEyes)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightEyes} right eyes, has {numRightEyes}.");
                victory = false;
            }

            int numHearts = bodyPart.containedOrgans.Count(element => element is Heart);
            if (bodyPart.maxHearts != numHearts)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxHearts} hearts, has {numHearts}.");
                victory = false;
            }

            int numLivers = bodyPart.containedOrgans.Count(element => element is Liver);
            if (bodyPart.maxLivers != numLivers)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLivers} livers, has {numLivers}.");
                victory = false;
            }

            int numLeftLungs = bodyPart.containedOrgans.Count(element => element is LeftLung);
            if (bodyPart.maxLeftLungs != numLeftLungs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftLungs} left lungs, has {numLeftLungs}.");
                victory = false;
            }

            int numRightLungs = bodyPart.containedOrgans.Count(element => element is RightLung);
            if (bodyPart.maxRightLungs != numRightLungs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightLungs} right lungs, has {numRightLungs}.");
                victory = false;
            }

            int numStomachs = bodyPart.containedOrgans.Count(element => element is Stomach);
            if (bodyPart.maxStomachs != numStomachs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxStomachs} stomachs, has {numStomachs}.");
                victory = false;
            }

        }


        
        //checking amounts of various drugs in the body
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart.slowPoison > 0)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still poisoned.");
                victory = false;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (organ.slowPoison > 0)
            {
                textLog.NewLogEntry($"{organ.name} is still poisoned.");
                victory = false;
            }
        }


        //currently, all charms are set to expire after 30 minutes anyway, but best to check just in case:
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (bodyPart.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still charmed.");
                victory = false;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (organ.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{organ.name} is still charmed.");
                victory = false;
            }
        }

        hasPlayerWon = victory;
        if (victory)
        {
            GameObject.FindObjectOfType<GoldTracker>().goldAccumulated += 500;
            textLog.NewLogEntry("Congratulations, he'll live! Your payment is 500 gold. Transfer in 5 seconds...");

            yield return new WaitForSeconds(5 * Time.timeScale);
            UnityEngine.SceneManagement.SceneManager.LoadScene("UnlockScreen");

        }

    }

}
