using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public List<Button> buttons;
    public Text goldText;

    private UnlockTracker unlockTracker;

    public int medicine_poison_cost;
    public int medicine_speed_cost;
    public int medicine_blood_cost;
    public int bodypartdelivery_cost;
    public int heartcharm_cost;
    public int lungcharm_cost;
    public int bloodregencharm_cost;
    public int petrificationcharm_cost;
    public int clockworkdelivery_cost;



    // Start is called before the first frame update
    void Start()
    {
        unlockTracker = FindObjectOfType<UnlockTracker>();
        UnityEngine.Events.UnityAction action = null;

        int gold = FindObjectOfType<GoldTracker>().goldAccumulated - FindObjectOfType<GoldTracker>().goldSpent;
        goldText.text = $"Gold: {gold}";

        if (unlockTracker.medicine_poison || gold < medicine_poison_cost)
        {
            buttons[0].interactable = false;
        }
        action = () => { unlockTracker.medicine_poison = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += medicine_poison_cost; };
        buttons[0].onClick.AddListener(action);
        buttons[0].transform.GetChild(0).GetComponent<Text>().text = $"Poison Medication License: {medicine_poison_cost} Gold";


        if (unlockTracker.medicine_speed || gold < medicine_speed_cost)
        {
            buttons[1].interactable = false;
        }
        action = () => { unlockTracker.medicine_speed = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += medicine_speed_cost; };
        buttons[1].onClick.AddListener(action);
        buttons[1].transform.GetChild(0).GetComponent<Text>().text = $"Temporal Medication License: {medicine_speed_cost} Gold";


        if (unlockTracker.medicine_blood || gold < medicine_blood_cost)
        {
            buttons[2].interactable = false;
        }
        action = () => { unlockTracker.medicine_blood = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += medicine_blood_cost; };
        buttons[2].onClick.AddListener(action);
        buttons[2].transform.GetChild(0).GetComponent<Text>().text = $"Blood Medication License: {medicine_blood_cost} Gold";


        if (unlockTracker.charms_heart || gold < heartcharm_cost)
        {
            buttons[3].interactable = false;
        }
        action = () => { unlockTracker.charms_heart = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += heartcharm_cost; };
        buttons[3].onClick.AddListener(action);
        buttons[3].transform.GetChild(0).GetComponent<Text>().text = $"Heart Charm License: {heartcharm_cost} Gold";


        if (unlockTracker.charms_lung || gold < lungcharm_cost)
        {
            buttons[4].interactable = false;
        }
        action = () => { unlockTracker.charms_lung = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += lungcharm_cost; };
        buttons[4].onClick.AddListener(action);
        buttons[4].transform.GetChild(0).GetComponent<Text>().text = $"Lung Charm License: {lungcharm_cost} Gold";


        if (unlockTracker.charms_blood_regen || gold < bloodregencharm_cost)
        {
            buttons[5].interactable = false;
        }
        action = () => { unlockTracker.charms_blood_regen = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += bloodregencharm_cost; };
        buttons[5].onClick.AddListener(action);
        buttons[5].transform.GetChild(0).GetComponent<Text>().text = $"Blood Regeneration Charm License: {bloodregencharm_cost} Gold";


        if (unlockTracker.charms_petrification || gold < petrificationcharm_cost)
        {
            buttons[6].interactable = false;
        }
        action = () => { unlockTracker.charms_petrification = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += petrificationcharm_cost; };
        buttons[6].onClick.AddListener(action);
        buttons[6].transform.GetChild(0).GetComponent<Text>().text = $"Petrification Charm License: {petrificationcharm_cost} Gold";


        if (unlockTracker.spawn || gold < bodypartdelivery_cost)
        {
            buttons[7].interactable = false;
        }
        action = () => { unlockTracker.spawn = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += bodypartdelivery_cost; };
        buttons[7].onClick.AddListener(action);
        buttons[7].transform.GetChild(0).GetComponent<Text>().text = $"Bodypart Delivery Service: {bodypartdelivery_cost} Gold";


        if (unlockTracker.spawn_clock || gold < clockworkdelivery_cost)
        {
            buttons[8].interactable = false;
        }
        action = () => { unlockTracker.spawn_clock = true; LevelStart(); FindObjectOfType<GoldTracker>().goldSpent += clockworkdelivery_cost; };
        buttons[8].onClick.AddListener(action);
        buttons[8].transform.GetChild(0).GetComponent<Text>().text = $"Clockwork Delivery Service: {clockworkdelivery_cost} Gold";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelStart()
    {
        FindObjectOfType<LevelManager>().LevelStart();
    }

}
