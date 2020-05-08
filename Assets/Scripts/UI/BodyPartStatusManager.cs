using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class BodyPartStatusManager : MonoBehaviour
{
    private List<BodyPart> bodyParts;
    public GameObject statusPrefab;
    public GameObject statusPanel;

    // Start is called before the first frame update
    void Start()
    {
        PopulateBodyPartsList();
        UpdateStatusCollection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PopulateBodyPartsList()
    {
        bodyParts = FindObjectsOfType<BodyPart>().ToList();
    }

    void UpdateStatusCollection()
    {
        //kill all children of this object
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //add everything
        foreach (BodyPart bodyPart in bodyParts)
        {
            AddStatus(bodyPart);
        }

    }

    public void AddStatus(BodyPart bodyPart)
    {
        GameObject status = Instantiate(statusPrefab, statusPanel.transform);
        status.name = bodyPart.name;

        status.transform.GetChild(0).GetComponent<Text>().text = bodyPart.name;

        BloodBar bloodBar = status.transform.GetChild(1).GetChild(0).GetComponent<BloodBar>();
        OxygenBar oxygenBar = status.transform.GetChild(1).GetChild(1).GetComponent<OxygenBar>();
        HealthBar healthBar = status.transform.GetChild(1).GetChild(2).GetComponent<HealthBar>();

        bloodBar.bodyPart = bodyPart;
        bloodBar.MinMax();
        oxygenBar.bodyPart = bodyPart;
        oxygenBar.MinMax();
        healthBar.bodyPart = bodyPart;
        healthBar.MinMax();
    }

    public void RemoveStatus(BodyPart bodyPart)
    {
        foreach (Transform child in transform)
        {
            if (child.name == bodyPart.name)
            {
                GameObject.Destroy(child.gameObject);
                return;
            }
        }
    }
}
