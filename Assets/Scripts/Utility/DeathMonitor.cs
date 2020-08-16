using UnityEngine;

public class DeathMonitor : MonoBehaviour
{

    public BodyPart brain;
    public bool isTimePassing;
    public float brainDeadDuration;
    public float brainDeadDurationLimit;
    private Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        clock = FindObjectOfType<Clock>();
        brainDeadDuration = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isTimePassing)
        {
            if (IsBrainDead())
            {
                brainDeadDuration += Time.deltaTime * clock.globalTimeScalingFactor;
            }
        }


        if (brainDeadDuration >= brainDeadDurationLimit)
        {
            Debug.Log("Game Over: Brain Death");
        }
        */
    }

    private bool IsBrainDead()
    {

        return brain.damage >= brain.damageMax;
    }
}
