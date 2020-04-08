using System.Collections;
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
    public Text descriptionText;
    public Text messageBox;
    public LifeMonitor lifeMonitor;

    // Start is called before the first frame update
    void Start()
    {
        //assign starting button actions
        AssignDefaultButtons();

        //button 0 selected by default
        menuButtons[0].GetComponent<Button>().Select();

        PopulateBodyPartsList();

        //set button font sizes
        foreach (GameObject buttonObject in menuButtons)
        {
            Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            buttonText.fontSize = 40;
        }

        selectedBodyPartText.text = "No Body Part Selected";
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

        if (selectedBodyPart) selectedBodyPartText.text = selectedBodyPart.name;
        else selectedBodyPartText.text = "No Body Part Selected";

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

    //instate default menu
    void AssignDefaultButtons()
    {
        bodyPartMenuCounter = 0;
        organMenuCounter = 0;

        ClearAllButtons();

        AssignSelectBodyPartOptions(menuButtons[0]);
        AssignSelectOrganOptions(menuButtons[1]);
        AssignSelectBodyPartActionOptions(menuButtons[2]);
        AssignSelectOrganActionOptions(menuButtons[3]);

        AssignVictoryCheck(menuButtons[6]);
    }


    //make a button take us to the bodypart action select menu
    void AssignSelectBodyPartActionOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBodyPartActionOptions);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT BODYPART ACTION";
    }

    void SelectBodyPartActionOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        AssignSelectSurgeryActionOptions(menuButtons[0], selectedBodyPart);
        AssignSelectBloodActionOptions(menuButtons[1], selectedBodyPart);
        AssignSelectMedicineActionOptions(menuButtons[2], selectedBodyPart);
        AssignExamineBodyPart(menuButtons[3], selectedBodyPart);

    }


    //make a button take us to the organ action select menu
    void AssignSelectOrganActionOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectOrganActionOptions);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT ORGAN ACTION";
    }

    void SelectOrganActionOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        AssignSelectSurgeryActionOptions(menuButtons[0], selectedOrgan);
        AssignSelectBloodActionOptions(menuButtons[1], selectedOrgan);
        AssignSelectMedicineActionOptions(menuButtons[2], selectedOrgan);
        AssignExamineOrgan(menuButtons[3], selectedOrgan);
    }


    //make a button take us to the bodypart select menu
    void AssignSelectBodyPartOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBodyPartOptions);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "SELECT BODY PART";
    }

    void SelectBodyPartOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        while(bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart+6))
        {
            //select bodypart, then reset to default
            AssignSelectBodyPart(menuButtons[bodyPartMenuCounter - countStart], bodyParts[bodyPartMenuCounter]);
            menuButtons[bodyPartMenuCounter - countStart].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
            bodyPartMenuCounter += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            menuButtons[7].GetComponent<Button>().onClick.AddListener(SelectBodyPartOptions);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }


    //make a button take us to the organ select menu
    void AssignSelectOrganOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectOrganOptions);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "SELECT ORGAN";
    }

    void SelectOrganOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        //put organs on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numOrgans = organs.Count();
        int countStart = organMenuCounter;
        while (organMenuCounter < Mathf.Min(numOrgans, countStart + 6))
        {
            //select organ, then reset to default
            AssignSelectOrgan(menuButtons[organMenuCounter - countStart], organs[organMenuCounter]);
            menuButtons[organMenuCounter - countStart].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
            organMenuCounter += 1;
        }

        if (organMenuCounter < numOrgans)
        {
            //more organs button
            menuButtons[7].GetComponent<Button>().onClick.AddListener(SelectOrganOptions);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }


    }


    //make a button take us to the surgery action menu
    void AssignSelectSurgeryActionOptions(GameObject buttonObject, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectSurgeryActionOptions(bodypart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT SURGERY ACTION";
    }

    void SelectSurgeryActionOptions(BodyPart bodypart)
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        if (bodypart is Organ)
        {
            AssignRemoveOrganButton(menuButtons[0], (Organ)bodypart);
            AssignImplantOrganButton(menuButtons[1], selectedBodyPart, (Organ)bodypart);
            AssignDestroyOrgan(menuButtons[2], (Organ)bodypart);
        }
        else
        {
            AssignRemoveBodyPartButton(menuButtons[0], bodypart);
            AssignAttachBodyPartButton(menuButtons[1], bodypart);
            AssignDestroyBodyPart(menuButtons[2], bodypart);
        }

    }


    //make a button take us to the blood action menu
    void AssignSelectBloodActionOptions(GameObject buttonObject, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectBloodActionOptions(bodypart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT BLOOD ACTION";
    }

    void SelectBloodActionOptions(BodyPart bodypart)
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        if (bodypart is Organ)
        {
            AssignAddBloodButton(menuButtons[0], bodypart);
            AssignRemoveBloodButton(menuButtons[1], bodypart);
        }
        else
        {
            AssignBandagesButton(menuButtons[0], bodypart);
            AssignBloodlettingButton(menuButtons[1], bodypart);
            AssignAddBloodButton(menuButtons[2], bodypart);
            AssignRemoveBloodButton(menuButtons[3], bodypart);
        }
    }


    //make a button take us to the medicine action
    void AssignSelectMedicineActionOptions(GameObject buttonObject, BodyPart bodypart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectMedicineActionOptions(bodypart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT MEDICINE ACTION";
    }

    void SelectMedicineActionOptions(BodyPart bodypart)
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        if (bodypart is Organ)
        {
            AssignInjectHealthPotionButton(menuButtons[0], bodypart);
        }
        else
        {
            AssignInjectHealthPotionButton(menuButtons[0], bodypart);
        }
    }





    #region Selection

    //make a button select a given bodypart
    void AssignSelectBodyPart(GameObject buttonObject, BodyPart bodyPart)
    {
        UnityEngine.Events.UnityAction action = () => { SelectBodyPart(bodyPart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPart.name;
        buttonText.fontSize = 40;
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
        buttonText.fontSize = 40;
    }

    //select a given bodypart
    void SelectOrgan(Organ organ)
    {
        selectedOrgan = organ;
    }

    #endregion


    #region Medicine

    void AssignInjectHealthPotionButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 10.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Medicine.InjectHealthPotion(bodypart, seconds); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "INJECT HEALTH POTION: " + seconds + " seconds";

    }

    #endregion


    #region Blood

    void AssignBandagesButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 60.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.Bandages(bodypart, seconds); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES: " + seconds + " seconds";
    }

    void AssignBloodlettingButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 60.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.Bloodletting(bodypart, seconds); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BLOODLETTING: " + seconds + " seconds";
    }

    void AssignAddBloodButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select something for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.AddBlood(bodypart, seconds); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ADD BLOOD: " + seconds + " seconds";
    }

    void AssignRemoveBloodButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Blood.RemoveBlood(bodypart, seconds); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "REMOVE BLOOD: " + seconds + " seconds";
    }

    #endregion


    #region BodyPartSurgery

    void AssignRemoveBodyPartButton(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null)
        {
            action = () => { messageBox.text = "You need to select a bodypart for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.RemoveBodyPart(bodypart, seconds); AssignDefaultButtons(); };
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

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        //put bodyparts on first 6 buttons
        //TODO: Order this list somehow. Alphabetically, maybe?
        int numBodyParts = bodyParts.Count();
        int countStart = bodyPartMenuCounter;
        while (bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart + 6))
        {
            //assign button to connect chosen with selected, then go back to default
            AssignConnectTwoBodyParts(bodypart, bodyParts[bodyPartMenuCounter], menuButtons[bodyPartMenuCounter - countStart]);
            menuButtons[bodyPartMenuCounter - countStart].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
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
        UnityEngine.Events.UnityAction action = () => { Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
    }

    void AssignDestroyBodyPart(GameObject buttonObject, BodyPart bodypart)
    {
        float seconds = 10 * 60.0f;
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
            action = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds); AssignDefaultButtons(); selectedBodyPart = null; };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "DESTROY: " + seconds + " seconds";
    }

    #endregion


    #region OrganSurgery

    void AssignImplantOrganButton(GameObject buttonObject, BodyPart bodypart, Organ organ)
    {
        UnityEngine.Events.UnityAction action = null;
        if (bodypart == null || organ == null)
        {
            action = () => { messageBox.text = "You need to select both a bodypart and an organ for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.ImplantOrgan(organ, bodypart, 600.0f); };
        }


        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "IMPLANT ORGAN INTO BODYPART (10 minutes)";
    }


    void AssignRemoveOrganButton(GameObject buttonObject, Organ organ)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action = null;
        if (organ == null)
        {
            action = () => { messageBox.text = "You need to select an organ for that!"; };
        }
        else
        {
            action = () => { Actions_Surgery.RemoveOrgan(organ, seconds); AssignDefaultButtons(); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXTRACT ORGAN: " + seconds + " seconds";
    }


    void AssignDestroyOrgan(GameObject buttonObject, Organ organ)
    {
        float seconds = 10 * 60.0f;
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
            action = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds); AssignDefaultButtons(); selectedBodyPart = null; };
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
            action = () => { descriptionText.text = bodypart.GenerateDescription(); };
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
            action = () => { descriptionText.text = organ.GenerateDescription(); };
        }

        buttonObject.GetComponent<Button>().onClick.AddListener(action);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXAMINE ORGAN";
    }

    #endregion


    void AssignVictoryCheck(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action = () => { lifeMonitor.VictoryCheck(); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "WAIT AN HOUR";
    }
}
