using UnityEngine;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        FindObjectOfType<NewGameManager>().NewGame();
    }

    public void LoadGame()
    { 
        FindObjectOfType<NewGameManager>().LoadGame();
    }

    public void NewDailyChallenge()
    {
        FindObjectOfType<NewGameManager>().NewDailyChallenge();
    }
}
