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
    private BodyPartManager bodyPartManager;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayerWon = false;
        bodyPartManager = FindObjectOfType<BodyPartManager>();
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
        if (VitalsCheck())
        {
            victory = false;
        }

        //checking for an appropriate number of organs /  bodyparts
        if (BodyPartCountCheck())
        {
            victory = false;
        }

        //checking amounts of various drugs in the body
        if (DrugCheck())
        {
            victory = false;
        }

        //currently, all charms are set to expire after 30 minutes anyway, but best to check just in case:
        if (CharmCheck())
        {
            victory = false;
        }

        //check if there are any embedded objects in bodyparts
        //(later, we'll probably want specific embedded objects to be acceptable)
        if (EmbeddedObjectCheck())
        {
            victory = false;
        }

        hasPlayerWon = victory;
        if (victory)
        {
            FindObjectOfType<GoldTracker>().goldAccumulated += FindObjectOfType<GameSetupScenarioTracker>().goldReward;
            textLog.NewLogEntry($"Congratulations, they'll live! Your payment is {FindObjectOfType<GameSetupScenarioTracker>().goldReward} gold. Transfer in 5 seconds...");
        }
        else
        {
            textLog.NewLogEntry("Insufficient patient treatment. No payment for you. Transfer in 5 seconds...");
        }

        FindObjectOfType<ButtonActions>().DisableAllButtons();
        yield return new WaitForSeconds(5 * Time.timeScale);
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnlockScreen");

    }

    public bool SafeToDischarge()
    {
        BodyPartManager bodyPartManager = FindObjectOfType<BodyPartManager>();
        bool safe = true;

        //check there's an appropriate number of each organ and limb
        if (BodyPartCountCheck())
        {
            safe = false;
        }

        //currently, all charms are set to expire after 30 minutes anyway, but best to check just in case:
        if (CharmCheck())
        {
            safe = false;
        }

        if (EmbeddedObjectCheck())
        {
            safe = false;
        }

        return safe;

    }

    public bool DrugCheck()
    {
        bool result = false;
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (!bodyPart.isPartOfMainBody)
            {
                continue;
            }
            if (bodyPart.slowPoison > 0)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still poisoned.");
                result = false;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (!organ.isPartOfMainBody)
            {
                continue;
            }
            if (organ.slowPoison > 0)
            {
                textLog.NewLogEntry($"{organ.name} is still poisoned.");
                result = false;
            }
        }

        return result;
    }

    public bool CharmCheck()
    {
        bool result = false;
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (!bodyPart.isPartOfMainBody || bodyPart is Organ)
            {
                continue;
            }
            if (bodyPart.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{bodyPart.name} is still charmed.");
                result = true;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (!organ.isPartOfMainBody)
            {
                continue;
            }
            if (organ.GetComponent<Charm>() != null)
            {
                textLog.NewLogEntry($"{organ.name} is still charmed.");
                result = true;
            }
        }
        return result;
    }

    public bool EmbeddedObjectCheck()
    {
        bool result = false;

        //check if there are any embedded objects in bodyparts
        //(later, we'll probably want specific embedded objects to be acceptable)
        foreach (EmbeddedObject embeddedObject in FindObjectsOfType<EmbeddedObject>())
        {
            if (!(embeddedObject.parentBodyPart is null))
            {
                if (!(embeddedObject is ClockworkHeart))
                {
                    textLog.NewLogEntry($"Something is embedded inside the {embeddedObject.parentBodyPart.name}.");
                    result = true;
                }
            }
        }
        return result;
    }

    public bool BodyPartCountCheck()
    {
        bool result = false;

        //check there's an appropriate number of each organ and limb
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (!bodyPart.isPartOfMainBody)
            {
                continue;
            }

            if (bodyPart.requiresReplacing)
            {
                textLog.NewLogEntry($"{bodyPart.name} requires replacement!");
                result = true;
            }

            if (bodyPart.requiresAmputation)
            {
                textLog.NewLogEntry($"{bodyPart.name} requires amputation!");
                result = true;
            }

            //limbs
            int numLeftArms = bodyPart.connectedBodyParts.Count(element => element is LeftArm);
            if (bodyPart.maxLeftArms != numLeftArms)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftArms} left arms, has {numLeftArms}.");
                result = true;
            }

            int numRightArms = bodyPart.connectedBodyParts.Count(element => element is RightArm);
            if (bodyPart.maxRightArms != numRightArms)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightArms} right arms, has {numRightArms}.");
                result = true;
            }

            int numLeftLegs = bodyPart.connectedBodyParts.Count(element => element is LeftLeg);
            if (bodyPart.maxLeftLegs != numLeftLegs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftLegs} left legs, has {numLeftLegs}.");
                result = true;
            }

            int numRightLegs = bodyPart.connectedBodyParts.Count(element => element is RightLeg);
            if (bodyPart.maxRightLegs != numRightLegs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightLegs} right legs, has {numRightLegs}.");
                result = true;
            }

            int numHeads = bodyPart.connectedBodyParts.Count(element => element is Head);
            if (bodyPart.maxHeads != numHeads)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxHeads} heads, has {numHeads}.");
                result = true;
            }

            int numTorsos = bodyPart.connectedBodyParts.Count(element => element is Torso);
            if (bodyPart.maxTorsos != numTorsos)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxTorsos} torsos, has {numTorsos}.");
                result = true;
            }

            //organs
            int numBrains = bodyPart.containedOrgans.Count(element => element is Brain);
            if (bodyPart.maxBrains != numBrains)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxBrains} brains, has {numBrains}.");
                result = true;
            }

            int numLeftEyes = bodyPart.containedOrgans.Count(element => element is LeftEye);
            if (bodyPart.maxLeftEyes != numLeftEyes)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftEyes} left eyes, has {numLeftEyes}.");
                result = true;
            }

            int numRightEyes = bodyPart.containedOrgans.Count(element => element is RightEye);
            if (bodyPart.maxRightEyes != numRightEyes)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightEyes} right eyes, has {numRightEyes}.");
                result = true;
            }

            int numHearts = bodyPart.containedOrgans.Count(element => element is Heart);
            if (bodyPart.maxHearts != numHearts)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxHearts} hearts, has {numHearts}.");
                result = true;
            }

            int numLivers = bodyPart.containedOrgans.Count(element => element is Liver);
            if (bodyPart.maxLivers != numLivers)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLivers} livers, has {numLivers}.");
                result = true;
            }

            int numLeftLungs = bodyPart.containedOrgans.Count(element => element is LeftLung);
            if (bodyPart.maxLeftLungs != numLeftLungs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxLeftLungs} left lungs, has {numLeftLungs}.");
                result = true;
            }

            int numRightLungs = bodyPart.containedOrgans.Count(element => element is RightLung);
            if (bodyPart.maxRightLungs != numRightLungs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxRightLungs} right lungs, has {numRightLungs}.");
                result = true;
            }

            int numStomachs = bodyPart.containedOrgans.Count(element => element is Stomach);
            if (bodyPart.maxStomachs != numStomachs)
            {
                textLog.NewLogEntry($"{bodyPart.name} should have {bodyPart.maxStomachs} stomachs, has {numStomachs}.");
                result = true;
            }

        }

        return result;
    }

    public bool VitalsCheck()
    {
        bool result = false;
        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            if (!bodyPart.isPartOfMainBody)
            {
                continue;
            }
            if (bodyPart.blood < bodyPart.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough blood.");
                result = true;
            }
            if (bodyPart.oxygen < bodyPart.oxygenRequired)
            {
                textLog.NewLogEntry($"{bodyPart.name} doesn't have enough oxygen.");
                result = true;
            }
            if (bodyPart.damage > (bodyPart.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{bodyPart.name} is above 20% damage.");
                result = true;
            }
        }
        foreach (Organ organ in bodyPartManager.organs)
        {
            if (!organ.isPartOfMainBody)
            {
                continue;
            }
            if (organ.blood < organ.bloodRequiredToFunction)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough blood.");
                result = true;
            }
            if (organ.oxygen < organ.oxygenRequired)
            {
                textLog.NewLogEntry($"{organ.name} doesn't have enough oxygen.");
                result = true;
            }
            if (organ.damage > (organ.damageMax / 5.0f))
            {
                textLog.NewLogEntry($"{organ.name} is above 20% damage.");
                result = true;
            }
        }
        return result;
    }

    public bool ClockworkHeartCheck()
    {
        /*
        bool result = false;

        if (FindObjectOfType<MedicalProcedureGenerator>().ClockworkHeartRequest)
        {

            Heart[] hearts = FindObjectsOfType<Heart>();
            int n = 0;
            foreach (Heart heart in hearts)
            {
                if (heart.isPartOfMainBody)
                {
                    n += 1;
                }
            }

            if (n > 0)
            {
                result = true;
                textLog.NewLogEntry($"The patient still has {hearts.Count()} hearts that aren't clockwork.");
            }


        }

        return result;
        */
        return false;
        
    }

}
