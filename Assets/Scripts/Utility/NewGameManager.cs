using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewGameManager : MonoBehaviour
{
    public UnlockTracker unlockTracker;
    public GoldTracker goldTracker;
    public InjurySpawnTracker injurySpawnTracker;


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
        FindObjectOfType<LevelManager>().LevelStart();
    }

    public void NewDailyChallenge()
    {
        string today = DateTime.Today.ToString("yyyyMMdd");
        Debug.Log(DateTime.Today);
        Debug.Log((int)long.Parse(today));
        UnityEngine.Random.InitState((int)long.Parse(today));
        NewGame();
    }

    public void LoadGame()
    {
        FindObjectOfType<SaveManager>().DecodeTrackers();
        NewGame();
    }

}
