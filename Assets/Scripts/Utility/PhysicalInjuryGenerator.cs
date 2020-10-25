using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysicalInjuryGenerator : MonoBehaviour
{
    public GameObject body;
    public TextLog textLog;
    private List<BodyPart> bodyParts;
    private List<Organ> organs;
    private UnlockTracker unlockTracker;

    public GameObject bullet;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        InjurySpawnTracker injurySpawnTracker = GameObject.FindObjectOfType<InjurySpawnTracker>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
        PopulateBodyPartsList();
        PopulateOrgansList();
        EasyInjuries(injurySpawnTracker.easyInjuries);
        MediumInjuries(injurySpawnTracker.mediumInjuries);
        HardInjuries(injurySpawnTracker.hardInjuries);
    }

    //select n random bodyparts
    public void EasyInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries != 0)
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
                        textLog.NewLogEntry($"The {bodyPart.name} has been stabbed.");
                        Stab(bodyPart);
                        break;

                    case 1:
                        Debug.Log($"Shot {bodyPart.name}");
                        textLog.NewLogEntry($"The {bodyPart.name} has been shot.");
                        Shoot(bodyPart);
                        break;

                    case 2:
                        Debug.Log($"Crushed {bodyPart.name}");
                        textLog.NewLogEntry($"The {bodyPart.name} has been crushed.");
                        Crush(bodyPart);
                        break;

                    case 3:
                        if (!unlockTracker.medicine_poison)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"Snake bit {bodyPart.name}");
                        textLog.NewLogEntry($"The {bodyPart.name} has been bitten by a venomous snake.");
                        SnakeBite(bodyPart);
                        break;

                    case 4:
                        if (!unlockTracker.medicine_poison)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"Poisoned {bodyPart.name}");
                        textLog.NewLogEntry($"The {bodyPart.name} has been poisoned.");
                        SlowPoison(bodyPart);
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void MediumInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries != 0)
        {

            for (int i = 0; i < numberOfInjuries; i++)
            {
                //selected bodypart to injure, and injury to apply
                int injuryNumber = Random.Range(0, 8);
                Organ organ = organs[Random.Range(0, organs.Count)];
                BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];

                //apply injury
                switch (injuryNumber)
                {
                    case 0:
                        if (!unlockTracker.medicine_blood && !unlockTracker.spawn)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"Stabbed {organ.name}");
                        textLog.NewLogEntry($"The {organ.name} has been stabbed.");
                        Stab(organ);
                        Stab(organ.connectedBodyParts[0]);
                        break;

                    case 1:
                        if (!unlockTracker.medicine_blood && !unlockTracker.spawn)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"Shot {organ.name}");
                        textLog.NewLogEntry($"The {organ.name} has been shot.");
                        Shoot(organ);
                        break;

                    case 2:
                        if (!unlockTracker.medicine_poison && !unlockTracker.spawn)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"Crushed {organ.name}");
                        textLog.NewLogEntry($"The {organ.name} has been poisoned.");
                        SlowPoison(organ);
                        SlowPoison(organ.connectedBodyParts[0]);
                        break;

                    case 3:
                        if (!unlockTracker.spawn)
                        {
                            i--;
                            break;
                        }
                        Debug.Log($"{organ.name} missing");
                        textLog.NewLogEntry($"The {organ.name} is missing?!");
                        Missing(organ);
                        break;

                    case 4:
                        if (organ is Eye || organ is Stomach || organ is Liver || organ is Brain || !(organ.gameObject.GetComponent<PetrificationCharm>() is null) )
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} petrified");
                        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");
                        Petrify(organ);
                        break;

                    case 5:
                        if (bodyPart is Torso || bodyPart is Head)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"Severed {bodyPart.name}");
                        textLog.NewLogEntry($"The {bodyPart.name} has been severed.");
                        Sever(bodyPart);
                        break;

                    case 6:
                        if (organ is Heart || organ is Lung || organ is Brain)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} external");
                        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");
                        Remove(organ);
                        break;

                    case 7:
                        Debug.Log($"Bomb implanted in {bodyPart.name}");
                        textLog.NewLogEntry($"There's an explosive device in the {bodyPart.name}");
                        ImplantBomb(bodyPart);
                        break;

                    default:
                        break;
                }
            }

        }
    }

    public void HardInjuries(int numberOfInjuries)
    {

        if (numberOfInjuries != 0)
        {

            for (int i = 0; i < numberOfInjuries; i++)
            {
                //selected bodypart to injure, and injury to apply
                int injuryNumber = Random.Range(0, 5);
                Organ organ = organs[Random.Range(0, organs.Count)];
                BodyPart bodyPart = bodyParts[Random.Range(0, bodyParts.Count)];

                //apply injury
                switch (injuryNumber)
                {
                    case 0:
                        BodyPart Torso = FindObjectOfType<Torso>();
                        Sever(Torso);
                        Debug.Log($"Severed {Torso.name}");
                        textLog.NewLogEntry($"The {Torso.name} has been severed from all limbs.");
                        break;

                    case 1:
                        BodyPart head = FindObjectOfType<Head>();
                        Sever(head);
                        Debug.Log($"Severed {head.name}");
                        textLog.NewLogEntry($"Decapitation!");
                        break;

                    case 2:
                        if (!(organ is Brain))
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} petrified");
                        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");
                        Petrify(organ);
                        break;

                    case 3:
                        if (!(organ is Brain))
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} petrified");
                        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");
                        Petrify(organ);
                        break;

                    case 4:
                        if ((organ is Heart && !unlockTracker.charms_heart) || (organ is Lung && !unlockTracker.charms_lung) || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart))
                        {
                            if (!(organ is Heart || organ is Lung || organ is Brain))
                            {
                                i -= 1;
                                break;
                            }
                        }
                        Debug.Log($"{organ.name} external");
                        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");
                        Remove(organ);
                        break;

                    case 5:
                        if (!unlockTracker.spawn || (organ is Heart && !unlockTracker.charms_heart) || (organ is Lung && !unlockTracker.charms_lung) || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart))
                        {
                            if (!(organ is Heart || organ is Lung || organ is Brain))
                            {
                                i -= 1;
                                break;
                            }
                        }
                        Debug.Log($"{organ.name} missing");
                        textLog.NewLogEntry($"The {organ.name} is missing!?.");
                        Missing(organ);
                        break;

                    default:
                        break;
                }
            }

        }
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

        if (bodyPart is Organ && bodyPart.connectedBodyParts.Count > 0)
        {
            BodyPart parentBodyPart = bodyPart.connectedBodyParts[0];
            parentBodyPart.damage = Mathf.Min(parentBodyPart.damage + 30, parentBodyPart.damageMax);
            parentBodyPart.bloodLossRate += 10;
        }

        GameObject newBullet = Instantiate(bullet);
        newBullet.name = "Bullet";
        newBullet.GetComponent<Bullet>().Embed(bodyPart);
    }

    public void ImplantBomb(BodyPart bodyPart)
    {
        GameObject newBomb = Instantiate(bomb);
        newBomb.name = "Bomb";
        newBomb.GetComponent<Bomb>().Embed(bodyPart);
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

    public void Missing(Organ organ)
    {
        organs.Remove((Organ)organ);
        Actions_Surgery.RemoveOrgan((Organ)organ, 0, 0);
        Actions_Surgery.DeleteBodyPart(organ, 0, 0);
    }

    public void Remove(Organ organ)
    {
        organs.Remove(organ);
        Actions_Surgery.RemoveOrgan(organ, 0, 0);
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
