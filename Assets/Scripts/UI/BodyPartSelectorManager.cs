using System.Collections;
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

    public GameObject lungSelectorPrefab;
    public GameObject heartSelectorPrefab;
    public GameObject brainSelectorPrefab;
    public GameObject eyeSelectorPrefab;
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
            NewSelector(bodyPart, headSelectorPrefab, headSelectorsPanel);
        }

        else if (bodyPart is Torso)
        {
            NewSelector(bodyPart, torsoSelectorPrefab, torsoSelectorsPanel);
        }

        else if (bodyPart is LeftArm)
        {
            NewSelector(bodyPart, leftArmSelectorPrefab, leftArmSelectorsPanel);
        }

        else if (bodyPart is RightArm)
        {
            NewSelector(bodyPart, rightArmSelectorPrefab, rightArmSelectorsPanel);
        }

        else if (bodyPart is LeftLeg)
        {
            NewSelector(bodyPart, leftLegSelectorPrefab, leftLegSelectorsPanel);
        }

        else if (bodyPart is RightLeg)
        {
            NewSelector(bodyPart, rightLegSelectorPrefab, rightLegSelectorsPanel);
        }

    }

    public void NewOrgan(Organ organ)
    {
        if (organ is Lung)
        {
            NewSelector(organ, lungSelectorPrefab, organSelectorsPanel);
        }

        if (organ is Heart)
        {
            NewSelector(organ, heartSelectorPrefab, organSelectorsPanel);
        }

        if (organ is Brain)
        {
            NewSelector(organ, brainSelectorPrefab, organSelectorsPanel);
        }

        if (organ is Eye)
        {
            NewSelector(organ, eyeSelectorPrefab, organSelectorsPanel);
        }

        if (organ is Liver)
        {
            NewSelector(organ, liverSelectorPrefab, organSelectorsPanel);
        }

        if (organ is Stomach)
        {
            NewSelector(organ, stomachSelectorPrefab, organSelectorsPanel);
        }

    }

    private void NewSelector(BodyPart bodyPart, GameObject selectorPrefab, GameObject panel)
    {
        if (bodyPart.isPartOfMainBody)
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, panel.transform);
            organSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
        }
        else
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, externalSelectorsPanel.transform);
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
            if (organ.connectedBodyParts.Count == 0 || organ.connectedBodyParts[0] == selectedBodyPart)
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
