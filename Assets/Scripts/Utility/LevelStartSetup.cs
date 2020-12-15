using UnityEngine;

public class LevelStartSetup : MonoBehaviour
{
    private GameSetupScenarioTracker gameSetupScenarioTracker;
    private MedicalProcedureGenerator medicalProcedureGenerator;
    private PhysicalInjuryGenerator physicalInjuryGenerator;

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Generating Level");
        gameSetupScenarioTracker = FindObjectOfType<GameSetupScenarioTracker>();
        medicalProcedureGenerator = FindObjectOfType<MedicalProcedureGenerator>();
        physicalInjuryGenerator = FindObjectOfType<PhysicalInjuryGenerator>();

        int choice = Random.Range(0, 2);

        //A (hopefully) temporary fix; all current injuries need one of these two,
        //so to prevent spawning injuries if none of the injuries can be fixed,
        //here we are
        UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
        if (!unlockTracker.spawn_clock && !unlockTracker.spawn)
        {
            choice = 0;
        }

        switch (choice)
        {
            case 0:
                Debug.Log("Generating Injuries");
                gameSetupScenarioTracker.NewInjuries();
                physicalInjuryGenerator.GenerateInjuries();
                gameSetupScenarioTracker.goldReward = CalculateInjuriesGoldReward();
                break;

            case 1:
                Debug.Log("Generating voluntary Procedures");
                gameSetupScenarioTracker.NewProcedures();
                medicalProcedureGenerator.RequestedProcedures(gameSetupScenarioTracker.requestedProcedures);
                gameSetupScenarioTracker.goldReward = CalculateProceduresGoldReward();
                break;

            default:
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    int CalculateProceduresGoldReward()
    {
        int gold = gameSetupScenarioTracker.requestedProcedures * gameSetupScenarioTracker.requestedProcedureReward;
        return gold;
    }

    int CalculateInjuriesGoldReward()
    {
        int gold = 0;
        gold += gameSetupScenarioTracker.easyInjuryReward * gameSetupScenarioTracker.easyInjuries;
        gold += gameSetupScenarioTracker.mediumInjuryReward * gameSetupScenarioTracker.mediumInjuries;
        gold += gameSetupScenarioTracker.hardInjuryReward * gameSetupScenarioTracker.hardInjuries;
        return gold;
    }

}
