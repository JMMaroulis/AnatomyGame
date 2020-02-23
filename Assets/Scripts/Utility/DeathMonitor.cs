using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMonitor : MonoBehaviour
{

    public BodyPart brain;
    public bool isTimePassing;
    public float brainDeadDuration;
    public float brainDeadDurationLimit;

    // Start is called before the first frame update
    void Start()
    {
        brainDeadDuration = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimePassing)
        {
            if (IsBrainDead())
            {
                brainDeadDuration += Time.deltaTime;
            }
        }


        if (brainDeadDuration >= brainDeadDurationLimit)
        {
            Debug.Log("Game Over: Brain Death");
        }
    }

    private bool IsBrainDead()
    {
        return brain.damage >= brain.damageMax;
    }
}
