using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalInjuryGenerator : MonoBehaviour
{
    public GameObject body;
    public Text messageBox;
    public int numberStartingInjuries;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;

    // Start is called before the first frame update
    void Start()
    {
        PopulateBodyPartsList();
        InjureRandomBodypart(numberStartingInjuries);
    }

    //select n random bodyparts
    public void InjureRandomBodypart(int numberOfInjuries)
    {
        string injuryText = "";

        for (int i = 0; i < numberOfInjuries; i++)
        {
            //selected bodypart to injure, and injury to apply
            BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];
            int injuryNumber = Random.Range(0, 5);

            //apply injury
            switch (injuryNumber)
            {
                case 0:
                    Debug.Log($"Stabbed {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been stabbed.";
                    Stab(bodyPart);
                    break;

                case 1:
                    Debug.Log($"Shot {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been shot.";
                    Shoot(bodyPart);
                    break;

                case 2:
                    Debug.Log($"Crushed {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been crushed.";
                    Crush(bodyPart);
                    break;

                case 3:
                    Debug.Log($"Snake bit {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been bitten by a venomous snake.";
                    SnakeBite(bodyPart);
                    break;

                case 4:
                    Debug.Log($"Crushed {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been poisoned.";
                    SlowPoison(bodyPart);
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

    public void SnakeBite(BodyPart bodyPart)
    {
        bodyPart.damage = Mathf.Min(bodyPart.damage + 10, bodyPart.damageMax);
        bodyPart.bloodLossRate += 3;
        bodyPart.slowPoison += 50;
    }

    public void SlowPoison(BodyPart bodyPart)
    {
        bodyPart.slowPoison += 100;
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
