using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InjurySpawnTracker : MonoBehaviour
{
    public int easyInjuries;
    public int mediumInjuries;
    public int hardInjuries;
    public int patientNumber;

    // Start is called before the first frame update
    void Start()
    {
        patientNumber = 1;
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void Reset()
    {
        easyInjuries = 0;
        mediumInjuries = 0;
        hardInjuries = 0;
        patientNumber = 0;
    }

}
