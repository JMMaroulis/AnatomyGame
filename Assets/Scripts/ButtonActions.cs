using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button bandageButton = GameObject.FindGameObjectWithTag("bandages").GetComponent<Button>();
        bandageButton.onClick.AddListener(boop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void boop()
    {
        Debug.Log("boop");
    }
}
