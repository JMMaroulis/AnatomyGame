using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<GameObject> menuButtons;
    public GameObject selectedBodyPartObject = null;
    public GameObject selectedOrganObject = null;
    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();
    private List<GameObject> organObjects = new List<GameObject>();
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
        bodyPartObjects = new List<GameObject>();
        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyPartObjects.Add(body.transform.GetChild(i).gameObject);
        }
    }

    void PopulateOrgansList()
    {
        organObjects = new List<GameObject>();
        List<Organ> organs = FindObjectsOfType<Organ>().ToList();

        foreach (Organ organ in organs)
        {
            organObjects.Add(organ.transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PopulateBodyPartsList();
        PopulateOrgansList();

        if (selectedBodyPartObject) selectedBodyPartText.text = selectedBodyPartObject.name;
        else selectedBodyPartText.text = "No Body Part Selected";

        if (selectedOrganObject) selectedOrganText.text = selectedOrganObject.name;
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
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bandages(selectedBodyPartObject, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES: " + seconds + " seconds";
    }

    void AssignBloodlettingButton(GameObject buttonObject)
    {
        float seconds = 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bloodletting(selectedBodyPartObject, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BLOODLETTING: " + seconds + " seconds";
    }

    void AssignAddBloodButton(GameObject buttonObject)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.AddBlood(selectedBodyPartObject, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ADD BLOOD: " + seconds + " seconds";
    }

    void AssignRemoveBloodButton(GameObject buttonObject)
    {
        float seconds = 120.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.RemoveBlood(selectedBodyPartObject, seconds); };
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
        int numBodyParts = bodyPartObjects.Count();
        int countStart = bodyPartMenuCounter;
        while(bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart+6))
        {
            //select bodypart, then reset to default
            AssignSelectBodyPart(menuButtons[bodyPartMenuCounter - countStart], bodyPartObjects[bodyPartMenuCounter]);
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
        int numOrgans = organObjects.Count();
        int countStart = organMenuCounter;
        while (organMenuCounter < Mathf.Min(numOrgans, countStart + 6))
        {
            //select organ, then reset to default
            AssignSelectOrgan(menuButtons[organMenuCounter - countStart], organObjects[organMenuCounter]);
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
    void AssignSelectBodyPart(GameObject buttonObject, GameObject bodyPartObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { SelectBodyPart(bodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPartObject.name;
        buttonText.fontSize = 40;
    }

    //select a given bodypart, reset currently selected organ
    void SelectBodyPart(GameObject bodyPartObject)
    {
        selectedBodyPartObject = bodyPartObject;
        selectedOrganObject = null;
    }

    //make a button select a given bodypart
    void AssignSelectOrgan(GameObject buttonObject, GameObject organObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { SelectOrgan(organObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        if (organObject.GetComponent<Organ>().connectedBodyPartsGameObjects.Count != 0)
        {
            buttonText.text = $"{organObject.GetComponent<Organ>().connectedBodyPartsGameObjects[0].name} : {organObject.name}";
        }
        else
        {
            buttonText.text = $"External : {organObject.name}";
        }
        buttonText.fontSize = 40;
    }

    //select a given bodypart
    void SelectOrgan(GameObject organObject)
    {
        selectedOrganObject = organObject;
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
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.RemoveBodyPart(selectedBodyPartObject, seconds); AssignDefaultButtons(); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "AMPUTATE BODYPART: " + seconds + " seconds";
    }

    void AssignRemoveOrganButton(GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.RemoveOrgan(selectedOrganObject, seconds); AssignDefaultButtons(); };
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
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.ImplantOrgan(selectedOrganObject, selectedBodyPartObject, 600.0f); };
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
        int numBodyParts = bodyPartObjects.Count();
        int countStart = bodyPartMenuCounter;
        while (bodyPartMenuCounter < Mathf.Min(numBodyParts, countStart + 6))
        {
            //assign button to connect chosen with selected, then go back to default
            AssignConnectTwoBodyParts(selectedBodyPartObject, bodyPartObjects[bodyPartMenuCounter], menuButtons[bodyPartMenuCounter - countStart]);
            menuButtons[bodyPartMenuCounter - countStart].GetComponent<Button>().onClick.AddListener(AssignDefaultButtons);
            menuButtons[bodyPartMenuCounter - countStart].transform.GetChild(0).gameObject.GetComponent<Text>().text = bodyPartObjects[bodyPartMenuCounter].name;
            bodyPartMenuCounter += 1;
        }

        if (bodyPartMenuCounter < numBodyParts)
        {
            //more bodyparts button
            menuButtons[7].GetComponent<Button>().onClick.AddListener(SelectBodyPartToAttachOptions);
            menuButtons[7].transform.GetChild(0).gameObject.GetComponent<Text>().text = "More...";
        }
    }

    void AssignConnectTwoBodyParts(GameObject bodyPartObject1, GameObject bodyPartObject2, GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.ConnectBodyParts(bodyPartObject1, bodyPartObject2, seconds); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);
    }

    void AssignDestroyBodyPart(GameObject buttonObject)
    {
        float seconds = 10 * 60.0f;
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.DeleteBodyPart(selectedBodyPartObject, seconds); AssignDefaultButtons(); selectedBodyPartObject = null;};
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "DESTROY: " + seconds + " seconds";
    }

    void AssignExamineBodyPart(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { descriptionReporting.FetchDescription(selectedBodyPartObject.GetComponent<BodyPart>()); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "EXAMINE BODYPART";
    }
}
