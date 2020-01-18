﻿using System.Collections;
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

        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyPartObjects.Add(body.transform.GetChild(i).gameObject);
        }

        //set button font sizes
        foreach (GameObject buttonObject in menuButtons)
        {
            Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            buttonText.fontSize = 40;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //make button take us back to default menu
    void AssignDefaultButtons()
    {
        AssignSelectBodyPartOptions(menuButtons[1]);
        bodyPartMenuCounter = 0;
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

    //make a button apply bandages to selected bodypart
    void AssignBandagesButton(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(Bandages);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES";
    }

    //apply bandages to selected bodypart
    void Bandages()
    {
        Debug.Log("applying bandages to" + selectedBodyPartObject.name);
        selectedBodyPartObject.GetComponent<BodyPart>().bloodLossRate -= 10;
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


}