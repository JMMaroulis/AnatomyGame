using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    private DeathMonitor deathMonitor;
    private LifeMonitor lifeMonitor;

    //TODO: find some way to get this thing working as a public bool in the inspector with properties, rather than just updating every bodypart value every frame
    public bool isTimePassing; // { get { return _isTimePassing; } set { _isTimePassing = value; BodyPartsTimePaassing(); } }

    private float timeElapsed;
    public float clockTarget;
    public float globalTimeScalingFactor;
    public bool actionCancelFlag;

    public Text currentTime;
    public Text clockText;
    public Slider clockSlider;

    private BodyPartManager bodyPartManager;

    // Start is called before the first frame update
    void Start()
    {
        deathMonitor = FindObjectOfType<DeathMonitor>();
        lifeMonitor = FindObjectOfType<LifeMonitor>();
        bodyPartManager = FindObjectOfType<BodyPartManager>();

        //Time.timeScale = globalTimeScalingFactor;
        isTimePassing = false;
        currentTime.text = "00:00:00";
        clockTarget = 0.0f;
        timeElapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        globalTimeScalingFactor = clockSlider.value;
        //Time.timeScale = globalTimeScalingFactor;
        clockText.text = $"Time Scaling: {globalTimeScalingFactor}x";

        //checking if it's time for time to stop yet
        if (timeElapsed >= clockTarget)
        {
            isTimePassing = false;
        }

        BodyPartsTimePassing();
        deathMonitor.isTimePassing = isTimePassing;
        lifeMonitor.isTimePassing = isTimePassing;

        UpdateCurrentTime();       

    }

    //would be great if we could trigger this on isTimePassing changing, rather than invoking it manually every frame
    public void BodyPartsTimePassing()
    {

        foreach (BodyPart bodyPart in bodyPartManager.bodyParts)
        {
            bodyPart.isTimePassing = isTimePassing;
        }

    }

    //sets time passing for all bodyparts for [ingameSeconds] of IN-GAME-TIME
    public void StartClockUntil(float ingameSeconds)
    {
        isTimePassing = true;
        clockTarget += ingameSeconds;
    }

    public void StopClock()
    {
        actionCancelFlag = true;
        clockTarget = timeElapsed;
    }

    void UpdateCurrentTime()
    {
        if (isTimePassing)
        {
            timeElapsed += Time.deltaTime * globalTimeScalingFactor;
            int timeElapsedTemp = (int)timeElapsed;
            currentTime.text = string.Format("{0:00}:{1:00}:{2:00}", timeElapsedTemp / 3600, (timeElapsedTemp / 60) % 60, timeElapsedTemp % 60);
        }
    }

}
