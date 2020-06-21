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

    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassing == true)
        {
            timePassed += Time.deltaTime;
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

