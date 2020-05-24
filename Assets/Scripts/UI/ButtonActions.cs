using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<Button> menuButtons;
    public List<Button> menuTabs;
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private List<Organ> organs = new List<Organ>();

    public BodyPart selectedBodyPart = null;
    public GameObject body;

    public Text selectedBodyPartText;
    public Text messageBox;
    public Text examineBox;

    private int bodyPartMenuCounter = 0;
    private int organMenuCounter = 0;

    public LifeMonitor lifeMonitor;
    public Clock clock;
    public ActionTimeBar actionTimeBar;

    private UnlockTracker unlocks;
    private float secondCounter;

    // Start is called before the first frame update
    void Start()
    {
        ClearAllButtons();
        PopulateBodyPartsList();
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

    void PopulateBodyPartsList()
    {
        bodyParts = new List<BodyPart>();
        

        foreach (BodyPart bodypart in FindObjectsOfType<BodyPart>().ToList<BodyPart>())
        {
            if (!(bodypart is Organ))
            {
                bodyParts.Add(bodypart);
            }
        }
        //bodyParts.Sort();
    }

    void PopulateOrgansList()
    {
        organs = FindObjectsOfType<Organ>().ToList<Organ>();
    }

    // Update is called once per frame
    void Update()
    {
        PopulateBodyPartsList();
        PopulateOrgansList();

        //update selected bodypart name text
        if (selectedBodyPart is Organ)
        {
            if (selectedBodyPart.connectedBodyParts.Count != 0)
            {
                selectedBodyPartText.text = $"{selectedBodyPart.connectedBodyParts[0]} : {selectedBodyPart.name}";
            }
            else
            {
                selectedBodyPartText.text = $"External : {selectedBodyPart.name}";
            }
        }
        else if (selectedBodyPart) selectedBodyPartText.text = selectedBodyPart.name;

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
    void ClearAllButtons()
    {
        foreach (Button button in menuButtons)
        {
            button.onClick.RemoveAllListeners();
            button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
        }
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
        int numBodyParts = bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        while(bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart + 7))
        {
            //select bodypart
            AssignSelectBodyPart(menuButtons[bodyPartMenuCounter - countStart], bodyParts[bodyPartMenuCounter]);
            bodyPartMenuCounter += 1;
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
        int numOrgans = organs.Count();
        int countStart = organMenuCounter;
        while (organMenuCounter < Mathf.Min(numOrgans, countStart + 7))
        {
            //select organ
            AssignSelectOrgan(menuButtons[organMenuCounter - countStart], organs[organMenuCounter]);
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
            messageBox.text = "Please select an organ or limb.";
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
            messageBox.text = "Please select an organ or limb.";
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
            messageBox.text = "Please select an organ or limb.";
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
            messageBox.text = "Please select an organ or limb.";
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
        AssignSpawnLungButton(menuButtons[1]);
        AssignSpawnBrainButton(menuButtons[2]);
        AssignSpawnEyeButton(menuButtons[3]);
        AssignSpawnLiverButton(menuButtons[4]);
        AssignSpawnStomachButton(menuButtons[5]);
    }

    public void SelectSpawnBodyPartActionOptions()
    {
        ClearAllButtons();

        AssignSpawnArmButton(menuButtons[0]);
        AssignSpawnLegButton(menuButtons[1]);
        AssignSpawnHeadButton(menuButtons[2]);
        AssignSpawnTorsoButton(menuButtons[3]);
    }


    #region Medicine

    void AssignInjectHealthPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 30;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectHealthPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Health Potion into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Health Potion (100 units): {seconds} seconds, {goldCost} gold";

    }

    void AssignInjectAntidoteButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectAntidote(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Antidote into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Antidote (50 units): {seconds} seconds, {goldCost} gold";

    }

    void AssignInjectSlowPoisonButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectSlowPoison(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Slow Poison into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Slow Poison (50 units): {seconds} seconds, {goldCost} gold";

    }

    void AssignInjectStasisPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectStasisPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Stasis Potion into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Stasis Potion (50 units): {seconds} seconds, {goldCost} gold";

    }

    void AssignInjectHastePotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectHastePotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Haste Potion into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Haste Potion (50 units): {seconds} seconds, {goldCost} gold";

    }

    void AssignInjectCoagulantPotionButton(Button button, BodyPart bodypart)
    {
        float seconds = 10.0f;
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectCoagulantPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Coagulant Potion into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Coagulant Potion (50 units): {seconds} seconds, {goldCost} gold";

    }

    #endregion

    #region Charms

    void AssignApplyHeartCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Charms.ApplyHeartCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Heart Charm to the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Heart Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

    }

    void AssignApplyLungCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Charms.ApplyLungCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Lung Charm to the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Lung Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

    }

    void AssignApplyPetrificationCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Charms.ApplyPetrificationCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Petrification Charm to the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Petrification Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

    }

    void AssignApplyBloodRegenCharm(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 200;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Charms.ApplyBloodRegenCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Blood Regeneration Charm to the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Apply Blood Regen Charm (30 Minutes): {seconds} seconds, {goldCost} gold";

    }

    #endregion

    #region Blood

    void AssignBandagesButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 10;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.Bandages(bodypart, seconds, goldCost); messageBox.text = $"Applying bandages to the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bandages: {seconds} seconds, {goldCost} gold";
    }

    void AssignBloodlettingButton(Button button, BodyPart bodypart)
    {
        float seconds = 30.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.Bloodletting(bodypart, seconds, goldCost); messageBox.text = $"Inducing bleeding in the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Bloodletting: {seconds} seconds, {goldCost} gold";
    }

    void AssignAddBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 20;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.AddBlood(bodypart, seconds, goldCost); messageBox.text = $"Injecting 100 units of blood into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Blood: (100 units): {seconds} seconds, {goldCost} gold";
    }

    void AssignRemoveBloodButton(Button button, BodyPart bodypart)
    {
        float seconds = 20.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.RemoveBlood(bodypart, seconds, goldCost); messageBox.text = $"Extracting 100 units of blood from the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Remove Blood: (100 units): {seconds} seconds, {goldCost} gold";
    }

    #endregion

    #region BodyPartSurgery

    void AssignRemoveBodyPartButton(Button button, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.RemoveBodyPart(bodypart, seconds, goldCost); messageBox.text = $"Removing the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Amputate Bodypart: {seconds} seconds, {goldCost} gold";
    }


    void AssignAttachBodyPartButton(Button button, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { SelectBodyPartToAttachOptions(bodypart); };
        }
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
        int numBodyParts = bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        while (bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart + 6))
        {
            Button menuButton = menuButtons[bodyPartMenuCounter - countStart];

            //assign button to connect chosen with selected, then go back to default
            AssignConnectTwoBodyParts(bodypart, bodyParts[bodyPartMenuCounter], menuButton);
            menuButton.transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyParts[bodyPartMenuCounter].name;
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
        int goldCost = 20;
        UnityEngine.Events.UnityAction action = () => { Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds, goldCost); messageBox.text = $"connecting the {bodyPart1.name} to the {bodyPart2.name}..."; actionTimeBar.Reset(seconds); };
        button.onClick.AddListener(action);
    }

    void AssignDestroyBodyPart(Button button, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;

        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else if (selectedBodyPart.connectedBodyParts.Count() != 0)
        {
            action = () => { messageBox.text = "That bodypart is still connected to things!"; };
        }
        else
        {
            action = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds, goldCost); messageBox.text = $"Destroying the {bodypart.name}..."; selectedBodyPart = null; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";
    }

    #endregion

    #region OrganSurgery

    public void AssignImplantOrganOptions(Button button)
    {
        UnityEngine.Events.UnityAction action = null;
        if (!(selectedBodyPart is Organ))
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { ImplantOrganOptions(true); };
        }

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
        int numBodyParts = bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        while (bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart + 7))
        {
            //select bodypart
            AssignImplantOrganButton(menuButtons[bodyPartMenuCounter - countStart], bodyParts[bodyPartMenuCounter], (Organ)selectedBodyPart);
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
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null || organ == null)
        {
            action = () => { messageBox.text = "You need to select both a bodypart and an organ for that!"; };
        }
        else if (bodypart.containedOrgans.Contains(organ))
        {
            action = () => { messageBox.text = "The organ is already in there!"; };
        }
        else if (organ.connectedBodyParts.Count != 0)
        {
            action = () => { messageBox.text = "The organ is already inside something else!"; };
        }
        else
        {
            action = () => { Actions_Surgery.ImplantOrgan(organ, bodypart, seconds, goldCost); messageBox.text = $"Implanting the {organ.name} into the {bodypart.name}..."; actionTimeBar.Reset(seconds); };
        }


        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant {organ.name} into {bodypart.name}: {seconds} seconds, {goldCost} gold";
    }


    void AssignRemoveOrganButton(Button button, Organ organ)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;
        if (organ == null)
        {
            action = () => { messageBox.text = "You need to select an organ for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.RemoveOrgan(organ, seconds, goldCost); messageBox.text = $"Extracting the {organ.name}..."; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Extract Organ: {seconds} seconds, {goldCost} gold";
    }


    void AssignDestroyOrgan(Button button, Organ organ)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;

        if (organ == null)
        {
            action = () => { messageBox.text = "You need to select a organ for that!"; };
        }
        else if (organ == null || organ.connectedBodyParts.Count() != 0)
        {
            action = () => { messageBox.text = "That organ is still inside something!"; };
        }
        else
        {           
            action = () => {  Actions_Surgery.DeleteBodyPart(organ, seconds, goldCost); messageBox.text = $"Destroying the {organ.name}..."; selectedBodyPart = null; actionTimeBar.Reset(seconds); };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";
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
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnHeart(seconds, goldCost); messageBox.text = $"Spawning a new heart..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new heart: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnLungButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLung(seconds, goldCost); messageBox.text = $"Spawning a new lung..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new lung: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnBrainButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new brain: {seconds} seconds, {goldCost} gold";
    }

    void AssignSpawnEyeButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnEye(seconds, goldCost); messageBox.text = $"Spawning a new eye..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new eye: {seconds} seconds, {goldCost} gold";
    }

    void AssignSpawnLiverButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLiver(seconds, goldCost); messageBox.text = $"Spawning a new liver..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new liver: {seconds} seconds, {goldCost} gold";
    }

    void AssignSpawnStomachButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnStomach(seconds, goldCost); messageBox.text = $"Spawning a new stomach..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new stomach: {seconds} seconds, {goldCost} gold";
    }

    void AssignSpawnArmButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnArm(seconds, goldCost); messageBox.text = $"Spawning a new arm..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new arm: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnLegButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLeg(seconds, goldCost); messageBox.text = $"Spawning a new leg..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new leg: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnTorsoButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnTorso(seconds, goldCost); messageBox.text = $"Spawning a new torso..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new torso: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnHeadButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnHead(seconds, goldCost); messageBox.text = $"Spawning a new head..."; actionTimeBar.Reset(seconds); };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new head: {seconds} seconds, {goldCost} gold";

    }

    #endregion

    #region wait

    void AssignWaitTenSeconds(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(10.0f); messageBox.text = "Waiting ten seconds..."; actionTimeBar.Reset(10.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait ten seconds";
    }

    void AssignWaitThirtySeconds(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(30.0f); messageBox.text = "Waiting thirty seconds..."; actionTimeBar.Reset(30.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait thirty seconds";
    }

    void AssignWaitOneMinute(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(60.0f); messageBox.text = "Waiting one minute..."; actionTimeBar.Reset(60.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait one minute";
    }

    void AssignWaitFiveMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(300.0f); messageBox.text = "Waiting five minutes..."; actionTimeBar.Reset(300.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait five minutes";
    }

    void AssignWaitTenMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(600.0f); messageBox.text = "Waiting ten minutes..."; actionTimeBar.Reset(600.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait ten minutes";
    }

    void AssignWaitThirtyMinutes(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(1800.0f); messageBox.text = "Waiting thirty minutes..."; actionTimeBar.Reset(1800.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait thirty minutes";
    }

    void AssignWaitOneHour(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(3600.0f); lifeMonitor.VictoryCheck(); messageBox.text = "Waiting one hour..."; actionTimeBar.Reset(3600.0f); };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait an hour (Victory Check)";
    }

    #endregion

}
