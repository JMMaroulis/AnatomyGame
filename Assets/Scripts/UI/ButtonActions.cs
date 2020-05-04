using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<Button> menuButtons;
    public BodyPart selectedBodyPart = null;
    public Organ selectedOrgan = null;
    public GameObject body;
    private List<BodyPart> bodyParts = new List<BodyPart>();
    private List<Organ> organs = new List<Organ>();
    private int bodyPartMenuCounter = 0;
    private int organMenuCounter = 0;
    public Text selectedBodyPartText;
    public Text selectedOrganText;
    public Text messageBox;
    public LifeMonitor lifeMonitor;
    public Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        ClearAllButtons();
        PopulateBodyPartsList();
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
        if (selectedBodyPart) selectedBodyPartText.text = selectedBodyPart.name;
        else selectedBodyPartText.text = "No Body Part Selected";

        //update selected organ name text
        if (selectedOrgan) {
            if (selectedOrgan.connectedBodyParts.Count != 0)
            {
                selectedOrganText.text = $"{selectedOrgan.connectedBodyParts[0]} : {selectedOrgan.name}";
            }
            else
            {
                selectedOrganText.text = $"External : {selectedOrgan.name}";
            }
        }
        else selectedOrganText.text = "No Organ Selected";

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

        AssignRemoveOrganButton(menuButtons[0], selectedOrgan);
        AssignImplantOrganButton(menuButtons[1], selectedBodyPart, selectedOrgan);
        AssignDestroyOrgan(menuButtons[2], selectedOrgan);
    }


    //make a button take us to the waiting action select menu
    void AssignSelectWaitActionOptions(Button button)
    {
        button.GetComponent<Button>().onClick.AddListener(SelectWaitActionOptions);
        button.transform.GetChild(0).gameObject.GetComponent<Text>().text = "WAIT...";
    }

    public void SelectWaitActionOptions()
    {
        ClearAllButtons();

        AssignWaitOneMinute(menuButtons[0]);
        AssignWaitOneHour(menuButtons[1]);
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

        AssignAddBloodButton(menuButtons[0], selectedOrgan);
        AssignRemoveBloodButton(menuButtons[1], selectedOrgan);
    }

    public void SelectMedicineActionOptions_BodyPart()
    {
        ClearAllButtons();

        AssignInjectHealthPotionButton(menuButtons[0], selectedBodyPart);
        AssignInjectAntidoteButton(menuButtons[1], selectedBodyPart);
        AssignInjectSlowPoisonButton(menuButtons[2], selectedBodyPart);
        AssignInjectStasisPotionButton(menuButtons[3], selectedBodyPart);
        AssignInjectCoagulantPotionButton(menuButtons[4], selectedBodyPart);
    }

    public void SelectMedicineActionOptions_Organ()
    {
        ClearAllButtons();

        AssignInjectHealthPotionButton(menuButtons[0], selectedOrgan);
        AssignInjectAntidoteButton(menuButtons[1], selectedOrgan);
        AssignInjectSlowPoisonButton(menuButtons[2], selectedOrgan);
        AssignInjectStasisPotionButton(menuButtons[3], selectedOrgan);
        AssignInjectCoagulantPotionButton(menuButtons[4], selectedOrgan);

    }

    public void SelectCharmActionOptions_BodyPart()
    {
        ClearAllButtons();

        AssignApplyHeartCharm(menuButtons[0], selectedBodyPart);
        AssignApplyLungCharm(menuButtons[1], selectedBodyPart);
        AssignApplyPetrificationCharm(menuButtons[2], selectedBodyPart);
        AssignApplyBloodRegenCharm(menuButtons[3], selectedBodyPart);
    }

    public void SelectCharmActionOptions_Organ()
    {
        ClearAllButtons();

        AssignApplyHeartCharm(menuButtons[0], selectedOrgan);
        AssignApplyLungCharm(menuButtons[1], selectedOrgan);
        AssignApplyPetrificationCharm(menuButtons[2], selectedOrgan);
        AssignApplyBloodRegenCharm(menuButtons[3], selectedOrgan);
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
        UnityEngine.Events.UnityAction action = () => { SelectOrgan(organ); };
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

    //select a given bodypart
    void SelectOrgan(Organ organ)
    {
        selectedOrgan = organ;
    }

    #endregion

    public void SelectSpawnOrganActionOptions()
    {
        ClearAllButtons();

        AssignSpawnHeartButton(menuButtons[0]);
        AssignSpawnLungButton(menuButtons[1]);
        AssignSpawnBrainButton(menuButtons[2]);
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
        int goldCost = 50;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectHealthPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Health Potion into the {bodypart.name}..."; };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Health Potion (50 units): {seconds} seconds, {goldCost} gold";

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
            action = () => { Actions_Medicine.InjectAntidote(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Antidote into the {bodypart.name}..."; };
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
            action = () => { Actions_Medicine.InjectSlowPoison(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Slow Poison into the {bodypart.name}..."; };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Slow Potion (50 units): {seconds} seconds, {goldCost} gold";

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
            action = () => { Actions_Medicine.InjectStasisPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Stasis Potion into the {bodypart.name}..."; };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Inject Stasis Potion (50 units): {seconds} seconds, {goldCost} gold";

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
            action = () => { Actions_Medicine.InjectCoagulantPotion(bodypart, seconds, goldCost); messageBox.text = $"Injecting 50 units of Coagulant Potion into the {bodypart.name}..."; };
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
            action = () => { Actions_Charms.ApplyHeartCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Heart Charm to the {bodypart.name}..."; };
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
            action = () => { Actions_Charms.ApplyLungCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Lung Charm to the {bodypart.name}..."; };
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
            action = () => { Actions_Charms.ApplyPetrificationCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Petrification Charm to the {bodypart.name}..."; };
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
            action = () => { Actions_Charms.ApplyBloodRegenCharm(bodypart, seconds, goldCost); messageBox.text = $"Applying a Blood Regeneration Charm to the {bodypart.name}..."; };
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
            action = () => { Actions_Blood.Bandages(bodypart, seconds, goldCost); messageBox.text = $"Applying bandages to the {bodypart.name}..."; };
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
            action = () => { Actions_Blood.Bloodletting(bodypart, seconds, goldCost); messageBox.text = $"Inducing bleeding in the {bodypart.name}..."; };
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
            action = () => { Actions_Blood.AddBlood(bodypart, seconds, goldCost); messageBox.text = $"Injecting 100 units of blood into the {bodypart.name}..."; };
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
            action = () => { Actions_Blood.RemoveBlood(bodypart, seconds, goldCost); messageBox.text = $"Extracting 100 units of blood from the {bodypart.name}..."; };
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
            action = () => { Actions_Surgery.RemoveBodyPart(bodypart, seconds, goldCost); messageBox.text = $"Removing the {bodypart.name}..."; };
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
        UnityEngine.Events.UnityAction action = () => { Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds, goldCost); messageBox.text = $"connecting the {bodyPart1.name} to the {bodyPart2.name}..."; };
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
            action = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds, goldCost); bodypart = null; messageBox.text = $"Destroying the {bodypart.name}..."; };
        }

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Destroy: {seconds} seconds, {goldCost} gold";
    }

    #endregion


    #region OrganSurgery

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
            action = () => { Actions_Surgery.ImplantOrgan(organ, bodypart, seconds, goldCost); messageBox.text = $"Implanting the {organ.name} into the {bodypart.name}..."; };
        }


        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Implant organ into limb: {seconds} seconds, {goldCost} gold";
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
            action = () => { Actions_Surgery.RemoveOrgan(organ, seconds, goldCost); messageBox.text = $"Extracting the {organ.name}..."; };
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
            action = () => { Actions_Surgery.DeleteBodyPart(organ, seconds, goldCost); organ = null; messageBox.text = $"Destroying the {organ.name}..."; };
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
            messageBox.text = "You need to select a bodypart for that!";
        }
        else
        {
            messageBox.text = selectedBodyPart.GenerateDescription();
        }
    }

    public void ExamineSelectedOrgan()
    {
        if (selectedOrgan == null)
        {
            messageBox.text = "You need to select an organ for that!";
        }
        else
        {
            messageBox.text = selectedOrgan.GenerateDescription();
        }
    }

    #endregion



    #region spawnBodyParts

    void AssignSpawnHeartButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnHeart(seconds, goldCost); messageBox.text = $"Spawning a new heart..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new heart: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnLungButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLung(seconds, goldCost); messageBox.text = $"Spawning a new lung..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new lung: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnBrainButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new brain: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnArmButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnArm(seconds, goldCost); messageBox.text = $"Spawning a new arm..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new arm: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnLegButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLeg(seconds, goldCost); messageBox.text = $"Spawning a new leg..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new leg: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnTorsoButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnTorso(seconds, goldCost); messageBox.text = $"Spawning a new torso..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new torso: {seconds} seconds, {goldCost} gold";

    }

    void AssignSpawnHeadButton(Button button)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 250;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnHead(seconds, goldCost); messageBox.text = $"Spawning a new head..."; };

        button.onClick.AddListener(action);

        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = $"Spawn a new head: {seconds} seconds, {goldCost} gold";

    }

    #endregion


    void AssignWaitOneHour(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { lifeMonitor.VictoryCheck(); messageBox.text = "Waiting one hour..."; };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait an hour (Victory Check)";
    }

    void AssignWaitOneMinute(Button button)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(60.0f); messageBox.text = "Waiting one minute..."; };
        button.onClick.AddListener(action);
        Text buttonText = button.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Wait one minute";
    }
}
