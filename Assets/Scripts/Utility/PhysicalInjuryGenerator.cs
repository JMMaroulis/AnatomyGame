using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhysicalInjuryGenerator : MonoBehaviour
{
    public GameObject body;
    public TextLog textLog;
    private BodyPartManager bodyPartManager;
    private UnlockTracker unlockTracker;

    public GameObject bullet;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        GameSetupScenarioTracker injurySpawnTracker = GameObject.FindObjectOfType<GameSetupScenarioTracker>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
        EasyInjuries(injurySpawnTracker.easyInjuries);
        MediumInjuries(injurySpawnTracker.mediumInjuries);
        HardInjuries(injurySpawnTracker.hardInjuries);
        FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
    }

    public Organ RandomOrgan()
    {
        Organ organ = bodyPartManager.organs[Random.Range(0, bodyPartManager.organs.Count)];
        return organ;
    }

    public BodyPart RandomLimb()
    {
        BodyPart limb = bodyPartManager.bodyParts[Random.Range(0, bodyPartManager.bodyParts.Count)];
        while (limb is Organ)
        {
            limb = bodyPartManager.bodyParts[Random.Range(0, bodyPartManager.bodyParts.Count)];
        }
        return limb;
    }

    //select n random bodyparts
    public void EasyInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries != 0)
        {

            for (int i = 0; i < numberOfInjuries; i++)
            {

                //select and apply random injury
                int injuryNumber = Random.Range(0, 5);
                BodyPart limb;
                switch (injuryNumber)
                {
                    case 0:
                        limb = RandomLimb();
                        Debug.Log($"Stabbed {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been stabbed.");
                        Stab(limb);
                        break;

                    case 1:
                        limb = RandomLimb();
                        Debug.Log($"Shot {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been shot.");
                        Shoot(limb);
                        break;

                    case 2:
                        limb = RandomLimb();
                        Debug.Log($"Crushed {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been crushed.");
                        Crush(limb);
                        break;

                    case 3:
                        if (!unlockTracker.medicine_poison)
                        {
                            i--;
                            break;
                        }
                        limb = RandomLimb();
                        Debug.Log($"Snake bit {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been bitten by a venomous snake.");
                        SnakeBite(limb);
                        break;

                    case 4:
                        if (!unlockTracker.medicine_poison)
                        {
                            i--;
                            break;
                        }
                        limb = RandomLimb();
                        Debug.Log($"Poisoned {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been poisoned.");
                        SlowPoison(limb);
                        break;

                    default:
                        break;
                }
            }
        }
    }

    public void MediumInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries > 0)
        {

            for (int i = 0; i < numberOfInjuries; i++)
            {
                Organ organ;
                BodyPart limb;
                int n = 0;

                //apply injury
                int injuryNumber = Random.Range(0, 8);
                switch (injuryNumber)
                {
                    case 0:
                        if (!unlockTracker.medicine_blood && !unlockTracker.spawn)
                        {
                            i--;
                            break;
                        }
                        organ = RandomOrgan();
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
                        organ = RandomOrgan();
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
                        organ = RandomOrgan();
                        Debug.Log($"Crushed {organ.name}");
                        textLog.NewLogEntry($"The {organ.name} has been poisoned.");
                        SlowPoison(organ);
                        break;

                    case 3:
                        organ = RandomOrgan();
                        while (organ is Brain || !(organ.gameObject.GetComponent<PetrificationCharm>() is null) || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5 || organ is Brain || (!unlockTracker.spawn && !unlockTracker.spawn_clock))
                        {
                            i -= 1;
                            break;
                        }
                        organ = RandomOrgan();
                        Debug.Log($"{organ.name} missing");
                        textLog.NewLogEntry($"The {organ.name} is missing?!");
                        Missing(organ);
                        break;

                    case 4:
                        organ = RandomOrgan();
                        bool x = false;
                        x = x || (organ is Lung && !unlockTracker.charms_lung);
                        x = x || (organ is Heart && !unlockTracker.charms_heart);
                        x = x || (organ is Brain && (!unlockTracker.charms_heart || !unlockTracker.charms_lung));
                        x = x || !(organ.GetComponent<PetrificationCharm>() is null);
                        while (x || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} petrified");
                        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");
                        Petrify(organ);
                        break;

                    case 5:
                        limb = RandomLimb();
                        while (limb is Torso || limb is Head ||  n < 5)
                        {
                            limb = RandomLimb();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"Severed {limb.name}");
                        textLog.NewLogEntry($"The {limb.name} has been severed.");
                        Sever(limb);
                        break;

                    case 6:
                        organ = RandomOrgan();
                        while (organ is Heart || organ is Brain || organ is Lung || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} external");
                        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");
                        Remove(organ);
                        break;

                    case 7:
                        limb = RandomLimb();
                        Debug.Log($"Bomb implanted in {limb.name}");
                        textLog.NewLogEntry($"There's an explosive device in the {limb.name}");
                        ImplantBomb(limb);
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
            Organ organ;
            BodyPart limb;
            int n = 0;

            for (int i = 0; i < numberOfInjuries; i++)
            {
                //selected bodypart to injure, and injury to apply
                int injuryNumber = Random.Range(0, 5);

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
                        organ = RandomOrgan();
                        while ( !(organ is Brain) || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} petrified");
                        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");
                        Petrify(organ);
                        break;

                    case 4:
                        organ = RandomOrgan();
                        while ((organ is Heart && !unlockTracker.charms_heart) || (organ is Lung && !unlockTracker.charms_lung) || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart) || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
                        }
                        Debug.Log($"{organ.name} external");
                        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");
                        Remove(organ);
                        break;

                    case 5:
                        organ = RandomOrgan();
                        while (!unlockTracker.spawn || (organ is Heart && !unlockTracker.charms_heart) || (organ is Lung && !unlockTracker.charms_lung) || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart) || n < 5)
                        {
                            organ = RandomOrgan();
                            n += 1;
                        }
                        if (n > 5)
                        {
                            i -= 1;
                            break;
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

        EmbeddedObject newBullet = FindObjectOfType<ObjectSpawner>().SpawnBullet("bullet");
        newBullet.Embed(bodyPart);
    }

    public void ImplantBomb(BodyPart bodyPart)
    {
        EmbeddedObject newBomb = FindObjectOfType<ObjectSpawner>().SpawnBomb("bomb");
        newBomb.Embed(bodyPart);
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
        foreach(BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += 20.0f;
        }
        Actions_Surgery.RemoveBodyPartProcess(bodyPart);
        bodyPart.bloodLossRate += 20.0f;
    }

    public void SlowPoison(BodyPart bodyPart)
    {
        bodyPart.slowPoison += 100;
    }

    public void Missing(Organ organ)
    {
        Actions_Surgery.RemoveOrganProcess(organ);
        Actions_Surgery.DeleteBodyPartProcess(organ);
    }

    public void Remove(Organ organ)
    {
        Actions_Surgery.RemoveOrganProcess(organ);
    }

    public void Petrify(BodyPart bodyPart)
    {
        Actions_Charms.ApplyPetrificationCharmProcess((Organ)bodyPart);
    }

    public List<BodyPart> BodyPartsList()
    {
        List<BodyPart> x = FindObjectsOfType<BodyPart>().ToList();
        var y = from bodyPart in x where bodyPart.isPartOfMainBody select bodyPart;
        return y.ToList<BodyPart>();
    }

    public List<Organ> PopulateOrgansList()
    {

        List<BodyPart> x = BodyPartsList();
        List<Organ> organs = new List<Organ>();

        //get organs from bodyparts list
        foreach(BodyPart bodyPart in x)
        {
            foreach (Organ organ in bodyPart.containedOrgans)
            {
                organs.Add(organ);
            }
        }

        return organs;

    }
}
