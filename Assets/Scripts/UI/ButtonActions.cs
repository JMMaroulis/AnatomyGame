using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<Button> menuButtons;
    public List<Button> menuTabs;

    public BodyPart selectedBodyPart = null;
    public GameObject body;

    public TextLog textLog;
    public Text examineBox;

    private int bodyPartMenuCounter = 0;
    private int organMenuCounter = 0;

    public LifeMonitor lifeMonitor;
    public ActionTimeBar actionTimeBar;

    private GoldTracker goldTracker;
    private UnlockTracker unlocks;
    private Clock clock;
    private float secondCounter;
    private BodyPartManager bodyPartManager;

    // Start is called before the first frame update
    void Start()
    {
        goldTracker = FindObjectOfType<GoldTracker>();
        clock = FindObjectOfType<Clock>();
        bodyPartManager = FindObjectOfType<BodyPartManager>();

        ClearAllButtons();
        examineBox.text = "";

        //enable/disable button tabs based on unlocks
        unlocks = FindObjectOfType<UnlockTracker>();
        menuTabs[2].gameObject.SetActive(unlocks.blood);
        menuTabs[3].gameObject.SetActive(unlocks.surgery);
        //menuTabs[4].gameObject.SetActive(unlocks.medicine); //medicine permissions on individual level
        menuTabs[5].gameObject.SetActive(unlocks.charms_petrification || unlocks.charms_heart || unlocks.charms_lung || unlocks.charms_blood_regen); //charm permissions on individual level
        menuTabs[6].gameObject.SetActive(unlocks.spawn);
        menuTabs[7].gameObject.SetActive(unlocks.spawn);
    }

    // Update is called once per frame
    void Update()
    {

        //update bodypart examination textbox every x seconds
        if (selectedBodyPart)
        {
            secondCounter += Time.unscaledDeltaTime;
            if(secondCounter >= 0.3f || examineBox.text == "")
            {
                ExamineSelectedBodyPart();
                secondCounter = 0.0f;
            }
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
    }

    public void DisableAllButtons()
    {
        foreach (Button button in menuButtons)
        {
            button.interactable = false;
        }
    }

    public void EnableAllButtons()
    {
        foreach (Button button in menuButtons)
        {
            button.interactable = true;
        }
    }

    public void CancelAction()
    {
        clock.StopClock();
        actionTimeBar.Reset(1.0f);
    }

    public void SelectBodyPartOptions(bool firstClick = true)
    {
        ClearAllButtons();
        if (firstClick)
        {
            bodyPartMenuCounter = 0;
        }

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while(menuCount < menuButtons.Count() && bodyPartMenuCounter < bodyPartManager.bodyParts.Count())
        {
            //select bodypart
            AssignSelectBodyPart(menuButtons[bodyPartMenuCounter - countStart], bodyPartManager.bodyParts[bodyPartMenuCounter]);
            bodyPartMenuCounter += 1;
            menuCount += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            UnityEngine.Events.UnityAction action = () => { SelectBodyPartOptions(false); };
            menuButtons[7].GetComponent<Button>().onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }

    }

    public void SelectOrganOptions(bool firstClick)
    {
        ClearAllButtons();
        if (firstClick)
        {
            organMenuCounter = 0;
        }

        //put organs on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numOrgans = bodyPartManager.organs.Count();
        int countStart = organMenuCounter;
        while (organMenuCounter < Mathf.Min(numOrgans, countStart + 7))
        {
            //select organ
            AssignSelectOrgan(menuButtons[organMenuCounter - countStart], bodyPartManager.organs[organMenuCounter]);
            organMenuCounter += 1;
        }

        if (organMenuCounter < numOrgans)
        {
            //more organs button
            UnityEngine.Events.UnityAction action = () => { SelectOrganOptions(false); };
            menuButtons[7].GetComponent<Button>().onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    public void SelectSurgeryAction()
    {
        if (selectedBodyPart is null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
        }
        else if (selectedBodyPart is Organ)
        {
            SelectSurgeryActionOptions_Organ();
        }
        else if (selectedBodyPart is BodyPart)
        {
            SelectSurgeryActionOptions_BodyPart();
        }
    }

    public void SelectSurgeryActionOptions_BodyPart()
    {
        ClearAllButtons();

        AssignRemoveBodyPartButton(menuButtons[0], selectedBodyPart);
        AssignAttachBodyPartButton(menuButtons[1], selectedBodyPart);
        AssignDestroyBodyPart(menuButtons[2], selectedBodyPart);
    }

    public void SelectSurgeryActionOptions_Organ()
    {
        ClearAllButtons();

        AssignRemoveOrganButton(menuButtons[0], (Organ)selectedBodyPart);
        AssignImplantOrganOptions(menuButtons[1]);
        AssignDestroyOrgan(menuButtons[2], (Organ)selectedBodyPart);
    }

    public void SelectWaitActionOptions()
    {
        ClearAllButtons();

        AssignWaitTenSeconds(menuButtons[0]);
        AssignWaitThirtySeconds(menuButtons[1]);
        AssignWaitOneMinute(menuButtons[2]);
        AssignWaitFiveMinutes(menuButtons[3]);
        AssignWaitTenMinutes(menuButtons[4]);
        AssignWaitThirtyMinutes(menuButtons[5]);
        AssignWaitOneHour(menuButtons[6]);
    }

    public void SelectBloodAction()
    {
        if (selectedBodyPart is null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
        }
        else if (selectedBodyPart is Organ)
        {
            SelectBloodActionOptions_Organ();
        }
        else if (selectedBodyPart is BodyPart)
        {
            SelectBloodActionOptions_BodyPart();
        }
    }

    public void SelectBloodActionOptions_BodyPart()
    {
        ClearAllButtons();

        AssignBandagesButton(menuButtons[0], selectedBodyPart);
        AssignBloodlettingButton(menuButtons[1], selectedBodyPart);
        AssignAddBloodButton(menuButtons[2], selectedBodyPart);
        AssignRemoveBloodButton(menuButtons[3], selectedBodyPart);
    }

    public void SelectBloodActionOptions_Organ()
    {
        ClearAllButtons();

        AssignAddBloodButton(menuButtons[0], selectedBodyPart);
        AssignRemoveBloodButton(menuButtons[1], selectedBodyPart);
    }

    public void SelectMedicineAction()
    {
        if (selectedBodyPart is null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
        }
        else if (selectedBodyPart is Organ)
        {
            SelectMedicineActionOptions_Organ();
        }
        else if (selectedBodyPart is BodyPart)
        {
            SelectMedicineActionOptions_BodyPart();
        }
    }

    public void SelectMedicineActionOptions_BodyPart()
    {
        ClearAllButtons();
        int n = 0;

        AssignInjectHealthPotionButton(menuButtons[n], selectedBodyPart); n++;
        if (unlocks.medicine_blood) 
        {
            AssignInjectCoagulantPotionButton(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.medicine_poison) 
        {
            AssignInjectAntidoteButton(menuButtons[n], selectedBodyPart); n++;
            AssignInjectSlowPoisonButton(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.medicine_speed)
        {
            AssignInjectStasisPotionButton(menuButtons[n], selectedBodyPart); n++;
            AssignInjectHastePotionButton(menuButtons[n], selectedBodyPart); n++;
        }
 
    }

    public void SelectMedicineActionOptions_Organ()
    {
        ClearAllButtons();
        int n = 0;

        AssignInjectHealthPotionButton(menuButtons[n], selectedBodyPart); n++;
        if (unlocks.medicine_blood)
        {
            AssignInjectCoagulantPotionButton(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.medicine_poison)
        {
            AssignInjectAntidoteButton(menuButtons[n], selectedBodyPart); n++;
            AssignInjectSlowPoisonButton(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.medicine_speed)
        {
            AssignInjectStasisPotionButton(menuButtons[n], selectedBodyPart); n++;
            AssignInjectHastePotionButton(menuButtons[n], selectedBodyPart); n++;
        }
    }

    public void SelectCharmAction()
    {
        if (selectedBodyPart is null)
        {
            textLog.NewLogEntry("Please select an organ or limb.");
        }
        else if (selectedBodyPart is Organ)
        {
            SelectCharmActionOptions_Organ();
        }
        else if (selectedBodyPart is BodyPart)
        {
            SelectCharmActionOptions_BodyPart();
        }
    }

    public void SelectCharmActionOptions_BodyPart()
    {
        int n = 0;
        ClearAllButtons();
        if (unlocks.charms_heart)
        {
            AssignApplyHeartCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_lung)
        {
            AssignApplyLungCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_blood_regen)
        {
            AssignApplyBloodRegenCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_petrification)
        {
            AssignApplyPetrificationCharm(menuButtons[n], selectedBodyPart); n++;
        }
    }

    public void SelectCharmActionOptions_Organ()
    {
        int n = 0;
        ClearAllButtons();
        if (unlocks.charms_heart)
        {
            AssignApplyHeartCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_lung)
        {
            AssignApplyLungCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_blood_regen)
        {
            AssignApplyBloodRegenCharm(menuButtons[n], selectedBodyPart); n++;
        }
        if (unlocks.charms_petrification)
        {
            AssignApplyPetrificationCharm(menuButtons[n], selectedBodyPart); n++;
        }
    }

    #region Selection

    //make a button select a given bodypart
    void AssignSelectBodyPart(Button button, BodyPart bodyPart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectBodyPart(bodyPart); };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPart.name;
    }

    //select a given bodypart
    void SelectBodyPart(BodyPart bodyPart)
    {
        selectedBodyPart = bodyPart;
        FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
    }

    //make a button select a given bodypart
    void AssignSelectOrgan(Button button, Organ organ)
    {
        UnityEngine.Events.UnityAction action = () => { SelectBodyPart(organ); };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        if (organ.connectedBodyParts.Count != 0)
        {
            buttonText.text = $"{organ.GetComponent<Organ>().connectedBodyParts[0].name} : {organ.name}";
        }
        else
        {
            buttonText.text = $"External : {organ.name}";
        }
    }

    #endregion


    public void SelectSpawnOrganActionOptions()
    {
        ClearAllButtons();

        AssignSpawnHeartButton(menuButtons[0]);
        AssignSpawnLeftLungButton(menuButtons[1]);
        AssignSpawnRightLungButton(menuButtons[2]);
        AssignSpawnBrainButton(menuButtons[3]);
        AssignSpawnLeftEyeButton(menuButtons[4]);
        AssignSpawnRightEyeButton(menuButtons[5]);
        AssignSpawnLiverButton(menuButtons[6]);
        AssignSpawnStomachButton(menuButtons[7]);
    }

    public void SelectSpawnBodyPartActionOptions()
    {
        ClearAllButtons();

        AssignSpawnLeftArmButton(menuButtons[0]);
        AssignSpawnRightArmButton(menuButtons[1]);
        AssignSpawnLeftLegButton(menuButtons[2]);
        AssignSpawnRightLegButton(menuButtons[3]);
        AssignSpawnHeadButton(menuButtons[4]);
        AssignSpawnTorsoButton(menuButtons[5]);
    }


    #region Medicine

    void AssignInjectHealthPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 30;
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
                Actions_Medicine.InjectHealthPotion(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 100 units of Health Potion into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Health Potion (100 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();

        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Health Potion: Processed at 1/5 units per second, heals one damage per unit.\nIf bodypart is undamaged, processed at 1/100 units per second.";

    }

    void AssignInjectAntidoteButton(Button button, BodyPart bodypart)
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
                Actions_Medicine.InjectAntidote(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Antidote into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Antidote (50 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Antidote: Mutually neutralises 1 unit of slow poison per second. Decays at 1/100 units per second if no poison present in the bodypart.";

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
                Actions_Medicine.InjectSlowPoison(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Slow Poison into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Slow Poison (50 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Slow Poison: Deals 1/1000 damage per unit per second. Slowly processed by liver.";

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
                Actions_Medicine.InjectStasisPotion(bodypart, seconds, goldCost); 
                textLog.NewLogEntry($"Injecting 50 units of Stasis Potion into the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Stasis Potion (50 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Decreases local rate of time passing in bodypart. Decays at 1/100 units per second, unaffected by own temporal slowing.\nWARNING: Effect of high dosages extremely dangerous and unpredictable.";

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
                Actions_Medicine.InjectStasisPotion(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 50 units of Stasis Potion into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Haste Potion (50 units): {seconds} seconds, {goldCost} gold";


        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Increases local rate of time passing in bodypart. Decays at 1/100 units per second, unaffected by own temporal acceleration.";


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
                Actions_Medicine.InjectCoagulantPotion(bodypart, seconds, goldCost); 
                textLog.NewLogEntry($"Injecting 50 units of Coagulant Potion into the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Coagulant Potion (50 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Decreases bloodloss rate by 0.5 units per second. Decays at 1/100 unit per second, if no bloodloss to be healed.";
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

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Heart Charm: Causes the bodypart to function as a heart for the duration.";

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

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Lung Charm: Causes the bodypart to function as a lung for the duration.";

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

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Petrification Charm: The bodypart ceases all functions for the duration.";

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

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Blood Regen Charm: Produces 30 units of blood per second for the duration, up to the bodypart maximum capacity.";

    }

    #endregion

    #region Blood

    void AssignBandagesButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 10;
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
                Actions_Blood.Bandages(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Applying bandages to the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bandages: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Reduced bloodloss by 10 units per second.";
    }

    void AssignBloodlettingButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 0;
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
                Actions_Blood.Bloodletting(bodypart, seconds, goldCost); 
                textLog.NewLogEntry($"Inducing bleeding in the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bloodletting: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Induces bloodloss of 10 units per second.";
    }

    void AssignAddBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 20;
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
                Actions_Blood.AddBlood(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Injecting 100 units of blood into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Blood: (100 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Directly adds 100 units of blood.";
    }

    void AssignRemoveBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 0;
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
                Actions_Blood.RemoveBlood(bodypart, seconds, goldCost); 
                textLog.NewLogEntry($"Extracting 100 units of blood from the {bodypart.name}..."); 
                actionTimeBar.Reset(seconds);
            }
        };
        
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Remove Blood: (100 units): {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Directly removes 100 units of blood.";
    }

    #endregion

    #region BodyPartSurgery

    void AssignRemoveBodyPartButton(Button button, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
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
                Actions_Surgery.RemoveBodyPart(bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Removing the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Amputate Bodypart: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Disconnects the bodypart from all connected bodyparts.\nCauses profuse bloodloss in connected bodyparts.";
    }

    void AssignAttachBodyPartButton(Button button, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if (bodypart == null)
            {
                textLog.NewLogEntry("You need to select a bodypart for that!");
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

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while (menuCount < menuButtons.Count() && bodyPartMenuCounter < bodyPartManager.bodyParts.Count())
        {
            Button menuButton = menuButtons[menuCount];
            BodyPart bodyPartToAttach = bodyPartManager.bodyParts[bodyPartMenuCounter];

            if( !selectedBodyPart.connectedBodyParts.Contains(bodyPartToAttach) 
                && !bodyPartToAttach.connectedBodyParts.Contains(selectedBodyPart) 
                && bodyPartToAttach.CheckConnectionValidity(selectedBodyPart) 
                && selectedBodyPart.CheckConnectionValidity(bodyPartToAttach))
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
        UnityEngine.Events.UnityAction action = () => {
            if (bodyPart1.connectedBodyParts.Contains(bodyPart2) || bodyPart2.connectedBodyParts.Contains(bodyPart1))
            {
                textLog.NewLogEntry($"Those are already connected!");
            }
            else
            {
                Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds, goldCost);
                textLog.NewLogEntry($"connecting the {bodyPart1.name} to the {bodyPart2.name}...");
                actionTimeBar.Reset(seconds);
            }

        };
        button.onClick.AddListener(action);

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = $"Connects {bodyPart1.name} to {bodyPart2.name}.";
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
            }
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds, goldCost);
                textLog.NewLogEntry($"Destroying the {bodypart.name}..."); selectedBodyPart = null;
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Destroys the selected bodypart.";
    }

    #endregion

    #region OrganSurgery

    public void AssignImplantOrganOptions(Button button)
    {
        UnityEngine.Events.UnityAction action = () =>
        {
            if (!(selectedBodyPart is Organ))
            {
                textLog.NewLogEntry("You need to select something for that!");
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

    public void ImplantOrganOptions(bool firstClick = true)
    {
        ClearAllButtons();
        if (firstClick)
        {
            bodyPartMenuCounter = 0;
        }

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyPartManager.bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        int menuCount = 0;
        while (menuCount < menuButtons.Count() && bodyPartMenuCounter < bodyPartManager.bodyParts.Count())
        {
            BodyPart bodyPart = bodyPartManager.bodyParts[bodyPartMenuCounter];

            if (bodyPart.CheckImplantValidity((Organ)selectedBodyPart))
            {
                //select bodypart
                AssignImplantOrganButton(menuButtons[bodyPartMenuCounter - countStart], bodyPart, (Organ)selectedBodyPart);
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

    void AssignImplantOrganButton(Button button, BodyPart bodypart, Organ organ)
    {
        float seconds = 60.0f * 10.0f;
        int goldCost = 0;
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
                Actions_Surgery.ImplantOrgan(organ, bodypart, seconds, goldCost);
                textLog.NewLogEntry($"Implanting the {organ.name} into the {bodypart.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant {organ.name} into {bodypart.name}: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Implants the selected organ into the selected bodypart.";
    }

    void AssignRemoveOrganButton(Button button, Organ organ)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = () =>
        {
            if (organ == null)
            {
                textLog.NewLogEntry("You need to select an organ for that!");
            }
            else
            {
                Actions_Surgery.RemoveOrgan(organ, seconds, goldCost);
                textLog.NewLogEntry($"Extracting the {organ.name}...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Extract Organ: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Removes the selected organ from the containing bodypart.";
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
                selectedBodyPart = null; actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Destroys the selected organ.\nSelected organ cannot be currently implanted in a bodypart.";
    }

    #endregion

    #region Examine

    public void ExamineSelectedBodyPart()
    {
        if (selectedBodyPart == null)
        {
            examineBox.text = "You need to select a something for that!";
        }
        else
        {
            examineBox.text = selectedBodyPart.GenerateDescription();
        }
    }

    #endregion

    #region spawnBodyParts

    void AssignSpawnHeartButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnHeart(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new heart...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new heart: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Heart, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnLeftLungButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnLeftLung(seconds, goldCost); 
                textLog.NewLogEntry($"Spawning a new lung...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new  left lung: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Left Lung, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnRightLungButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnRightLung(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new lung...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new Right Lung: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Right Lung, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnBrainButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new brain...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new brain: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Brain, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
    }

    void AssignSpawnLeftEyeButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnLeftEye(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new eye...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new left eye: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Left Eye, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
    }

    void AssignSpawnRightEyeButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnRightEye(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new eye...");
                actionTimeBar.Reset(seconds);
            }
        };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new right eye: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Right Eye, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
    }

    void AssignSpawnLiverButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnLiver(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new liver...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new liver: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Liver, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
    }

    void AssignSpawnStomachButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnStomach(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new stomach...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new stomach: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Stomach, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";
    }

    void AssignSpawnLeftArmButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnLeftArm(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new arm...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new left arm: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Left Arm, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnRightArmButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnRightArm(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new arm...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new right arm: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Right Arm, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnLeftLegButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnLeftLeg(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new leg...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new left leg: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Left Leg, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnRightLegButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnRightLeg(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new leg...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new right leg: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Right Leg, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    void AssignSpawnTorsoButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnTorso(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new torso...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new torso: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Torso, with all contained organs, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";


    }

    void AssignSpawnHeadButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () =>
        {
            if ((goldTracker.goldAccumulated - goldTracker.goldSpent) < goldCost)
            {
                textLog.NewLogEntry("Insufficient funds.");
            }
            else
            {
                Actions_SpawnBodyParts.SpawnHead(seconds, goldCost);
                textLog.NewLogEntry($"Spawning a new head...");
                actionTimeBar.Reset(seconds);
            }
        };
        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new head: {seconds} seconds, {goldCost} gold";

        MouseOver mouseover = button.transform.GetComponentInChildren<MouseOver>();
        mouseover.mouseoverEnabled = true;
        mouseover.ResetTimer();
        Text mouseOverText = mouseover.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        mouseOverText.text = "Creates a new Head, with all contained organs, unconnected to any bodypart. Has full blood, oxygen, and health levels.\nWill require immediate attention.";

    }

    #endregion

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

    void AssignWaitOneHour(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { Actions_Wait.WaitSeconds(3600.0f); lifeMonitor.VictoryCheck(3600.0f); textLog.NewLogEntry("Waiting one hour..."); actionTimeBar.Reset(3600.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait an hour (Victory Check)";
    }

    #endregion

}
