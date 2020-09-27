using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BodyPartSelectorManager : MonoBehaviour
{

    private List<BodyPartSelector> headSelectors = new List<BodyPartSelector>();
    public GameObject headSelectorsPanel;
    public GameObject headSelectorPrefab;

    private List<BodyPartSelector> torsoSelectors = new List<BodyPartSelector>();
    public GameObject torsoSelectorsPanel;
    public GameObject torsoSelectorPrefab;

    private List<BodyPartSelector> leftArmSelectors = new List<BodyPartSelector>();
    public GameObject leftArmSelectorsPanel;
    public GameObject leftArmSelectorPrefab;

    private List<BodyPartSelector> rightArmSelectors = new List<BodyPartSelector>();
    public GameObject rightArmSelectorsPanel;
    public GameObject rightArmSelectorPrefab;

    private List<BodyPartSelector> leftLegSelectors = new List<BodyPartSelector>();
    public GameObject leftLegSelectorsPanel;
    public GameObject leftLegSelectorPrefab;

    private List<BodyPartSelector> rightLegSelectors = new List<BodyPartSelector>();
    public GameObject rightLegSelectorsPanel;
    public GameObject rightLegSelectorPrefab;

    private List<BodyPartSelector> organSelectors = new List<BodyPartSelector>();
    public GameObject organSelectorsPanel;
    public GameObject organSelectorPrefab;

    public GameObject rightLungSelectorPrefab;
    public GameObject leftLungSelectorPrefab;
    public GameObject heartSelectorPrefab;
    public GameObject brainSelectorPrefab;
    public GameObject leftEyeyeSelectorPrefab;
    public GameObject rightEyeyeSelectorPrefab;
    public GameObject liverSelectorPrefab;
    public GameObject stomachSelectorPrefab;

    private List<BodyPartSelector> externalSelectors = new List<BodyPartSelector>();
    public GameObject externalSelectorsPanel;

    // Start is called before the first frame update
    void Start()
    {
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

        if (bodyPart is Head)
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
        if (organ is LeftLung)
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
            NewSelector(organ, leftEyeyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
        }
        else if (organ is RightEye)
        {
            NewSelector(organ, rightEyeyeSelectorPrefab, organSelectorsPanel, externalSelectorsPanel);
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
        }
        else
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, notPartOfMainPanel.transform);
            externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
        }
    }

    public void ResetSelectors()
    {
        ClearAllSelectors();
        BodyPart selectedBodyPart = FindObjectOfType<ButtonActions>().selectedBodyPart;

        //organ selectors for external organs, and organs inside selected bodypart
        foreach (Organ organ in FindObjectsOfType<Organ>())
        {
            BodyPart tempSelectedBodyPart = selectedBodyPart;

            //if selected an organ, use parent bodypart for determining which organs to display
            if (selectedBodyPart is Organ)
            {
                tempSelectedBodyPart = selectedBodyPart.connectedBodyParts[0];
            }

            if (organ.connectedBodyParts.Count == 0 || organ.connectedBodyParts[0] == tempSelectedBodyPart)
            {
                NewOrgan(organ);
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
