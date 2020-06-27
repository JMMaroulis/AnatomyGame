using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float timePassed;
    private bool timePassing;

    private float mouseoverPanelSizeX;
    private float mouseoverPanelSizeY;

    public bool mouseoverEnabled;
    public GameObject mouseoverPanel;


    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouseoverPanelSizeX = mouseoverPanel.GetComponent<Image>().rectTransform.rect.size.x;
        mouseoverPanelSizeY = mouseoverPanel.GetComponent<Image>().rectTransform.rect.size.y;

        if (timePassing == true)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed >= 0.5f && mouseoverEnabled)
        {
            // TODO: figure out what the hell is going on here, so I can replace the manual inputs with automatic geometry
            Vector3 newPosition = new Vector3(Input.mousePosition.x + 105, Input.mousePosition.y + 23, mouseoverPanel.transform.position.z);
            mouseoverPanel.transform.position = newPosition;
            mouseoverPanel.SetActive(true);
        }
        else
        {
            mouseoverPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
        timePassing = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject" + ": " + timePassed);
        timePassed = 0.0f;
        timePassing = false;
    }
}

