using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartSelectorManager : MonoBehaviour
{

    public List<BodyPartSelector> headSelectors;
    public GameObject headSelectorsPanel;
    public GameObject headSelectorPrefab;

    public List<BodyPartSelector> torsoSelectors;
    public GameObject torsoSelectorsPanel;
    public GameObject torsoSelectorPrefab;

    public List<BodyPartSelector> leftArmSelectors;
    public GameObject leftArmSelectorsPanel;
    public GameObject leftArmSelectorPrefab;

    public List<BodyPartSelector> rightArmSelectors;
    public GameObject rightArmSelectorsPanel;
    public GameObject rightArmSelectorPrefab;

    public List<BodyPartSelector> leftLegSelectors;
    public GameObject leftLegSelectorsPanel;
    public GameObject leftLegSelectorPrefab;

    public List<BodyPartSelector> rightLegSelectors;
    public GameObject rightLegSelectorsPanel;
    public GameObject rightLegSelectorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NewBodyPart(BodyPart bodyPart)
    {
        if (bodyPart is Head)
        {
            GameObject selector = GameObject.Instantiate(headSelectorPrefab, headSelectorsPanel.transform);
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
            headSelectors.Add(selector.GetComponent<BodyPartSelector>());
        }

        if (bodyPart is Torso)
        {
            GameObject selector = GameObject.Instantiate(torsoSelectorPrefab, torsoSelectorsPanel.transform);
            selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
            headSelectors.Add(selector.GetComponent<BodyPartSelector>());
        }

        if (bodyPart is Arm)
        {
            if (leftArmSelectors.Count < rightArmSelectors.Count)
            {
                GameObject selector = GameObject.Instantiate(leftArmSelectorPrefab, leftArmSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                leftArmSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = GameObject.Instantiate(rightArmSelectorPrefab, rightArmSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                rightArmSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }

        if (bodyPart is Leg)
        {
            if (leftLegSelectors.Count < rightLegSelectors.Count)
            {
                GameObject selector = GameObject.Instantiate(leftLegSelectorPrefab, leftLegSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                headSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
            else
            {
                GameObject selector = GameObject.Instantiate(rightLegSelectorPrefab, rightLegSelectorsPanel.transform);
                selector.GetComponent<BodyPartSelector>().bodyPart = bodyPart;
                headSelectors.Add(selector.GetComponent<BodyPartSelector>());
            }
        }
    }

}
