﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddol : MonoBehaviour
{

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
    }

}
