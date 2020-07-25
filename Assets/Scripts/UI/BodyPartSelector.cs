using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyPartSelector : MonoBehaviour, IPointerClickHandler
{
    public BodyPart bodyPart;
    private ButtonActions buttonActions;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        buttonActions = FindObjectOfType<ButtonActions>();
        text = transform.GetChild(0).GetComponent<Text>();
        text.text = bodyPart.name;
    }

    // Update is called once per frame
    void Update()
    {
        //should cease to exist if the bodypart doesn't exist
        if (bodyPart == null)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonActions.selectedBodyPart = bodyPart;
        if (!(bodyPart is Organ))
        {
            FindObjectOfType<BodyPartSelectorManager>().SelectBodyPart(bodyPart);
        }
        FindObjectOfType<ButtonActions>().ClearAllButtons();
    }

}
