﻿
using UnityEngine;

public class ddol : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
        Application.targetFrameRate = 60;
    }

}
