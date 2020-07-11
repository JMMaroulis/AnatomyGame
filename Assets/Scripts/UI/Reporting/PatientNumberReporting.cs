using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PatientNumberReporting : MonoBehaviour
{
    public Text patientNumberText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void PopulateBodyPartsList()
    {
        patientNumberText.text = $"Patient #{FindObjectOfType<InjurySpawnTracker>().patientNumber}";
    }

    // Update is called once per frame
    void Update()
    {

    }

}
