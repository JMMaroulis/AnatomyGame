using UnityEngine;

public class Charm : MonoBehaviour
{

    public float expiryTime;
    public float timeElapsed;
    public Clock clock;

    public BodyPartManager bodyPartManager;

    // Start is called before the first frame update
    void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        clock = FindObjectOfType<Clock>();
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
