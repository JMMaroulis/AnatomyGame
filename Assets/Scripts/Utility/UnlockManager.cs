using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    public List<Button> buttons;

    private UnlockTracker unlockTracker;

    // Start is called before the first frame update
    void Start()
    {
        unlockTracker = FindObjectOfType<UnlockTracker>();
        UnityEngine.Events.UnityAction action = null;

        if (unlockTracker.medicine_poison)
        {
            buttons[0].interactable = false;
        }
        action = () => { unlockTracker.medicine_poison = true; NextPatient(); };
        buttons[0].onClick.AddListener(action);
        buttons[0].transform.GetChild(0).GetComponent<Text>().text = "Poison Medication License";

        if (unlockTracker.medicine_speed)
        {
            buttons[1].interactable = false;
        }
        action = () => { unlockTracker.medicine_speed = true; NextPatient(); };
        buttons[1].onClick.AddListener(action);
        buttons[1].transform.GetChild(0).GetComponent<Text>().text = "Temporal Medication License";

        if (unlockTracker.medicine_blood)
        {
            buttons[2].interactable = false;
        }
        action = () => { unlockTracker.medicine_blood = true; NextPatient(); };
        buttons[2].onClick.AddListener(action);
        buttons[2].transform.GetChild(0).GetComponent<Text>().text = "Blood Medication License";

        if (unlockTracker.charms_heart)
        {
            buttons[3].interactable = false;
        }
        action = () => { unlockTracker.charms_heart = true; NextPatient(); };
        buttons[3].onClick.AddListener(action);
        buttons[3].transform.GetChild(0).GetComponent<Text>().text = "Heart Charm License";

        if (unlockTracker.charms_lung)
        {
            buttons[4].interactable = false;
        }
        action = () => { unlockTracker.charms_lung = true; NextPatient(); };
        buttons[4].onClick.AddListener(action);
        buttons[4].transform.GetChild(0).GetComponent<Text>().text = "Lung Charm License";

        if (unlockTracker.charms_blood_regen)
        {
            buttons[5].interactable = false;
        }
        action = () => { unlockTracker.charms_blood_regen = true; NextPatient(); };
        buttons[5].onClick.AddListener(action);
        buttons[5].transform.GetChild(0).GetComponent<Text>().text = "Blood Regeneration Charm License";

        if (unlockTracker.charms_petrification)
        {
            buttons[6].interactable = false;
        }
        action = () => { unlockTracker.charms_petrification = true; NextPatient(); };
        buttons[6].onClick.AddListener(action);
        buttons[6].transform.GetChild(0).GetComponent<Text>().text = "Petrification Charm License";

        if (unlockTracker.spawn)
        {
            buttons[7].interactable = false;
        }
        action = () => { unlockTracker.spawn = true; NextPatient(); };
        buttons[7].onClick.AddListener(action);
        buttons[7].transform.GetChild(0).GetComponent<Text>().text = "Bodypart Delivery Service";

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NextPatient()
    {
        GameObject.FindObjectOfType<InjurySpawnTracker>().NextPatient();
    }

}
