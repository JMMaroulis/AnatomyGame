using UnityEngine;

public class InjurySpawnTracker : MonoBehaviour
{
    public int easyInjuries;
    public int mediumInjuries;
    public int hardInjuries;
    public int patientNumber;

    public int levelstart_easyInjuries;
    public int levelstart_mediumInjuries;
    public int levelstart_hardInjuries;
    public int levelstart_patientNumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextPatient()
    {
        patientNumber += 1;

        easyInjuries += 1;
        if (easyInjuries == 3)
        {
            easyInjuries = 0;
            mediumInjuries += 1;
        }
        if (mediumInjuries == 3)
        {
            mediumInjuries = 0;
            hardInjuries += 1;
        }
    }

    public void LevelStart()
    {
        levelstart_easyInjuries = easyInjuries;
        levelstart_mediumInjuries = mediumInjuries;
        levelstart_hardInjuries = hardInjuries;
        levelstart_patientNumber = patientNumber;
        NextPatient();
    }

    public void OnLoad()
    {
        easyInjuries = levelstart_easyInjuries;
        mediumInjuries = levelstart_mediumInjuries;
        hardInjuries   = levelstart_hardInjuries;
        patientNumber  = levelstart_patientNumber;
    }

}
