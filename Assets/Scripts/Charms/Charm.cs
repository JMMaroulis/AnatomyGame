using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Charm : MonoBehaviour
{

    public float expiryTime;
    public float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0.0f;
        expiryTime = 60.0f * 30.0f;
    }

    public void CharmTimer(float seconds)
    {
        timeElapsed += seconds;
        if (timeElapsed >= expiryTime)
        {
            Destroy(this);
        }
    }

}
