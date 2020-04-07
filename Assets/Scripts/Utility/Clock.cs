using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    public GameObject body;
    public DeathMonitor deathMonitor;
    public LifeMonitor lifeMonitor;
    private List<BodyPart> bodyParts = new List<BodyPart>();

    //TODO: find some way to get this thing working as a public bool in the inspector with properties, rather than just updating every bodypart value every frame
    public bool isTimePassing; // { get { return _isTimePassing; } set { _isTimePassing = value; BodyPartsTimePaassing(); } }
    //private bool _isTimePassing;

    public float globalTimeScalingFactor;
    private float timeUntilClockStops = 0.0f;
    private float timeElapsed = 0.0f;
    public Text currentTime;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = globalTimeScalingFactor;
        isTimePassing = false;
        PopulateBodyPartsList();
        currentTime.text = "00:00:00";
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = globalTimeScalingFactor;
        PopulateBodyPartsList();

        //checking if it's time for time to stop yet
        timeUntilClockStops = Mathf.Max(timeUntilClockStops - Time.deltaTime, 0.0f);
        if (timeUntilClockStops <= 0.0f)
        {
            isTimePassing = false;
        }

        BodyPartsTimePassing();
        deathMonitor.isTimePassing = isTimePassing;
        lifeMonitor.isTimePassing = isTimePassing;


        UpdateCurrentTime();
    }


    void PopulateBodyPartsList()
    {
        bodyParts = FindObjectsOfType<BodyPart>().ToList<BodyPart>();
    }

    //would be great if we could trigger this on isTimePassing changing, rather than invoking it manually every frame
    public void BodyPartsTimePassing()
    {

        foreach (BodyPart bodyPart in bodyParts)
        {
            bodyPart.isTimePassing = isTimePassing;
        }

    }

    //sets time passing for all bodyparts for [ingameSeconds] of IN-GAME-TIME
    public void StartClockUntil(float ingameSeconds)
    {
        isTimePassing = true;
        timeUntilClockStops = ingameSeconds;
    }

    public static IEnumerator WaitForSecondsStatic(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void UpdateCurrentTime()
    {
        if (isTimePassing)
        {
            timeElapsed += Time.deltaTime;
            int timeElapsedTemp = (int)timeElapsed;
            currentTime.text = string.Format("{0:00}:{1:00}:{2:00}", timeElapsedTemp / 3600, (timeElapsedTemp / 60) % 60, timeElapsedTemp % 60);
        }
    }

}
