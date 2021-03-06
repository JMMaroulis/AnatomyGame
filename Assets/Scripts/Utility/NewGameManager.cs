﻿
using UnityEngine;
using System;

public class NewGameManager : MonoBehaviour
{
    public UnlockTracker unlockTracker;
    public GoldTracker goldTracker;
    public GameSetupScenarioTracker injurySpawnTracker;


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
        FindObjectOfType<SceneTransitionManager>().UnlockScreen();
    }

    public void NewDailyChallenge()
    {
        //TODO: CURRENTLY BROKEN
        //RANDOM.STATE BEING SET ON TRANSITION TO SAMPLESCENE

        string today = DateTime.Today.ToString("yyyyMMdd");
        Debug.Log(DateTime.Today);
        Debug.Log((int)long.Parse(today));
        UnityEngine.Random.InitState((int)long.Parse(today));
        NewGame();
    }

    public void LoadGame()
    {
        FindObjectOfType<SaveManager>().DecodeTrackers();
        FindObjectOfType<SceneTransitionManager>().UnlockScreen();
    }

}
