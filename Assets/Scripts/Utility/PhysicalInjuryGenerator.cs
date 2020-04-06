using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalInjuryGenerator : MonoBehaviour
{
    public GameObject body;
    public Text messageBox;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;

    // Start is called before the first frame update
    void Start()
    {
        PopulateBodyPartsList();
        InjureRandomBodypart(2);
    }

    //select n random bodyparts
    public void InjureRandomBodypart(int numberOfInjuries)
    {
        string injuryText = "";

        for (int i = 0; i < numberOfInjuries; i++)
        {
            //selected bodypart to injure, and injury to apply
            BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];
            int injuryNumber = Random.Range(0, 3);

            //apply injury
            switch (injuryNumber)
            {
                case 0:
                    Debug.Log($"Stabbed {bodyPart.name}");
                    injuryText += $"The {bodyPart.name} has been stabbed.\n";
                    Stab(bodyPart);
                    break;

                case 1:
                    Debug.Log($"Shot {bodyPart.name}");
                    injuryText += $"The {bodyPart.name} has been shot.\n";
                    Shoot(bodyPart);
                    break;

                case 2:
                    Debug.Log($"Crushed {bodyPart.name}");
                    injuryText += $"The {bodyPart.name} has been crushed.\n";
                    Crush(bodyPart);
                    break;

                default:
                    break;
            }
        }

        messageBox.text = injuryText;
    }

    //assorted physical injury methods
    public void Stab(BodyPart bodyPart)
    {
        bodyPart.damage = Mathf.Min(bodyPart.damage + 30, bodyPart.damageMax);
        bodyPart.bloodLossRate += 10;
    }

    public void Shoot(BodyPart bodyPart)
    {
        bodyPart.damage = Mathf.Min(bodyPart.damage + 30, bodyPart.damageMax);
        bodyPart.bloodLossRate += 10;
    }

    public void Crush(BodyPart bodyPart)
    {
        bodyPart.damage = Mathf.Min(bodyPart.damage + 30, bodyPart.damageMax);
        bodyPart.bloodLossRate += 10;
    }



    void PopulateBodyPartsList()
    {

        bodyParts = new List<BodyPart>();

        //get bodyparts from body
        for (int i = 0; i < body.transform.childCount; i++)
        {
            bodyParts.Add(body.transform.GetChild(i).GetComponent<BodyPart>());
        }

    }
}
