using UnityEngine;

public class GameSetupScenarioTracker : MonoBehaviour
{
    public int easyInjuries;
    public int mediumInjuries;
    public int hardInjuries;
    public int requestedProcedures;

    public int patientNumber;

    public int goldReward;

    public int easyInjuryReward;
    public int mediumInjuryReward;
    public int hardInjuryReward;
    public int requestedProcedureReward;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewInjuries()
    {
        easyInjuries = Mathf.FloorToInt(patientNumber % 3);
        mediumInjuries = Mathf.FloorToInt((patientNumber / 3) % 3);
        hardInjuries = Mathf.FloorToInt(patientNumber / 9);
    }

    public void NewProcedures()
    {
        requestedProcedures = Mathf.CeilToInt(patientNumber / 2);
        requestedProcedures = Mathf.Max(1, requestedProcedures);
    }

    public void OnLoad()
    {
        //here cause the level setup thing adds 1 to the patient number on scene start
        //so if we don't do this, on load, we'll have essentially skipped a patient
        patientNumber -= 1;
    }

}
