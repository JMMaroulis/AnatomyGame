using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    public List<GameObject> menuButtons;
    public GameObject selectedBodyPart;

    // Start is called before the first frame update
    void Start()
    {
        AssignBandagesButton(menuButtons[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ClearAllButtons()
    {
        foreach (GameObject buttonObject in menuButtons)
        {
            buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    void AssignBandagesButton(GameObject buttonObject)
    {
        buttonObject.GetComponent<Button>().onClick.AddListener(Bandages);
        Text buttonText = buttonObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        buttonText.text = "BANDAGES";
        buttonText.fontSize = 40;
    }

    void Bandages()
    {
        Debug.Log("applying bandages everywhere, for now");
        selectedBodyPart.GetComponent<BodyPart>().bloodLossRate -= 10;
    }
}
