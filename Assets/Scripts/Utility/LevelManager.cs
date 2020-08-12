using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GoldTracker goldTracker;
    public InjurySpawnTracker injurySpawnTracker;
    public UnlockTracker unlockTracker;
    public ActionTracker actionTracker;
    public RandomTracker randomTracker;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");

        goldTracker.LevelStart();
        injurySpawnTracker.LevelStart();
        unlockTracker.LevelStart();
        actionTracker.LevelStart();
        randomTracker.LevelStart();
    }
}