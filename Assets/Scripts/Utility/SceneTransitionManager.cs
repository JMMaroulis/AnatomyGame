using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SampleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void UnlockScreen()
    {
        FindObjectOfType<GameSetupScenarioTracker>().patientNumber += 1;
        FindObjectOfType<RandomTracker>().LevelStart();
        FindObjectOfType<PhysicalInjuryGenerator>().GenerateRandomNumbers();
        UnityEngine.SceneManagement.SceneManager.LoadScene("UnlockScreen");
        FindObjectOfType<SaveManager>().EncodeTrackers();
    }

    public void PerkScreen()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PerkScreen");
    }

}
