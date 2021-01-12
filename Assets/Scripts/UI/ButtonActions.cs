using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class ButtonActions : MonoBehaviour
{
    public List<Button> menuButtons;
    public List<Button> menuTabs;


    public GameObject selectedGameObject = null;
    public GameObject body;

    public TextLog textLog;
    public Text examineBox;

    private int bodyPartMenuCounter = 0;

    public LifeMonitor lifeMonitor;
    public ActionTimeBar actionTimeBar;

    private GoldTracker goldTracker;
    private UnlockTracker unlocks;
    private Clock clock;
    private float secondCounter;
    private BodyPartManager bodyPartManager;


    public Button bloodButton;
    public Button surgeryButton;
    public Button medicineButton;
    public Button charmsButton;
    public Button spawnLimbButton;
    public Button spawnOrganButton;
    public Button spawnClockLimbButton;
    public Button spawnClockOrganButton;
    public Button waitButton;
    public Button cancelActionButton;

    // Start is called before the first frame update
    void Start()
    {
        goldTracker = FindObjectOfType<GoldTracker>();
        clock = FindObjectOfType<Clock>();
        bodyPartManager = FindObjectOfType<BodyPartManager>();

        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        examineBox.text = "";
        cancelActionButton.interactable = false;

        EnableMenuTabs();
        UpdateMenuTabsInteractivity();

    }

    //enable/disable button tabs based on unlocks
    void EnableMenuTabs()
    {
        unlocks = FindObjectOfType<UnlockTracker>();
        bloodButton.gameObject.SetActive(unlocks.blood);
        surgeryButton.gameObject.SetActive(unlocks.surgery);
        medicineButton.gameObject.SetActive(true); //healing potions always required
        charmsButton.gameObject.SetActive(unlocks.charms_petrification || unlocks.charms_heart || unlocks.charms_lung || unlocks.charms_blood_regen); //charm permissions on individual level
        spawnLimbButton.gameObject.SetActive(unlocks.spawn);
        spawnOrganButton.gameObject.SetActive(unlocks.spawn);
        spawnClockLimbButton.gameObject.SetActive(unlocks.spawn_clock);
        spawnClockOrganButton.gameObject.SetActive(unlocks.spawn_clock);
        waitButton.gameObject.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {

        //update bodypart examination textbox every x seconds
        secondCounter += Time.unscaledDeltaTime;
        if(secondCounter >= 0.3f || examineBox.text == "")
        {
            ExamineSelectedGameObject();
            secondCounter = 0.0f;
        }
     
    }

    public void DisableAllButtons()
    {
        bloodButton.interactable = false;
        surgeryButton.interactable = false;
        medicineButton.interactable = false;
        charmsButton.interactable = false;
        spawnLimbButton.interactable = false;
        spawnOrganButton.interactable = false;
        spawnClockLimbButton.interactable = false;
        spawnClockOrganButton.interactable = false;
        waitButton.interactable = false;

        menuButtons[0].interactable = false;
        menuButtons[1].interactable = false;
        menuButtons[2].interactable = false;
        menuButtons[3].interactable = false;
        menuButtons[4].interactable = false;
        menuButtons[5].interactable = false;
        menuButtons[6].interactable = false;
        menuButtons[7].interactable = false;
    }

    public void UpdateMenuTabsInteractivity()
    {
        if (clock.isTimePassing)
        {
            bloodButton.interactable = false;
            surgeryButton.interactable = false;
            medicineButton.interactable = false;
            charmsButton.interactable = false;
            spawnLimbButton.interactable = false;
            spawnOrganButton.interactable = false;
            spawnClockLimbButton.interactable = false;
            spawnClockOrganButton.interactable = false;
            waitButton.interactable = false;
            return;
        }

        else if (selectedGameObject == null)
        {
            bloodButton.interactable = false;
            surgeryButton.interactable = false;
            medicineButton.interactable = false;
            charmsButton.interactable = false;
            spawnLimbButton.interactable = true;
            spawnOrganButton.interactable = true;
            spawnClockLimbButton.interactable = true;
            spawnClockOrganButton.interactable = true;
            waitButton.interactable = true;
            return;
        }

        else if (selectedGameObject == null)
        {
            bloodButton.interactable = false;
            surgeryButton.interactable = false;
            medicineButton.interactable = false;
            charmsButton.interactable = false;
            spawnLimbButton.interactable = true;
            spawnOrganButton.interactable = true;
            spawnClockLimbButton.interactable = true;
            spawnClockOrganButton.interactable = true;
            waitButton.interactable = true;
            return;
        }

        else if (!(selectedGameObject.GetComponent<BodyPart>() is null) && (selectedGameObject.GetComponent<Organ>() is null))
        {
            bloodButton.interactable = true;
            surgeryButton.interactable = true;
            medicineButton.interactable = true;
            charmsButton.interactable = true;
            spawnLimbButton.interactable = true;
            spawnOrganButton.interactable = true;
            spawnClockLimbButton.interactable = true;
            spawnClockOrganButton.interactable = true;
            waitButton.interactable = true;
            return;
        }

        else if (!(selectedGameObject.GetComponent<Organ>() is null))
        {
            bloodButton.interactable = true;
            surgeryButton.interactable = true;
            medicineButton.interactable = true;
            charmsButton.interactable = true;
            spawnLimbButton.interactable = true;
            spawnOrganButton.interactable = true;
            spawnClockLimbButton.interactable = true;
            spawnClockOrganButton.interactable = true;
            waitButton.interactable = true;
            return;
        }

        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            bloodButton.interactable = false;
            surgeryButton.interactable = true;
            medicineButton.interactable = false;
            charmsButton.interactable = false;
            spawnLimbButton.interactable = true;
            spawnOrganButton.interactable = true;
            spawnClockLimbButton.interactable = true;
            spawnClockOrganButton.interactable = true;
            waitButton.interactable = true;
            return;
        }
    }

    //remove all text and actions from all buttons
    public void ClearAllButtons()
    {
        foreach (Button button in menuButtons)
        {
            button.onClick.RemoveAllListeners();
            button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
            button.transform.GetComponentInChildren<MouseOver>().mouseoverEnabled = false;
            button.transform.GetComponentInChildren<MouseOver>().ResetTimer();
        }
        UpdateMenuTabsInteractivity();
    }

    public void UpdateMenuButtonsInteractivity(bool interactable)
    {
        if (clock.isTimePassing)
        {
            interactable = false;
        }

        foreach (Button button in menuButtons)
        {
            button.interactable = interactable;
        }
    }

    public void ActionInProgress()
    {
        UpdateMenuButtonsInteractivity(false);
        UpdateMenuTabsInteractivity();
        cancelActionButton.interactable = true;
    }

    public void ActionFinished()
    {
        UpdateMenuButtonsInteractivity(true);
        UpdateMenuTabsInteractivity();
        cancelActionButton.interactable = false;
    }

    //set as onclick in editor
    public void CancelAction()
    {
        clock.StopClock();
        actionTimeBar.Reset(1.0f);
    }

    public void SetButtonMouseoverText(Button button, string text)
    {
        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = text;
    }

    //set as onclick in editor
    public void SelectSurgeryAction()
    {
        if (selectedGameObject == null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
            return;
        }
        else if (!(selectedGameObject.GetComponent<Organ>() is null))
        {
            SelectSurgeryActionOptions_Organ();
        }
        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            SelectSurgeryActionOptions_EmbeddedObject();
        }
        else if (!(selectedGameObject.GetComponent<BodyPart>() is null))
        {
            SelectSurgeryActionOptions_BodyPart();
        }
    }

    private void SelectSurgeryActionOptions_BodyPart()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        BodyPart bodypart = selectedGameObject.GetComponent<BodyPart>();

        AssignRemoveBodyPartButton(menuButtons[0], bodypart);
        if (bodypart.connectedBodyParts.Count() == 0)
        {
            menuButtons[0].interactable = false;
        }

        AssignAttachBodyPartButton(menuButtons[1], bodypart);

        AssignDestroyBodyPart(menuButtons[2], bodypart);
        if (bodypart.connectedBodyParts.Count() != 0)
        {
            menuButtons[2].interactable = false;
        }
    }

    private void SelectSurgeryActionOptions_Organ()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        Organ organ = (Organ)selectedGameObject.GetComponent<BodyPart>();

        AssignRemoveOrganButton(menuButtons[0], organ);
        if (organ.connectedBodyParts.Count() == 0)
        {
            menuButtons[0].interactable = false;
        }

        AssignImplantOrganOptions(menuButtons[1]);
        if (organ.connectedBodyParts.Count() != 0)
        {
            menuButtons[1].interactable = false;
        }

        AssignDestroyOrgan(menuButtons[2], organ);
        if (organ.connectedBodyParts.Count() != 0)
        {
            menuButtons[2].interactable = false;
        }
    }

    private void SelectSurgeryActionOptions_EmbeddedObject()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        EmbeddedObject embeddedObject = selectedGameObject.GetComponent<EmbeddedObject>();

        AssignRemoveEmbeddedObjectButton(menuButtons[0], embeddedObject);
        if (embeddedObject.parentBodyPart is null)
        {
            menuButtons[0].interactable = false;
        }

        AssignImplantEmbeddedObjectOptions(menuButtons[1]);
        if (!(embeddedObject.parentBodyPart is null))
        {
            menuButtons[1].interactable = false;
        }
        //AssignDestroyEmbeddedObject(menuButtons[2], selectedGameObject.GetComponent<EmbeddedObject>());
    }


    //set as onclick in editor
    public void SelectWaitActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        AssignWaitTenSeconds(menuButtons[0]);
        AssignWaitThirtySeconds(menuButtons[1]);
        AssignWaitOneMinute(menuButtons[2]);
        AssignWaitFiveMinutes(menuButtons[3]);
        AssignWaitTenMinutes(menuButtons[4]);
        AssignWaitThirtyMinutes(menuButtons[5]);
        AssignDischargePatient(menuButtons[6]);
    }

    //set as onclick in editor
    public void SelectBloodAction()
    {
        if (selectedGameObject == null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
            return;
        }
        else if (selectedGameObject.GetComponent<BodyPart>() is Organ)
        {
            SelectBloodActionOptions_Organ();
        }
        else if (!(selectedGameObject.GetComponent<BodyPart>() is null))
        {
            SelectBloodActionOptions_BodyPart();
        }
        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            SelectBloodActionOptions_EmbeddedObject();
        }
    }

    private void SelectBloodActionOptions_BodyPart()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        AssignBandagesButton(menuButtons[0], selectedGameObject.GetComponent<BodyPart>());
        AssignBloodlettingButton(menuButtons[1], selectedGameObject.GetComponent<BodyPart>());
        AssignAddBloodButton(menuButtons[2], selectedGameObject.GetComponent<BodyPart>());
        AssignRemoveBloodButton(menuButtons[3], selectedGameObject.GetComponent<BodyPart>());
    }

    private void SelectBloodActionOptions_Organ()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        AssignAddBloodButton(menuButtons[0], selectedGameObject.GetComponent<BodyPart>());
        AssignRemoveBloodButton(menuButtons[1], selectedGameObject.GetComponent<BodyPart>());
    }

    private void SelectBloodActionOptions_EmbeddedObject()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(false);

        Debug.Log("AAAAA");
    }



    //set as onclick in editor
    public void SelectMedicineAction()
    {
        if (selectedGameObject == null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
            return;
        }
        else if (selectedGameObject.GetComponent<BodyPart>() is Organ)
        {
            SelectMedicineActionOptions_Organ();
        }
        else if (!(selectedGameObject.GetComponent<BodyPart>() is null))
        {
            SelectMedicineActionOptions_BodyPart();
        }
        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            SelectMedicineActionOptions_EmbeddedObject();
        }
    }

    private void SelectMedicineActionOptions_BodyPart()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        int n = 0;

        AssignInjectHealthPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        if (unlocks.medicine_blood) 
        {
            AssignInjectCoagulantPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.medicine_poison) 
        {
            AssignInjectAntidoteButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
            AssignInjectSlowPoisonButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.medicine_speed)
        {
            AssignInjectStasisPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
            AssignInjectHastePotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
 
    }

    private void SelectMedicineActionOptions_Organ()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        int n = 0;

        AssignInjectHealthPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        if (unlocks.medicine_blood)
        {
            AssignInjectCoagulantPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.medicine_poison)
        {
            AssignInjectAntidoteButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
            AssignInjectSlowPoisonButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.medicine_speed)
        {
            AssignInjectStasisPotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
            AssignInjectHastePotionButton(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
    }

    private void SelectMedicineActionOptions_EmbeddedObject()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(false);

        Debug.Log("AAAAA");
    }

    //set as onclick in editor
    public void SelectCharmAction()
    {
        if (selectedGameObject == null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
            return;
        }
        else if (selectedGameObject.GetComponent<BodyPart>() is Organ)
        {
            SelectCharmActionOptions_Organ();
        }
        else if (!(selectedGameObject.GetComponent<BodyPart>() is null))
        {
            SelectCharmActionOptions_BodyPart();
        }
        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            SelectCharmActionOptions_EmbeddedObject();
        }

    }

    private void SelectCharmActionOptions_BodyPart()
    {
        int n = 0;
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        if (unlocks.charms_heart)
        {
            AssignApplyHeartCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_lung)
        {
            AssignApplyLungCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_blood_regen)
        {
            AssignApplyBloodRegenCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_petrification)
        {
            AssignApplyPetrificationCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
    }

    private void SelectCharmActionOptions_Organ()
    {
        int n = 0;
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        if (unlocks.charms_heart)
        {
            AssignApplyHeartCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_lung)
        {
            AssignApplyLungCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_blood_regen)
        {
            AssignApplyBloodRegenCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
        if (unlocks.charms_petrification)
        {
            AssignApplyPetrificationCharm(menuButtons[n], selectedGameObject.GetComponent<BodyPart>()); n++;
        }
    }

    private void SelectCharmActionOptions_EmbeddedObject()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(false);

        Debug.Log("AAAAA");
    }

    //set as onclick in editor
    public void SelectSpawnOrganActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        float seconds = 60.0f * 5.0f;
        int goldCost = 250;

        AssignSpawnBodyPartTemplate(menuButtons[0], goldCost, seconds, Actions_SpawnBodyParts.SpawnHeart, "Heart");
        AssignSpawnBodyPartTemplate(menuButtons[1], goldCost, seconds, Actions_SpawnBodyParts.SpawnLeftLung, "Left Lung");
        AssignSpawnBodyPartTemplate(menuButtons[2], goldCost, seconds, Actions_SpawnBodyParts.SpawnRightLung, "Right Lung");
        AssignSpawnBodyPartTemplate(menuButtons[3], goldCost, seconds, Actions_SpawnBodyParts.SpawnBrain, "Brain");
        AssignSpawnBodyPartTemplate(menuButtons[4], goldCost, seconds, Actions_SpawnBodyParts.SpawnLeftEye, "Left Eye");
        AssignSpawnBodyPartTemplate(menuButtons[5], goldCost, seconds, Actions_SpawnBodyParts.SpawnRightEye, "Right Eye");
        AssignSpawnBodyPartTemplate(menuButtons[6], goldCost, seconds, Actions_SpawnBodyParts.SpawnLiver, "Liver");
        AssignSpawnBodyPartTemplate(menuButtons[7], goldCost, seconds, Actions_SpawnBodyParts.SpawnStomach, "Stomach");

    }

    //set as onclick in editor
    public void SelectSpawnClockworkOrganActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        float seconds = 60.0f * 5.0f;
        int goldCost = 150;

        AssignSpawnClockworkTemplate(menuButtons[0], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkHeart, "Clockwork Heart");
        AssignSpawnClockworkTemplate(menuButtons[1], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkLeftLung, "Clockwork Left Lung");
        AssignSpawnClockworkTemplate(menuButtons[2], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkRightLung, "Clockwork Right Lung");
        AssignSpawnClockworkTemplate(menuButtons[3], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkBrain, "Clockwork Brain");
        AssignSpawnClockworkTemplate(menuButtons[4], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkLeftEye, "Clockwork Left Eye");
        AssignSpawnClockworkTemplate(menuButtons[5], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkRightEye, "Clockwork Right Eye");
        AssignSpawnClockworkTemplate(menuButtons[6], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkLiver, "Clockwork Liver");
        AssignSpawnClockworkTemplate(menuButtons[7], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkStomach, "Clockwork Stomach");
    }

    //set as onclick in editor
    public void SelectSpawnClockworkLimbActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        float seconds = 60.0f * 5.0f;
        int goldCost = 150;

        AssignSpawnClockworkTemplate(menuButtons[0], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkLeftArm, "Clockwork Left Arm");
        AssignSpawnClockworkTemplate(menuButtons[1], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkRightArm, "Clockwork Right Arm");
        AssignSpawnClockworkTemplate(menuButtons[2], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkLeftLeg, "Clockwork Left Leg");
        AssignSpawnClockworkTemplate(menuButtons[3], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkRightLeg, "Clockwork Right Leg");
        AssignSpawnClockworkTemplate(menuButtons[4], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkHead, "Clockwork Head");
        AssignSpawnClockworkTemplate(menuButtons[5], goldCost, seconds, Actions_SpawnBodyParts.SpawnClockworkTorso, "Clockwork Torso");
    }


    //set as onclick in editor
    public void SelectSpawnBodyPartActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        float seconds = 60.0f * 5.0f;
        int goldCost = 250;

        AssignSpawnBodyPartTemplate(menuButtons[0], goldCost, seconds, Actions_SpawnBodyParts.SpawnLeftArm, "Left Arm");
        AssignSpawnBodyPartTemplate(menuButtons[1], goldCost, seconds, Actions_SpawnBodyParts.SpawnRightArm, "Right Arm");
        AssignSpawnBodyPartTemplate(menuButtons[2], goldCost, seconds, Actions_SpawnBodyParts.SpawnLeftLeg, "Left Leg");
        AssignSpawnBodyPartTemplate(menuButtons[3], goldCost, seconds, Actions_SpawnBodyParts.SpawnRightLeg, "Right Leg");
        AssignSpawnBodyPartTemplate(menuButtons[4], goldCost, seconds, Actions_SpawnBodyParts.SpawnHead, "Head");
        AssignSpawnBodyPartTemplate(menuButtons[5], goldCost, seconds, Actions_SpawnBodyParts.SpawnTorso, "Torso");
    }

    //set as onclick in editor
    public void SelectSpawnObjectActionOptions()
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
    }


    #region Medicine

    void AssignInjectHealthPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 30;
        int healthPotion = 100;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "health", healthPotion, seconds, goldCost);
                textLog.NewLogEntry($"Injecting {healthPotion} units of Health Potion into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Health Potion ({healthPotion} units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Health Potion: Processed at 1/5 units per second, heals one damage per unit.\nIf bodypart is undamaged, processed at 1/100 units per second.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignInjectAntidoteButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 30;
        float units = 100.0f;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "antidote", units, seconds, goldCost);
                textLog.NewLogEntry($"Injecting {units} units of Antidote into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Antidote ({units} units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Antidote: Mutually neutralises 1 unit of slow poison per second. Decays at 1/100 units per second if no poison present in the bodypart.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignInjectSlowPoisonButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "slow poison", 50.00f, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Slow Poison into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Slow Poison (50 units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Slow Poison: Deals 1/1000 damage per unit per second. Slowly processed by liver.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignInjectStasisPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "stasis", 50.00f, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Stasis Potion into the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Stasis Potion (50 units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Decreases local rate of time passing in bodypart. Decays at 1/100 units per second, unaffected by own temporal slowing.\nWARNING: Effect of high dosages extremely dangerous and unpredictable.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignInjectHastePotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "haste", 50.00f, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Haste Potion into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Haste Potion (50 units): {seconds} seconds, {goldCost} gold";


        string mouseoverText = "Increases local rate of time passing in bodypart. Decays at 1/100 units per second, unaffected by own temporal acceleration.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignInjectCoagulantPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Medicine.InjectPotion(bodypart, "coagulant", 50.00f, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Coagulant Potion into the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Coagulant Potion (50 units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Decreases bloodloss rate by 0.5 units per second. Decays at 1/100 unit per second, if no bloodloss to be healed.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #endregion

    #region Charms

    void AssignApplyHeartCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Charms.ApplyHeartCharm(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Applying a Heart Charm to the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Heart Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Heart Charm: Causes the bodypart to function as a heart for the duration.";
        SetButtonMouseoverText(button, mouseoverText);

    }

    void AssignApplyLungCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Charms.ApplyLungCharm(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Applying a Lung Charm to the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Lung Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Lung Charm: Causes the bodypart to function as a lung for the duration.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignApplyPetrificationCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Charms.ApplyPetrificationCharm(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Applying a Petrification Charm to the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };
        
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Petrification Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Petrification Charm: The bodypart ceases all functions for the duration.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignApplyBloodRegenCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Charms.ApplyBloodRegenCharm(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Applying a Blood Regeneration Charm to the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Blood Regen Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Blood Regen Charm: Produces 30 units of blood per second for the duration, up to the bodypart maximum capacity.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #endregion

    #region Blood

    void AssignBandagesButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 10;
        int bloodLossReduction = 10;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Blood.Bandages(bodypart, seconds, goldCost, bloodLossReduction);
                textLog.NewLogEntry($"Applying bandages to the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bandages: {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Reduced bloodloss by {bloodLossReduction} units per second.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignBloodlettingButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 0;
        int bloodLossInduction = 10;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Blood.Bloodletting(bodypart, seconds, goldCost, bloodLossInduction); 
                textLog.NewLogEntry($"Inducing bleeding in the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bloodletting: {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Induces bloodloss of {bloodLossInduction} units per second.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignAddBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 20;
        int blood = 500;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_Blood.AddBlood(bodypart, seconds, goldCost, blood);
                textLog.NewLogEntry($"Injecting {blood} units of blood into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Blood: ({blood} units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Injects {blood} units of blood.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignRemoveBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 0;
        int blood = 500;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Blood.RemoveBlood(bodypart, seconds, goldCost, blood); 
                textLog.NewLogEntry($"Extracting 100 units of blood from the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };
        
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Remove Blood: ({blood} units): {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Removes {blood} units of blood.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #endregion

    #region BodyPartSurgery

    void AssignRemoveBodyPartButton(Button button, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
                return;
            }
            else if (bodypart.connectedBodyParts.Count() == 0)
            {
                textLog.NewLogEntry("That isn't connected to anything!");
            }
            else if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Surgery.RemoveBodyPart(bodypart, seconds, goldCost, damage, bloodLossRate);
                textLog.NewLogEntry($"Removing the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Amputate Bodypart: {seconds} seconds, {goldCost} gold";

        string mouseoverText = 
            "Disconnects the bodypart from all connected bodyparts.\n" +
            "Causes bloodloss in this and connected bodyparts.\n" +
            "Causes damage in this and connected bodyparts.";

        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignAttachBodyPartButton(Button button, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select a bodypart for that!");
                return;
            }
            else
            {
                SelectBodyPartToAttachOptions(bodypart);
            }
        };

        bodyPartMenuCounter = 0;
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ATTACH TO... (10 minutes)";
    }

    void SelectBodyPartToAttachOptions(BodyPart bodypart)
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while (menuCount < menuButtons.Count() && bodyPartMenuCounter < bodyPartManager.bodyParts.Count())
        {
            Button menuButton = menuButtons[menuCount];
            BodyPart bodyPartToAttach = bodyPartManager.bodyParts[bodyPartMenuCounter];

            if(    !selectedGameObject.GetComponent<BodyPart>().connectedBodyParts.Contains(bodyPartToAttach) 
                && !bodyPartToAttach.connectedBodyParts.Contains(selectedGameObject.GetComponent<BodyPart>()) 
                && bodyPartToAttach.CheckConnectionValidity(selectedGameObject.GetComponent<BodyPart>()) 
                && selectedGameObject.GetComponent<BodyPart>().CheckConnectionValidity(bodyPartToAttach))
            {
                //assign button to connect chosen with selected, then go back to default
                AssignConnectTwoBodyParts(bodypart, bodyPartToAttach, menuButton);
                menuButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyPartToAttach.name;
                menuCount += 1;
            }

            bodyPartMenuCounter += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            UnityEngine.Events.UnityAction action = () => { SelectBodyPartToAttachOptions(bodypart); };
            menuButtons[7].onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    void AssignConnectTwoBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, Button button)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () => {
            if (bodyPart1.connectedBodyParts.Contains(bodyPart2) || bodyPart2.connectedBodyParts.Contains(bodyPart1))
            {
                textLog.NewLogEntry($"Those are already connected!");
            }
            else
            {
                Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds, goldCost, bloodLossRate, damage);
                textLog.NewLogEntry($"Connecting the {bodyPart1.name} to the {bodyPart2.name}...");
                actionTimeBar.Reset(seconds);
            }

        };
        button.onClick.AddListener(action);

        string mouseoverText = 
            $"Connects {bodyPart1.name} to {bodyPart2.name}.\n" +
            $"Causes bloodloss in both bodyparts.\n" +
            $"Causes damage to both bodyparts.";

        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignDestroyBodyPart(Button button, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select something for that!");
                return;
            }
            else if (bodypart.connectedBodyParts.Count() != 0)
            {
                textLog.NewLogEntry("That bodypart is still connected to something!");
            }
            else if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Surgery.DeleteBodyPart(selectedGameObject.GetComponent<BodyPart>(), seconds, goldCost);
                textLog.NewLogEntry($"Destroying the {bodypart.name}..."); selectedGameObject = null;
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Destroys the selected bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #endregion

    #region OrganSurgery

    public void AssignImplantOrganOptions(Button button)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if (selectedGameObject.GetComponent<Organ>() is null)
            {
                textLog.NewLogEntry("You need to select something for that!");
                return;
            }
            else
            {
                ImplantOrganOptions(true);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant organ into...";
    }

    public void AssignImplantEmbeddedObjectOptions(Button button)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if (selectedGameObject.GetComponent<EmbeddedObject>() is null)
            {
                textLog.NewLogEntry("You need to select something for that!");
                return;
            }
            else
            {
                ImplantEmbeddedObjectOptions(true);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant embedded object into...";
    }

    public void ImplantOrganOptions(bool firstClick = true)
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        if (firstClick)
        {
            bodyPartMenuCounter = 0;
        }

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while (menuCount < menuButtons.Count()-1 && bodyPartMenuCounter < bodyPartManager.bodyParts.Count()-1)
        {
            BodyPart bodyPart = bodyPartManager.bodyParts[bodyPartMenuCounter];

            if (bodyPart.CheckImplantValidity((Organ)selectedGameObject.GetComponent<BodyPart>()))
            {
                //select bodypart
                AssignImplantOrganButton(menuButtons[menuCount], bodyPart, (Organ)selectedGameObject.GetComponent<BodyPart>());
                menuCount += 1;
            }

            bodyPartMenuCounter += 1;

        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            UnityEngine.Events.UnityAction action = () => { ImplantOrganOptions(false); };
            menuButtons[7].GetComponent<Button>().onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    public void ImplantEmbeddedObjectOptions(bool firstClick = true)
    {
        ClearAllButtons();
        UpdateMenuButtonsInteractivity(true);
        if (firstClick)
        {
            bodyPartMenuCounter = 0;
        }

        //put bodyparts on first 7 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while (menuCount < menuButtons.Count()-1 && bodyPartMenuCounter < bodyPartManager.bodyParts.Count()-1)
        {
            BodyPart bodyPart = bodyPartManager.bodyParts[bodyPartMenuCounter];

            if (bodyPart.CheckEmbeddedObjectValidity(selectedGameObject.GetComponent<EmbeddedObject>()))
            {
                //select bodypart
                AssignEmbedObjectButton(menuButtons[menuCount], bodyPart, selectedGameObject.GetComponent<EmbeddedObject>());
                menuCount += 1;
            }

            bodyPartMenuCounter += 1;

        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            UnityEngine.Events.UnityAction action = () => { ImplantEmbeddedObjectOptions(false); };
            menuButtons[7].GetComponent<Button>().onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    void AssignImplantOrganButton(Button button, BodyPart bodypart, Organ organ)
    {
        float seconds = 60.0f * 10.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null || organ == null)
            {
                textLog.NewLogEntry("You need to select both a bodypart and an organ for that!");
            }
            else if (bodypart.containedOrgans.Contains(organ))
            {
                textLog.NewLogEntry("The organ is already in there!");
            }
            else if (organ.connectedBodyParts.Count != 0)
            {
                textLog.NewLogEntry("The organ is already inside something else!");
            }
            else
            {
                Actions_Surgery.ImplantOrgan(organ, bodypart, seconds, goldCost, bloodLossRate, damage);
                textLog.NewLogEntry($"Implanting the {organ.name} into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant {organ.name} into {bodypart.name}: {seconds} seconds, {goldCost} gold";

        string mouseoverText = 
            "Implants the selected organ into the selected bodypart.\n" +
           $"Increases bloodloss rate in {bodypart} by {bloodLossRate} units.\n" +
           $"Causes {damage} damage to the bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignEmbedObjectButton(Button button, BodyPart bodypart, EmbeddedObject embeddedObject)
    {
        float seconds = 60.0f * 10.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null || embeddedObject == null)
            {
                textLog.NewLogEntry("You need to select both a bodypart and an object for that!");
            }
            else if (bodypart.embeddedObjects.Contains(embeddedObject))
            {
                textLog.NewLogEntry("The object is already in there!");
            }
            else if (!(embeddedObject.parentBodyPart is null))
            {
                textLog.NewLogEntry("The object is already inside something else!");
            }
            else
            {
                Actions_Surgery.EmbedObject(embeddedObject, bodypart, seconds, goldCost, bloodLossRate, damage);
                textLog.NewLogEntry($"Implanting the {embeddedObject.name} into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant {embeddedObject.name} into {bodypart.name}: {seconds} seconds, {goldCost} gold";

        string mouseoverText =
            $"Implants the {embeddedObject} into the selected bodypart.\n" +
            $"Increases bloodloss rate in {bodypart} by {bloodLossRate} units.\n" +
            $"Causes {damage} damage to the bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignRemoveOrganButton(Button button, Organ organ)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (organ == null)
            {
                textLog.NewLogEntry("You need to select an organ for that!");
            }
            else
            {
                Actions_Surgery.RemoveOrgan(organ, seconds, goldCost, bloodLossRate, damage);
                textLog.NewLogEntry($"Extracting the {organ.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Extract Organ: {seconds} seconds, {goldCost} gold";

        string mouseoverText = 
            "Removes the selected organ from the containing bodypart." +
            ((organ.connectedBodyParts.Count() > 0) ? $"Increases bloodloss rate in {organ.connectedBodyParts[0]} by {bloodLossRate} units.\n" : "") +
           $"Causes {damage} damage to the bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignRemoveEmbeddedObjectButton(Button button, EmbeddedObject embeddedObject)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        float damage = 40;
        float bloodLossRate = 4;

        UnityEngine.Events.UnityAction action = () =>
        {
            if (embeddedObject == null)
            {
                textLog.NewLogEntry("You need to select an embedded object for that!");
            }
            else
            {
                Actions_Surgery.RemoveEmbeddedObject(embeddedObject, seconds, goldCost, bloodLossRate, damage);
                textLog.NewLogEntry($"Extracting the {embeddedObject.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Extract Embedded Object: {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Removes the selected embedded object from the containing bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignDestroyOrgan(Button button, Organ organ)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (organ == null)
            {
                textLog.NewLogEntry("You need to select a organ for that!");
            }
            else if (organ == null || organ.connectedBodyParts.Count() != 0)
            {
                textLog.NewLogEntry("That organ is still inside something!");
            }
            else
            {
                Actions_Surgery.DeleteBodyPart(organ, seconds, goldCost);
                textLog.NewLogEntry($"Destroying the {organ.name}...");
                selectedGameObject = null; actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";

        string mouseoverText = "Destroys the selected organ.\nSelected organ cannot be currently implanted in a bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #endregion

    #region Examine

    public void ExamineSelectedGameObject()
    {
        if (selectedGameObject == null)
        {
            examineBox.text = "Nothing currently selected.";
        }
        else if(!(selectedGameObject.GetComponent<BodyPart>() is null))
        {
            examineBox.text = selectedGameObject.GetComponent<BodyPart>().GenerateDescription();
        }
        else if (!(selectedGameObject.GetComponent<EmbeddedObject>() is null))
        {
            examineBox.text = selectedGameObject.GetComponent<EmbeddedObject>().GenerateDescription();
        }
    }

    #endregion

    void AssignSpawnBodyPartTemplate(Button button, int goldCost, float seconds, Action<float, int> spawnMethod, string partName)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                spawnMethod(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new {partName}..");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"{partName}: {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Creates a new {partName}, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    void AssignSpawnClockworkTemplate(Button button, int goldCost, float seconds, Action<float, int> spawnMethod, string partName)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                spawnMethod(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new {partName}..");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"{partName}: {seconds} seconds, {goldCost} gold";

        string mouseoverText = $"Creates a new {partName}, unconnected to any bodypart.";
        SetButtonMouseoverText(button, mouseoverText);
    }

    #region wait

    void AssignWaitTenSeconds(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(10.0f); textLog.NewLogEntry("Waiting ten seconds..."); actionTimeBar.Reset(10.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait ten seconds";
    }

    void AssignWaitThirtySeconds(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(30.0f); textLog.NewLogEntry("Waiting thirty seconds..."); actionTimeBar.Reset(30.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait thirty seconds";
    }

    void AssignWaitOneMinute(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(60.0f); textLog.NewLogEntry("Waiting one minute..."); actionTimeBar.Reset(60.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait one minute";
    }

    void AssignWaitFiveMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(300.0f); textLog.NewLogEntry("Waiting five minutes..."); actionTimeBar.Reset(300.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait five minutes";
    }

    void AssignWaitTenMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(600.0f); textLog.NewLogEntry("Waiting ten minutes..."); actionTimeBar.Reset(600.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait ten minutes";
    }

    void AssignWaitThirtyMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(1800.0f); textLog.NewLogEntry("Waiting thirty minutes..."); actionTimeBar.Reset(1800.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait thirty minutes";
    }

    void AssignDischargePatient(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.DischargePatient(); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Discharge Patient (30 minutes)";
    }

    #endregion

}
