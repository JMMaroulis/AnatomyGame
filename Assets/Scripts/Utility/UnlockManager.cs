using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public List<Button> buttons;

    public Button continueButton;
    public Text continueButtonText;

    public Text goldText;
    public Text malpracticeText;

    private UnlockTracker unlockTracker;
    private GoldTracker goldTracker;
    private GameSetupScenarioTracker gameSetupScenarioTracker;

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
        FindObjectOfType<PhysicalInjuryGenerator>().GenerateInjuries();
        goldTracker = FindObjectOfType<GoldTracker>();
        gameSetupScenarioTracker = FindObjectOfType<GameSetupScenarioTracker>();

        unlockTracker = FindObjectOfType<UnlockTracker>();
        UnityEngine.Events.UnityAction action = null;

        action = () => { unlockTracker.medicine_poison = true; FindObjectOfType<GoldTracker>().goldSpent += medicine_poison_cost; };
        buttons[0].onClick.AddListener(action);
        buttons[0].transform.GetChild(0).GetComponent<Text>().text = $"Poison Medication License: {medicine_poison_cost} Gold";

        action = () => { unlockTracker.medicine_speed = true; FindObjectOfType<GoldTracker>().goldSpent += medicine_speed_cost; };
        buttons[1].onClick.AddListener(action);
        buttons[1].transform.GetChild(0).GetComponent<Text>().text = $"Temporal Medication License: {medicine_speed_cost} Gold";

        action = () => { unlockTracker.medicine_blood = true; FindObjectOfType<GoldTracker>().goldSpent += medicine_blood_cost; };
        buttons[2].onClick.AddListener(action);
        buttons[2].transform.GetChild(0).GetComponent<Text>().text = $"Internal Bleeding Medication License: {medicine_blood_cost} Gold";

        action = () => { unlockTracker.charms_heart = true; FindObjectOfType<GoldTracker>().goldSpent += heartcharm_cost; };
        buttons[3].onClick.AddListener(action);
        buttons[3].transform.GetChild(0).GetComponent<Text>().text = $"Heart Charm License: {heartcharm_cost} Gold";

        action = () => { unlockTracker.charms_lung = true; FindObjectOfType<GoldTracker>().goldSpent += lungcharm_cost; };
        buttons[4].onClick.AddListener(action);
        buttons[4].transform.GetChild(0).GetComponent<Text>().text = $"Lung Charm License: {lungcharm_cost} Gold";

        action = () => { unlockTracker.charms_blood_regen = true; FindObjectOfType<GoldTracker>().goldSpent += bloodregencharm_cost; };
        buttons[5].onClick.AddListener(action);
        buttons[5].transform.GetChild(0).GetComponent<Text>().text = $"Blood Regeneration Charm License: {bloodregencharm_cost} Gold";

        action = () => { unlockTracker.charms_petrification = true; FindObjectOfType<GoldTracker>().goldSpent += petrificationcharm_cost; };
        buttons[6].onClick.AddListener(action);
        buttons[6].transform.GetChild(0).GetComponent<Text>().text = $"Petrification Charm License: {petrificationcharm_cost} Gold";

        action = () => { unlockTracker.spawn = true; FindObjectOfType<GoldTracker>().goldSpent += bodypartdelivery_cost; };
        buttons[7].onClick.AddListener(action);
        buttons[7].transform.GetChild(0).GetComponent<Text>().text = $"Bodypart Delivery Service: {bodypartdelivery_cost} Gold";

        action = () => { unlockTracker.spawn_clock = true; FindObjectOfType<GoldTracker>().goldSpent += clockworkdelivery_cost; };
        buttons[8].onClick.AddListener(action);
        buttons[8].transform.GetChild(0).GetComponent<Text>().text = $"Clockwork Delivery Service: {clockworkdelivery_cost} Gold";

        if (gameSetupScenarioTracker.unhappyPatients >= 3)
        {
            continueButton.interactable = false;
            continueButtonText.text = "Too many malpractice lawsuits! Career over!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = $"Gold: {goldTracker.gold}";
        malpracticeText.text = $"Malpractice Lawsuits Ongoing: {gameSetupScenarioTracker.unhappyPatients}";

        if (unlockTracker.medicine_poison || goldTracker.gold < medicine_poison_cost)
        {
            buttons[0].interactable = false;
        }
        if (unlockTracker.medicine_speed || goldTracker.gold < medicine_speed_cost)
        {
            buttons[1].interactable = false;
        }
        if (unlockTracker.medicine_blood || goldTracker.gold < medicine_blood_cost)
        {
            buttons[2].interactable = false;
        }
        if (unlockTracker.charms_heart || goldTracker.gold < heartcharm_cost)
        {
            buttons[3].interactable = false;
        }
        if (unlockTracker.charms_lung || goldTracker.gold < lungcharm_cost)
        {
            buttons[4].interactable = false;
        }
        if (unlockTracker.charms_blood_regen || goldTracker.gold < bloodregencharm_cost)
        {
            buttons[5].interactable = false;
        }
        if (unlockTracker.charms_petrification || goldTracker.gold < petrificationcharm_cost)
        {
            buttons[6].interactable = false;
        }
        if (unlockTracker.spawn || goldTracker.gold < bodypartdelivery_cost)
        {
            buttons[7].interactable = false;
        }
        if (unlockTracker.spawn_clock || goldTracker.gold < clockworkdelivery_cost)
        {
            buttons[8].interactable = false;
        }

    }

    public void PerkScreen()
    {
        FindObjectOfType<SceneTransitionManager>().PerkScreen();
    }

}
