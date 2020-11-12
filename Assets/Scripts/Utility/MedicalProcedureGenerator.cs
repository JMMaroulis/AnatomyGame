using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedicalProcedureGenerator : MonoBehaviour
{
    public GameObject body;
    public TextLog textLog;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;
    private UnlockTracker unlockTracker;

    //requested procedure logs
    public bool ClockworkHeartRequest;

    // Start is called before the first frame update
    void Start()
    {
        GameSetupScenarioTracker injurySpawnTracker = GameObject.FindObjectOfType<GameSetupScenarioTracker>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
        PopulateBodyPartsList();
        PopulateOrgansList();
        RequestedProcedures(injurySpawnTracker.requestedProcedures);
    }

    public void RequestedProcedures(int numberOfProcedures)
    {
        for (int i = 0; i < numberOfProcedures; i++)
        {
            int procedureNumber = Random.Range(0, 1);

            switch (procedureNumber)
            {
                case 0:
                    textLog.NewLogEntry("The patient requests that their heart(s) be clockwork, not biological.");
                    ClockworkHeartRequest = true;
                    break;

                default:
                    break;
            }

        }

    }

    void PopulateBodyPartsList()
    {

        bodyParts = new List<BodyPart>();

        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyParts.Add(body.transform.GetChild(i).GetComponent<BodyPart>());
        }

    }

    void PopulateOrgansList()
    {
        organs = new List<Organ>();

        //get organs from bodyparts list
        foreach(BodyPart bodyPart in bodyParts)
        {
            foreach (Organ organ in bodyPart.containedOrgans)
            {
                organs.Add(organ);
            }
        }

    }
}
