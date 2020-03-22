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
    public DescriptionReporting descriptionReporting;
    public Text selectedBodyPartText;
    public Text selectedOrganText;

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

        //get bodyparts from body
        //we can't just use findObejctsOfType<BodyPart>() here, cause that will get all of the organs as well
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyParts.Add(body.transform.GetChild(i).gameObject.GetComponent<BodyPart>());
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

        if (selectedOrgan) selectedOrganText.text = selectedOrgan.name;
        else selectedOrganText.text = "No Organ Selected";

    }

    //make button take us back to default menu
    void AssignDefaultButtons()
    {
        ClearAllButtons();
        AssignSelectBodyPartOptions(menuButtons[1]);
        AssignSelectOrganOptions(menuButtons[4]);
        bodyPartMenuCounter = 0;
        organMenuCounter = 0;

        AssignSelectBloodActionOptions(menuButtons[0]);
        AssignSelectSurgeryActionOptions(menuButtons[2]);
        AssignExamineBodyPart(menuButtons[3]);
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

    void AssignBandagesButton(GameObject buttonObject)
    {
        float seconds = 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bandages(selectedBodyPart, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES: " + seconds + " seconds";
    }

    void AssignBloodlettingButton(GameObject buttonObject)
    {
        float seconds = 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bloodletting(selectedBodyPart, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BLOODLETTING: " + seconds + " seconds";
    }

    void AssignAddBloodButton(GameObject buttonObject)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.AddBlood(selectedBodyPart, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ADD BLOOD: " + seconds + " seconds";
    }

    void AssignRemoveBloodButton(GameObject buttonObject)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.RemoveBlood(selectedBodyPart, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "REMOVE BLOOD: " + seconds + " seconds";
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

    //make a button select a given bodypart
    void AssignSelectBodyPart(GameObject buttonObject, BodyPart bodyPart)
    {
        UnityEngine.Events.UnityAction action1 = () => { SelectBodyPart(bodyPart); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPart.name;
        buttonText.fontSize = 40;
    }

    //select a given bodypart, reset currently selected organ
    void SelectBodyPart(BodyPart bodyPart)
    {
        selectedBodyPart = bodyPart;
        selectedOrgan = null;
    }

    //make a button select a given bodypart
    void AssignSelectOrgan(GameObject buttonObject, Organ organ)
    {
        UnityEngine.Events.UnityAction action1 = () => { SelectOrgan(organ); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

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

    void AssignSelectBloodActionOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBloodActionOptions);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT BLOOD ACTION";
    }

    void SelectBloodActionOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        AssignBandagesButton(menuButtons[0]);
        AssignBloodlettingButton(menuButtons[1]);
        AssignAddBloodButton(menuButtons[2]);
        AssignRemoveBloodButton(menuButtons[3]);

    }

    void AssignSelectSurgeryActionOptions(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectSurgeryActionOptions);
        buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = "SELECT SURGERY ACTION";
    }

    void SelectSurgeryActionOptions()
    {
        ClearAllButtons();

        //cancel button
        menuButtons[6].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
        menuButtons[6].transform.GetChild(0).gameObject.GetComponent<Text>().text = "CANCEL";

        AssignRemoveBodyPartButton(menuButtons[0]);
        AssignAttachBodyPartButton(menuButtons[1]);
        AssignDestroyBodyPart(menuButtons[2]);
        AssignRemoveOrganButton(menuButtons[3]);
        AssignImplantOrganButton(menuButtons[4]);
    }

    void AssignRemoveBodyPartButton(GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.RemoveBodyPart(selectedBodyPart, seconds); AssignDefaultButtons(); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "AMPUTATE BODYPART: " + seconds + " seconds";
    }

    void AssignRemoveOrganButton(GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.RemoveOrgan(selectedOrgan, seconds); AssignDefaultButtons(); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXTRACT ORGAN: " + seconds + " seconds";
    }

    void AssignAttachBodyPartButton(GameObject buttonObject)
    {
        bodyPartMenuCounter = 0;
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBodyPartToAttachOptions);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ATTACH TO... (10 minutes)";
    }

    void AssignImplantOrganButton(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = null;
        if (selectedBodyPart == null || selectedOrgan == null)
        {
            action1 = () => { Debug.Log("You need to select both a bodypart and an organ for that!"); };
        }
        else
        {
            action1 = () => { Actions_Surgery.ImplantOrgan(selectedOrgan, selectedBodyPart, 600.0f); };
        }


        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "IMPLANT ORGAN INTO BODYPART (10 minutes)";
    }

    void SelectBodyPartToAttachOptions()
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
            AssignConnectTwoBodyParts(selectedBodyPart, bodyParts[bodyPartMenuCounter], menuButtons[bodyPartMenuCounter - countStart]);
            menuButtons[bodyPartMenuCounter - countStart].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
            menuButtons[bodyPartMenuCounter - countStart].transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyParts[bodyPartMenuCounter].name;
            bodyPartMenuCounter += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            menuButtons[7].GetComponent<Button>().onClick.AddListener(SelectBodyPartToAttachOptions);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    void AssignConnectTwoBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.ConnectBodyParts(bodyPart1, bodyPart2, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);
    }

    void AssignDestroyBodyPart(GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPart, seconds); AssignDefaultButtons(); selectedBodyPart = null;};
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "DESTROY: " + seconds + " seconds";
    }

    void AssignExamineBodyPart(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { descriptionReporting.FetchDescription(selectedBodyPart.GetComponent<BodyPart>()); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXAMINE BODYPART";
    }
}
