using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<GameObject> menuButtons;
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
        foreach (GameObject buttonObject in menuButtons)
        {
            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
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
    void AssignSelectWaitActionOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectWaitActionOptions);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "WAIT...";
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
    }

    public void SelectMedicineActionOptions_Organ()
    {
        ClearAllButtons();

        AssignInjectHealthPotionButton(menuButtons[0], selectedOrgan);
        AssignInjectAntidoteButton(menuButtons[1], selectedOrgan);
        AssignInjectSlowPoisonButton(menuButtons[2], selectedOrgan);
        AssignInjectStasisPotionButton(menuButtons[3], selectedOrgan);

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
    void AssignSelectBodyPart(GameObject buttonObject, BodyPart bodyPart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectBodyPart(bodyPart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPart.name;
    }

    //select a given bodypart
    void SelectBodyPart(BodyPart bodyPart)
    {
        selectedBodyPart = bodyPart;
    }

    //make a button select a given bodypart
    void AssignSelectOrgan(GameObject buttonObject, Organ organ)
    {
        UnityEngine.Events.UnityAction action = () => { SelectOrgan(organ); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
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

    //make a button take us to the bodypart spawning action menu
    void AssignSelectSpawnBodyPartActionOptions(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action = () => { SelectSpawnOrganActionOptions(); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT ORGAN SPAWNING ACTION";
    }

    public void SelectSpawnOrganActionOptions()
    {
        ClearAllButtons();

        AssignSpawnHeartButton(menuButtons[0]);
        AssignSpawnLungButton(menuButtons[1]);
        AssignSpawnBrainButton(menuButtons[2]);
    }


    #region Medicine

    void AssignInjectHealthPotionButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "INJECT HEALTH POTION (50 units): " + seconds + " seconds";

    }

    void AssignInjectAntidoteButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "INJECT ANTIDOTE (50 units): " + seconds + " seconds";

    }

    void AssignInjectSlowPoisonButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "INJECT SLOW POISON (50 units): " + seconds + " seconds";

    }

    void AssignInjectStasisPotionButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "INJECT STASIS POTION(50 units): " + seconds + " seconds";

    }

    #endregion

    #region Charms

    void AssignApplyHeartCharm(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Apply Heart Charm (30 Minutes): " + seconds + " seconds";

    }

    void AssignApplyLungCharm(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Apply Lung Charm (30 Minutes): " + seconds + " seconds";

    }

    void AssignApplyPetrificationCharm(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Apply Petrification Charm (30 Minutes): " + seconds + " seconds";

    }

    void AssignApplyBloodRegenCharm(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Apply Blood Regen Charm (30 Minutes): " + seconds + " seconds";

    }

    #endregion


    #region Blood

    void AssignBandagesButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES: " + seconds + " seconds";
    }

    void AssignBloodlettingButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BLOODLETTING: " + seconds + " seconds";
    }

    void AssignAddBloodButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ADD BLOOD (100 units): " + seconds + " seconds";
    }

    void AssignRemoveBloodButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "REMOVE BLOOD (100 units): " + seconds + " seconds";
    }

    #endregion


    #region BodyPartSurgery

    void AssignRemoveBodyPartButton(GameObject buttonObject, BodyPart bodypart)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "AMPUTATE BODYPART: " + seconds + " seconds";
    }


    void AssignAttachBodyPartButton(GameObject buttonObject, BodyPart bodypart)
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
        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
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
            //assign button to connect chosen with selected, then go back to default
            AssignConnectTwoBodyParts(bodypart, bodyParts[bodyPartMenuCounter], menuButtons[bodyPartMenuCounter - countStart]);
            menuButtons[bodyPartMenuCounter - countStart].transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyParts[bodyPartMenuCounter].name;
            bodyPartMenuCounter += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            UnityEngine.Events.UnityAction action = () => { SelectBodyPartToAttachOptions(bodypart); };
            menuButtons[7].GetComponent<Button>().onClick.AddListener(action);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }


    void AssignConnectTwoBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 20;
        UnityEngine.Events.UnityAction action = () => { Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds, goldCost); messageBox.text = $"connecting the {bodyPart1.name} to the {bodyPart2.name}..."; };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
    }

    void AssignDestroyBodyPart(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;

        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else if (bodypart == null || selectedBodyPart.connectedBodyParts.Count() != 0)
        {
            action = () => { messageBox.text = "That bodypart is still connected to things!"; };
        }
        else
        {
            action = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds, goldCost); bodypart = null; messageBox.text = $"Destroying the {bodypart.name}..."; };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "DESTROY: " + seconds + " seconds";
    }

    #endregion


    #region OrganSurgery

    void AssignImplantOrganButton(GameObject buttonObject, BodyPart bodypart, Organ organ)
    {
        float seconds = 60.0f * 10.0f;
        int goldCost = 0;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null || organ == null)
        {
            action = () => { messageBox.text = "You need to select both a bodypart and an organ for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.ImplantOrgan(organ, bodypart, seconds, goldCost); messageBox.text = $"Implanting the {organ.name} into the {bodypart.name}..."; };
        }


        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "IMPLANT ORGAN INTO BODYPART (10 minutes)";
    }


    void AssignRemoveOrganButton(GameObject buttonObject, Organ organ)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXTRACT ORGAN: " + seconds + " seconds";
    }


    void AssignDestroyOrgan(GameObject buttonObject, Organ organ)
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

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "DESTROY: " + seconds + " seconds";
    }

    #endregion


    #region Examine

    void AssignExamineBodyPart(GameObject buttonObject, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { messageBox.text = bodypart.GenerateDescription(); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXAMINE BODYPART";
    }

    void AssignExamineOrgan(GameObject buttonObject, Organ organ)
    {
        UnityEngine.Events.UnityAction action = null;
        if (organ == null)
        {
            action = () => { messageBox.text = "You need to select an organ for that!"; };
        }
        else
        {
            action = () => { messageBox.text = organ.GenerateDescription(); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXAMINE ORGAN";
    }

    #endregion



    #region spawnBodyParts

    void AssignSpawnHeartButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnHeart(seconds, goldCost); messageBox.text = $"Spawning a new heart..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new heart: " + seconds + " seconds";

    }

    void AssignSpawnLungButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnLung(seconds, goldCost); messageBox.text = $"Spawning a new lung..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new lung: " + seconds + " seconds";

    }

    void AssignSpawnBrainButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new brain: " + seconds + " seconds";

    }

    void AssignSpawnArmButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new brain: " + seconds + " seconds";

    }

    void AssignSpawnLegButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new brain: " + seconds + " seconds";

    }

    void AssignSpawnTorsoButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new brain: " + seconds + " seconds";

    }

    void AssignSpawnHeadButton(GameObject buttonObject)
    {
        float seconds = 60.0f * 5.0f;
        int goldCost = 500;
        UnityEngine.Events.UnityAction action = () => { Actions_SpawnBodyParts.SpawnBrain(seconds, goldCost); messageBox.text = $"Spawning a new brain..."; };

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "Spawn a new brain: " + seconds + " seconds";

    }

    #endregion


    void AssignWaitOneHour(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action = () => { lifeMonitor.VictoryCheck(); messageBox.text = "Waiting one hour..."; };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "WAIT AN HOUR";
    }

    void AssignWaitOneMinute(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action = () => { clock.StartClockUntil(60.0f); messageBox.text = "Waiting one minute..."; };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "WAIT ONE MINUTE";
    }
}
