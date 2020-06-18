using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    void Start()
    {
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void SaveAndQuit()
    {
        FindObjectOfType<SaveManager>().EncodeTrackers();
        Quit();
    }

    public void CloseDialogue()
    {
        Debug.Log("go back");

        this.gameObject.SetActive(false);
    }

    public void OpenDialogue()
    {
        this.gameObject.SetActive(true);
    }
}
