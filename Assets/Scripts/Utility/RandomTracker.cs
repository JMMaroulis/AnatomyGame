using UnityEngine;

public class RandomTracker : MonoBehaviour
{

    private Random.State levelstart_randomstate;

    public void LevelStart()
    {
        var x = new System.Random();
        Random.InitState(x.Next(0, 10000));

        levelstart_randomstate = Random.state;
    }

    public void OnLoad()
    {
        Random.state = levelstart_randomstate;
    }

}
