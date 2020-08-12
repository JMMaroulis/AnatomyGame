using UnityEngine;

public class GoldTracker : MonoBehaviour
{
    public int goldSpent;
    public int goldAccumulated;

    public int levelstart_goldSpent;
    public int levelstart_goldAccumulated;

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
        levelstart_goldSpent = goldSpent;
        levelstart_goldAccumulated = goldAccumulated;
    }

    public void OnLoad()
    {
        goldSpent = levelstart_goldSpent;
        goldAccumulated = levelstart_goldAccumulated;
    }

}
