﻿using UnityEngine;

public class GoldTracker : MonoBehaviour
{
    public int goldSpent;
    public int goldAccumulated;
    public int gold
    {
        get { return goldAccumulated - goldSpent; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
