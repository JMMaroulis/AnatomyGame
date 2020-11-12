using UnityEngine;
using UnityEngine.UI;

public class PatientNumberReporting : MonoBehaviour
{
    public Text patientNumberText;
    public int patientNumber;

    // Start is called before the first frame update
    void Start()
    {
        patientNumberText.text = $"Patient #{FindObjectOfType<GameSetupScenarioTracker>().patientNumber}";
    }

    // Update is called once per frame
    void Update()
    {

    }

}
