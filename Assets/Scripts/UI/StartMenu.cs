using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    public void NewDailyChallenge()
    {
        FindObjectOfType<NewGameManager>().NewDailyChallenge();
    }
}
