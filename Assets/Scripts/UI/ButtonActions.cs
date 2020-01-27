using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonActions : MonoBehaviour
{
    public List<GameObject> menuButtons;
    public GameObject selectedBodyPartObject = null;
    public GameObject body;
    private List<GameObject> bodyPartObjects = new List<GameObject>();
    private int bodyPartMenuCounter = 0;

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
    }

    void PopulateBodyPartsList()
    {
        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            if (!bodyPartObjects.Contains(body.transform.GetChild(i).gameObject))
            {
                bodyPartObjects.Add(body.transform.GetChild(i).gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        PopulateBodyPartsList();
    }

    //make button take us back to default menu
    void AssignDefaultButtons()
    {
        AssignSelectBodyPartOptions(menuButtons[1]);
        bodyPartMenuCounter = 0;

        AssignSelectBloodActionOptions(menuButtons[0]);
        AssignSelectSurgeryActionOptions(menuButtons[2]);

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
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bandages(selectedBodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES";
    }

    void AssignBloodlettingButton(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.Bloodletting(selectedBodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BLOODLETTING";
    }

    void AssignAddBloodButton(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.AddBlood(selectedBodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ADD BLOOD";
    }

    void AssignRemoveBloodButton(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { Actions_Blood.RemoveBlood(selectedBodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "REMOVE BLOOD";
    }

    //make a button take us to the bodypart select menu
    void AssignSelectBodyPartOptions(GameObject buttonObject)
    {
        ClearAllButtons();
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBodyPartOptions);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "SELECT BODY PART";
    }

    //NOTE: Eventually there's going to be bodyparts containing organs.
    //maybe they'll be 'bodyparts' too, maybe a different class of object.
    //best to keep in mind that we most likely won't want to select those here.
    //some sort of 'SelectBodyPartOrganOptions()' method or something.
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

    //make a button select a given bodypart
    void AssignSelectBodyPart(GameObject buttonObject, GameObject bodyPartObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { SelectBodyPart(bodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = bodyPartObject.name;
        buttonText.fontSize = 40;
    }

    //select a given bodypart
    void SelectBodyPart(GameObject bodyPartObject)
    {
        selectedBodyPartObject = bodyPartObject;
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
    }

    void AssignRemoveBodyPartButton(GameObject buttonObject)
    {
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.RemoveBodyPart(selectedBodyPartObject); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "AMPUTATE";
    }

    void AssignAttachBodyPartButton(GameObject buttonObject)
    {
        bodyPartMenuCounter = 0;
        buttonObject.GetComponent<Button>().onClick.AddListener(SelectBodyPartToAttachOptions);

        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "ATTACH TO...";
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
        UnityEngine.Events.UnityAction action1 = () => { Actions_Surgery.ConnectBodyParts(bodyPartObject1, bodyPartObject2); };
        buttonObject.GetComponent<Button>().onClick.AddListener(action1);
    }
}
