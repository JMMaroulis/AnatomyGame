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
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void NewDailyChallenge()
    {
        string today = DateTime.Today.ToString("yyyyMMdd");
        Debug.Log(DateTime.Today);
        Debug.Log((int)long.Parse(today));
        UnityEngine.Random.InitState((int)long.Parse(today));
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
