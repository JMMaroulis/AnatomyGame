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
    private string injuryText;

    // Start is called before the first frame update
    void Start()
    {
        InjurySpawnTracker injurySpawnTracker = GameObject.FindObjectOfType<InjurySpawnTracker>();
        PopulateBodyPartsList();
        PopulateOrgansList();
        EasyInjuries(injurySpawnTracker.easyInjuries);
        MediumInjuries(injurySpawnTracker.mediumInjuries);
        HardInjuries(injurySpawnTracker.hardInjuries);
    }

    //select n random bodyparts
    public void EasyInjuries(int numberOfInjuries)
    {
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

    public void MediumInjuries(int numberOfInjuries)
    {
        for (int i = 0; i < numberOfInjuries; i++)
        {
            //selected bodypart to injure, and injury to apply
            int injuryNumber = Random.Range(0, 6);
            Organ organ = organs[Random.Range(0, organs.Count)];
            BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];

            //apply injury
            switch (injuryNumber)
            {
                case 0:
                    Debug.Log($"Stabbed {organ.name}");
                    injuryText += $"\nThe {organ.name} has been stabbed.";
                    Stab(organ);
                    Stab(organ.connectedBodyParts[0]);
                    break;

                case 1:
                    Debug.Log($"Shot {organ.name}");
                    injuryText += $"\nThe {organ.name} has been shot.";
                    Shoot(organ);
                    Shoot(organ.connectedBodyParts[0]);
                    break;

                case 2:
                    Debug.Log($"Crushed {organ.name}");
                    injuryText += $"\nThe {organ.name} has been poisoned.";
                    SlowPoison(organ);
                    SlowPoison(organ.connectedBodyParts[0]);
                    break;

                case 3:
                    Debug.Log($"{organ.name} missing");
                    injuryText += $"\nThe {organ.name} is missing?!";
                    organs.Remove(organ);
                    Missing(organ);
                    break;

                case 4:
                    if (organ is Eye)
                    {
                        i -= 1;
                        break;
                    }
                    Debug.Log($"{organ.name} petrified");
                    injuryText += $"\nThe {organ.name} has been temporarily petrified.";
                    Petrify(organ);
                    break;

                case 5:
                    if (bodyPart is Torso)
                    {
                        i -= 1;
                        break;
                    }
                    Debug.Log($"Severed {bodyPart.name}");
                    injuryText += $"\nThe {bodyPart.name} has been severed.";
                    Sever(bodyPart);
                    break;

                default:
                    break;
            }
        }

        messageBox.text = injuryText;
    }

    public void HardInjuries(int numberOfInjuries)
    {
        for (int i = 0; i < numberOfInjuries; i++)
        {
            //selected bodypart to injure, and injury to apply
            int injuryNumber = Random.Range(0, 1);
            Organ organ = organs[Random.Range(0, organs.Count)];
            BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];

            //apply injury
            switch (injuryNumber)
            {
                case 0:
                    BodyPart Torso = FindObjectOfType<Torso>();
                    Sever(Torso);

                    Debug.Log($"Severed {Torso.name}");
                    injuryText += $"\nThe {Torso.name} has been stabbed.";
                    Sever(Torso.connectedBodyParts[0]);
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

    public void Sever(BodyPart bodyPart)
    {
        bodyPart.damage += 50;
        Actions_Surgery.RemoveBodyPart(bodyPart, 0, 0);
    }

    public void SlowPoison(BodyPart bodyPart)
    {
        bodyPart.slowPoison += 100;
    }

    public void Missing(BodyPart bodyPart)
    {
        Actions_Surgery.RemoveOrgan((Organ)bodyPart, 0, 0);
        Actions_Surgery.DeleteBodyPart(bodyPart, 0, 0);
    }

    public void Petrify(BodyPart bodyPart)
    {
        Actions_Charms.ApplyPetrificationCharm((Organ)bodyPart, 0, 0);
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

    void PopulateOrgansList()
    {
        organs = new List<Organ>();

        //get organs from bodyparts list
        foreach(BodyPart bodyPart in bodyParts)
        {
            foreach (Organ organ in bodyPart.containedOrgans)
            {
                organs.Add(organ);
            }
        }

    }
}
