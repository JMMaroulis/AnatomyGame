﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    private Slider slider;
    public BodyPart bodyPart;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = bodyPart.blood;
    }

    public void MinMax()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = bodyPart.bloodMax;
    }
}
