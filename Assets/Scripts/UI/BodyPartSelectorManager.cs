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
            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(headSelectorPrefab, headSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                headSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(headSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }

        }

        else if (bodyPart is Torso)
        {
            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(torsoSelectorPrefab, torsoSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                torsoSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(torsoSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

        else if (bodyPart is LeftArm)
        {
            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(leftArmSelectorPrefab, leftArmSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                leftArmSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(leftArmSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

        else if (bodyPart is RightArm)
        {
            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(rightArmSelectorPrefab, rightArmSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                rightArmSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(rightArmSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

        else if (bodyPart is LeftLeg)
        {

            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(leftLegSelectorPrefab, leftLegSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                leftLegSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(leftLegSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

        else if (bodyPart is RightLeg)
        {
            if (bodyPart.isPartOfMainBody)
            {
                GameObject selector = Instantiate(rightLegSelectorPrefab, rightLegSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                rightLegSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = Instantiate(rightLegSelectorPrefab, externalSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

    }

    public void NewOrgan(Organ organ)
    {
        if (organ.isPartOfMainBody)
        {
            GameObject selector = GameObject.Instantiate(organSelectorPrefab, organSelectorsPanel.transform);
            organSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = organ;
        }
        else
        {
            GameObject selector = GameObject.Instantiate(organSelectorPrefab, externalSelectorsPanel.transform);
            externalSelectors.Add(selector.GetComponent<BodyPartSelector>());
            selector.GetComponent<BodyPartSelector>().bodyPart = organ;
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
