﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyPartSelector : MonoBehaviour, IPointerClickHandler
{
    public BodyPart bodyPart;
    public Image connectedImage;
    public Image disconnectedImage;
    public Image borderHighlight;
    private ButtonActions buttonActions;
    private BodyPartSelectorManager bodyPartSelectorManager;
    private EmbeddedObjectSelectorManager embeddedObjectSelectorManager;


    // Start is called before the first frame update
    void Start()
    {
        bodyPartSelectorManager = FindObjectOfType<BodyPartSelectorManager>();
        embeddedObjectSelectorManager = FindObjectOfType<EmbeddedObjectSelectorManager>();
        buttonActions = FindObjectOfType<ButtonActions>();
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        //should cease to exist if the bodypart doesn't exist
        if (bodyPart == null)
        {
            bodyPartSelectorManager.ResetSelectors();
            embeddedObjectSelectorManager.ResetSelectors();
        }

        connectedImage.enabled = bodyPart.connectedBodyParts.Count > 0;
        disconnectedImage.enabled = bodyPart.connectedBodyParts.Count == 0;

        ColourUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonActions.selectedGameObject = bodyPart.gameObject;
        bodyPartSelectorManager.ResetSelectors();
        embeddedObjectSelectorManager.ResetSelectors();
        buttonActions.ClearAllButtons();
        buttonActions.UpdateMenuButtonsInteractivity(false);
    }

    public void ColourUpdate()
    {
        float bloodLossRatio = bodyPart.bloodLossRate / 40.0f;
        float gb = 1 - bloodLossRatio;

        connectedImage.color = new Color(1, gb, gb);
        disconnectedImage.color = new Color(1, gb, gb);
    }

}
