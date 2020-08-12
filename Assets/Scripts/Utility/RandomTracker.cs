using UnityEngine;

public class RandomTracker : MonoBehaviour
{

    public Random.State levelstart_randomstate;

    public void LevelStart()
    {
        levelstart_randomstate = Random.state;   
    }

    public void OnLoad()
    {
        Random.state = levelstart_randomstate;
    }
}
