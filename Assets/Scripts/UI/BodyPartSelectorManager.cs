using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BodyPartSelectorManager : MonoBehaviour
{
    //selector tracker lists
    private List<BodyPartSelector> headSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> torsoSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> leftArmSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> rightArmSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> leftLegSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> rightLegSelectors = new List<BodyPartSelector>();
    private List<BodyPartSelector> organSelectors = new List<BodyPartSelector>();

    //panels
    public GameObject headSelectorsPanel;
    public GameObject torsoSelectorsPanel;
    public GameObject leftArmSelectorsPanel;
    public GameObject rightArmSelectorsPanel;
    public GameObject leftLegSelectorsPanel;
    public GameObject rightLegSelectorsPanel;
    public GameObject organSelectorsPanel;

    //selector prefabs
    public GameObject headSelectorPrefab;
    public GameObject torsoSelectorPrefab;
    public GameObject leftArmSelectorPrefab;
    public GameObject rightArmSelectorPrefab;
    public GameObject leftLegSelectorPrefab;
    public GameObject rightLegSelectorPrefab;

    public GameObject clockworkheadSelectorPrefab;
    public GameObject clockworktorsoSelectorPrefab;
    public GameObject clockworkleftArmSelectorPrefab;
    public GameObject clockworkrightArmSelectorPrefab;
    public GameObject clockworkleftLegSelectorPrefab;
    public GameObject clockworkrightLegSelectorPrefab;

    public GameObject rightLungSelectorPrefab;
    public GameObject leftLungSelectorPrefab;
    public GameObject heartSelectorPrefab;
    public GameObject brainSelectorPrefab;
    public GameObject leftEyeSelectorPrefab;
    public GameObject rightEyeSelectorPrefab;
    public GameObject liverSelectorPrefab;
    public GameObject stomachSelectorPrefab;

    public GameObject clockworkrightLungSelectorPrefab;
    public GameObject clockworkleftLungSelectorPrefab;
    public GameObject clockworkheartSelectorPrefab;
    public GameObject clockworkbrainSelectorPrefab;
    public GameObject clockworkleftEyeSelectorPrefab;
    public GameObject clockworkrightEyeSelectorPrefab;
    public GameObject clockworkliverSelectorPrefab;
    public GameObject clockworkstomachSelectorPrefab;

    private List<BodyPartSelector> externalSelectors = new List<BodyPartSelector>();
    public GameObject externalSelectorsPanel;

    private ButtonActions buttonActions;

    // Start is called before the first frame update
    void Start()
    {
        buttonActions = FindObjectOfType<ButtonActions>();

        List<BodyPart> bodyParts = FindObjectsOfType<BodyPart>().ToList();
        foreach (BodyPart bodyPart in bodyParts)
        {
            if (!(bodyPart is Organ))
            {
                NewBodyPart(bodyPart);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewBodyPart(BodyPart bodyPart)
    {
        if (bodyPart is ClockworkHead)
        {
            NewSelector(bodyPart, clockworkheadSelectorPrefab, headSelectorsPanel, headSelectorsPanel);
        }

        else if (bodyPart is ClockworkTorso)
        {
            NewSelector(bodyPart, clockworktorsoSelectorPrefab, torsoSelectorsPanel, torsoSelectorsPanel);
        }

        else if (bodyPart is ClockworkLeftArm)
        {
            NewSelector(bodyPart, clockworkleftArmSelectorPrefab, leftArmSelectorsPanel, leftArmSelectorsPanel);
        }

        else if (bodyPart is ClockworkRightArm)
        {
            NewSelector(bodyPart, clockworkrightArmSelectorPrefab, rightArmSelectorsPanel, rightArmSelectorsPanel);
        }

        else if (bodyPart is ClockworkLeftLeg)
        {
            NewSelector(bodyPart, clockworkleftLegSelectorPrefab, leftLegSelectorsPanel, leftLegSelectorsPanel);
        }

        else if (bodyPart is ClockworkRightLeg)
        {
            NewSelector(bodyPart, clockworkrightLegSelectorPrefab, rightLegSelectorsPanel, rightLegSelectorsPanel);
        }

        else if (bodyPart is Head)
        {
            NewSelector(bodyPart, headSelectorPrefab, headSelectorsPanel, headSelectorsPanel);
        }

        else if (bodyPart is Torso)
        {
            NewSelector(bodyPart, torsoSelectorPrefab, torsoSelectorsPanel, torsoSelectorsPanel);
        }

        else if (bodyPart is LeftArm)
        {
            NewSelector(bodyPart, leftArmSelectorPrefab, leftArmSelectorsPanel, leftArmSelectorsPanel);
        }

        else if (bodyPart is RightArm)
        {
            NewSelector(bodyPart, rightArmSelectorPrefab, rightArmSelectorsPanel, rightArmSelectorsPanel);
        }

        else if (bodyPart is LeftLeg)
        {
            NewSelector(bodyPart, leftLegSelectorPrefab, leftLegSelectorsPanel, leftLegSelectorsPanel);
        }

        else if (bodyPart is RightLeg)
        {
            NewSelector(bodyPart, rightLegSelectorPrefab, rightLegSelectorsPanel, rightLegSelectorsPanel);
        }

    }

    public void NewOrgan(Organ organ)
    {
        if (organ is ClockworkLeftLung)
        {
            NewSelector(organ, clockworkleftLungSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkRightLung)
        {
            NewSelector(organ, clockworkrightLungSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkHeart)
        {
            NewSelector(organ, clockworkheartSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkBrain)
        {
            NewSelector(organ, clockworkbrainSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkLeftEye)
        {
            NewSelector(organ, clockworkleftEyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }
        else if (organ is ClockworkRightEye)
        {
            NewSelector(organ, clockworkrightEyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkLiver)
        {
            NewSelector(organ, clockworkliverSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is ClockworkStomach)
        {
            NewSelector(organ, clockworkstomachSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is LeftLung)
        {
            NewSelector(organ, leftLungSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is RightLung)
        {
            NewSelector(organ, rightLungSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is Heart)
        {
            NewSelector(organ, heartSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is Brain)
        {
            NewSelector(organ, brainSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is LeftEye)
        {
            NewSelector(organ, leftEyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }
        else if (organ is RightEye)
        {
            NewSelector(organ, rightEyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is Liver)
        {
            NewSelector(organ, liverSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

        else if (organ is Stomach)
        {
            NewSelector(organ, stomachSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }

    }

    private void NewSelector(BodyPart bodyPart, GameObject selectorPrefab, GameObject panel, GameObject notPartOfMainPanel)
    {
        if ((bodyPart.isPartOfMainBody && bodyPart is BodyPart) || (bodyPart.connectedBodyParts.Count == 1 && bodyPart is Organ))
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, panel.transform);
            organSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
            selector.GetComponent<BodyPartSelector>().borderHighlight.enabled = ShouldBeHighlighted(bodyPart);
        }
        else
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, notPartOfMainPanel.transform);
            externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
            selector.GetComponent<BodyPartSelector>().borderHighlight.enabled = ShouldBeHighlighted(bodyPart);
        }

    }

    private bool ShouldBeHighlighted(BodyPart bodyPart)
    {
        if (buttonActions.selectedGameObject == null)
        {
            return false;
        }

        //if bodypart is selected
        if (bodyPart.gameObject == buttonActions.selectedGameObject)
        {
            return true;
        }

        //if selected organ inside bodypart is selected
        if (buttonActions.selectedGameObject.GetComponent<Organ>() != null)
        {
            if (bodyPart.containedOrgans.Contains(buttonActions.selectedGameObject.GetComponent<Organ>()))
            {
                return true;
            }
        }


        //if selected embedded inside bodypart is selected
        if (buttonActions.selectedGameObject.GetComponent<EmbeddedObject>() != null)
        {
            BodyPart parent = buttonActions.selectedGameObject.GetComponent<EmbeddedObject>().parentBodyPart;
            if (parent == null)
            {
                return false;
            }

            if (bodyPart == parent)
            {
                return true;
            }


            foreach (Organ organ in parent.containedOrgans)
            {
                if (organ.embeddedObjects.Contains(buttonActions.selectedGameObject.GetComponent<EmbeddedObject>()))
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void ResetSelectors(BodyPart selectedBodyPartOverride = null)
    {
        ClearAllSelectors();

        BodyPart selectedBodyPart;
        try
        {
            selectedBodyPart = selectedBodyPartOverride ?? FindObjectOfType<ButtonActions>().selectedGameObject.GetComponent<BodyPart>();
        }
        catch (System.Exception)
        {
            selectedBodyPart = null;
        }

        if (!(selectedBodyPart is null))
        {
            //organ selectors for external organs, and organs inside selected bodypart
            foreach (Organ organ in FindObjectsOfType<Organ>())
            {
                BodyPart tempSelectedBodyPart = selectedBodyPart;

                //if selected an organ, use parent bodypart for determining which organs to display
                if (selectedBodyPart is Organ && selectedBodyPart.connectedBodyParts.Count() > 0)
                {
                    tempSelectedBodyPart = selectedBodyPart.connectedBodyParts[0];
                }

                if (organ.connectedBodyParts.Count == 0 || organ.connectedBodyParts[0] == tempSelectedBodyPart)
                {
                    NewOrgan(organ);
                }
            }
        }

        //make new bodypart selectors
        foreach (BodyPart bodyPart in FindObjectsOfType<BodyPart>())
        {
            if (!(bodyPart is Organ))
            {
                NewBodyPart(bodyPart);
            }
        }
    }

    private void ClearAllSelectors()
    {
        headSelectors      = ClearSelectorsFromList(headSelectors);
        torsoSelectors     = ClearSelectorsFromList(torsoSelectors);
        leftArmSelectors   = ClearSelectorsFromList(leftArmSelectors);
        rightArmSelectors  = ClearSelectorsFromList(rightArmSelectors);
        leftLegSelectors   = ClearSelectorsFromList(leftLegSelectors);
        rightLegSelectors  = ClearSelectorsFromList(rightLegSelectors);
        organSelectors     = ClearSelectorsFromList(organSelectors);
        externalSelectors  = ClearSelectorsFromList(externalSelectors);
    }

    private List<BodyPartSelector> ClearSelectorsFromList(List<BodyPartSelector> selectors)
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            Destroy(selectors[i].gameObject);
        }
        return new List<BodyPartSelector>();
    }

}
