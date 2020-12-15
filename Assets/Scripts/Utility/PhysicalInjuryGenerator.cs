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

    public GameSetupScenarioTracker gameSetupTracker;

    // Start is called before the first frame update
    void Start()
    {
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        gameSetupTracker = GameObject.FindObjectOfType<GameSetupScenarioTracker>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
    }

    public void GenerateInjuries()
    {
        EasyInjuries(gameSetupTracker.easyInjuries);
        MediumInjuries(gameSetupTracker.mediumInjuries);
        HardInjuries(gameSetupTracker.hardInjuries);
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
            int i = 0;
            while (i < numberOfInjuries)
            {

                //select and apply random injury
                int injuryNumber = Random.Range(0, 5);
                switch (injuryNumber)
                {
                    case 0:
                        if (StabLimb()) { i += 1; }
                        break;

                    case 1:
                        if (ShootLimb()) { i += 1; }
                        break;

                    case 2:
                        if (CrushLimb()) { i += 1; }
                        break;

                    case 3:
                        if (SnakeBite()) { i += 1; }
                        break;

                    case 4:
                        if (SlowPoisonLimb()) { i += 1; }
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
            int i = 0;
            while (i < numberOfInjuries)
            {

                //apply injury
                int injuryNumber = Random.Range(0, 8);
                switch (injuryNumber)
                {
                    case 0:
                        if (StabOrgan()) { i += 1; }
                        break;

                    case 1:
                        if (ShootOrgan()) { i += 1; }
                        break;

                    case 2:
                        if (SlowPoisonOrgan()) { i += 1; }
                        break;

                    case 3:
                        if (MissingOrganMedium()) { i += 1; }
                        break;

                    case 4:
                        if (PetrifyOrganMedium()) { i += 1; }
                        break;

                    case 5:
                        if (SeverLimbMedium()) { i += 1; }
                        break;

                    case 6:
                        if (RemoveOrganMedium()) { i += 1; }
                        break;

                    case 7:
                        if (ImplantBomb()) { i += 1; }
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
            int i = 0;
            while (i < numberOfInjuries)
            {
                //selected bodypart to injure, and injury to apply
                int injuryNumber = Random.Range(0, 4);

                //apply injury
                switch (injuryNumber)
                {
                    case 0:
                        if (SeverTorso()) { i += 1; }
                        break;

                    case 1:
                        if (SeverHead()) { i += 1; }
                        break;

                    case 2:
                        if (RemoveOrganHard()) { i += 1; }
                        break;

                    case 3:
                        if (MissingOrganHard()) { i += 1; }
                        break;

                    default:
                        break;
                }
            }

        }
    }

    //assorted physical injury methods
    public bool StabOrgan()
    {
        if (!unlockTracker.medicine_blood && !unlockTracker.spawn)
        {
            return false;
        }

        Organ organ = RandomOrgan();

        Debug.Log($"Stabbed {organ.name}");
        textLog.NewLogEntry($"The {organ.name} has been stabbed.");

        organ.damage = Mathf.Clamp(organ.damage + 30, 0, organ.damageMax);
        organ.bloodLossRate += 10;

        if (organ.connectedBodyParts.Count() > 0)
        {
            organ.connectedBodyParts[0].damage = Mathf.Clamp(organ.connectedBodyParts[0].damage + 30, 0, organ.connectedBodyParts[0].damageMax);
            organ.connectedBodyParts[0].bloodLossRate += 10;
        }

        return true;
    }

    public bool StabLimb()
    {
        BodyPart limb = RandomLimb();

        Debug.Log($"Stabbed {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been stabbed.");

        limb.damage = Mathf.Clamp(limb.damage + 30, 0, limb.damageMax);
        limb.bloodLossRate += 10;

        return true;
    }

    public bool ShootLimb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Shot {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been shot.");

        limb.damage = Mathf.Min(limb.damage + 30, limb.damageMax);
        limb.bloodLossRate += 10;

        if (limb is Organ && limb.connectedBodyParts.Count > 0)
        {
            BodyPart parentBodyPart = limb.connectedBodyParts[0];
            parentBodyPart.damage = Mathf.Min(parentBodyPart.damage + 30, parentBodyPart.damageMax);
            parentBodyPart.bloodLossRate += 10;
        }

        EmbeddedObject newBullet = FindObjectOfType<ObjectSpawner>().SpawnBullet("bullet");
        newBullet.Embed(limb);

        return true;
    }

    public bool ShootOrgan()
    {
        if (!unlockTracker.medicine_blood && !unlockTracker.spawn)
        {
            return false;
        }

        Organ organ = RandomOrgan();
        Debug.Log($"Shot {organ.name}");
        textLog.NewLogEntry($"The {organ.name} has been shot.");

        organ.damage = Mathf.Min(organ.damage + 30, organ.damageMax);
        organ.bloodLossRate += 10;

        if (organ.connectedBodyParts.Count > 0)
        {
            BodyPart parentBodyPart = organ.connectedBodyParts[0];
            parentBodyPart.damage = Mathf.Min(parentBodyPart.damage + 30, parentBodyPart.damageMax);
            parentBodyPart.bloodLossRate += 10;
        }

        EmbeddedObject newBullet = FindObjectOfType<ObjectSpawner>().SpawnBullet("bullet");
        newBullet.Embed(organ);

        return true;
    }

    public bool ImplantBomb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Bomb implanted in {limb.name}");
        textLog.NewLogEntry($"There's an explosive device in the {limb.name}");

        EmbeddedObject newBomb = FindObjectOfType<ObjectSpawner>().SpawnBomb("bomb");
        newBomb.Embed(limb);

        return true;
    }

    public bool CrushLimb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Crushed {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been crushed.");

        limb.damage = Mathf.Min(limb.damage + 30, limb.damageMax);
        limb.bloodLossRate += 10;

        return true;
    }

    public bool SnakeBite()
    {
        if (!unlockTracker.medicine_poison)
        {
            return false;
        }

        BodyPart limb = RandomLimb();
        Debug.Log($"Snake bit {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been bitten by a venomous snake.");

        limb.damage = Mathf.Min(limb.damage + 10, limb.damageMax);
        limb.bloodLossRate += 3;
        limb.slowPoison += 50;

        return true;
    }

    public bool SeverTorso()
    {
        BodyPart torso = FindObjectOfType<Torso>();

        if (torso.connectedBodyParts.Count() == 0)
        {
            return false;
        }

        Debug.Log($"Severed {torso.name}");
        textLog.NewLogEntry($"The {torso.name} has been severed from all limbs.");

        torso.damage += 50;
        foreach(BodyPart connectedBodyPart in torso.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += 20.0f;
        }
        Actions_Surgery.RemoveBodyPartProcess(torso);
        torso.bloodLossRate += 20.0f;

        return true;
    }

    public bool SeverHead()
    {
        BodyPart head = FindObjectOfType<Head>();

        if (head.connectedBodyParts.Count() == 0)
        {
            return false;
        }

        Debug.Log($"Severed {head.name}");
        textLog.NewLogEntry($"The {head.name} has been severed from all limbs.");

        head.damage += 50;
        foreach (BodyPart connectedBodyPart in head.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += 20.0f;
        }
        Actions_Surgery.RemoveBodyPartProcess(head);
        head.bloodLossRate += 20.0f;

        return true;
    }

    public bool SeverLimbMedium()
    {
        bool x = true;
        int n = 0;
        BodyPart limb = new BodyPart();
        while (x && n < 5)
        {
            limb = RandomLimb();
            x = false;
            x = x || limb.connectedBodyParts.Count() == 0;
            x = x || limb is Head;
            n += 1;
        }

        if (n >= 5)
        {
            return false;
        }

        Debug.Log($"Severed {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been severed from all limbs.");

        limb.damage += 50;
        foreach (BodyPart connectedBodyPart in limb.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += 20.0f;
        }
        Actions_Surgery.RemoveBodyPartProcess(limb);
        limb.bloodLossRate += 20.0f;

        return true;
    }

    public bool SlowPoisonOrgan()
    {
        if (!unlockTracker.medicine_poison)
        {
            return false;
        }

        BodyPart organ = RandomOrgan();
        Debug.Log($"Poisoned {organ.name}");
        textLog.NewLogEntry($"The {organ.name} has been poisoned.");

        organ.slowPoison += 100;

        return true;
    }

    public bool SlowPoisonLimb()
    {
        if (!unlockTracker.medicine_poison)
        {
            return false;
        }

        BodyPart limb = RandomLimb();
        Debug.Log($"Poisoned {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been poisoned.");

        limb.slowPoison += 100;

        return true;
    }

    public bool MissingOrganMedium()
    {
        Organ organ = RandomOrgan();
        int n = 0;
        bool x = true;
        x = x || (organ is Brain);
        x = x || (organ is Heart && !unlockTracker.charms_heart);
        x = x || (organ is Lung && !unlockTracker.charms_lung);
        x = x || !(organ.gameObject.GetComponent<PetrificationCharm>() is null);

        while (x && n < 5)
        {
            organ = RandomOrgan();
            x = x || (organ is Brain);
            x = x || (organ is Heart && !unlockTracker.charms_heart);
            x = x || (organ is Lung && !unlockTracker.charms_lung);
            x = x || !(organ.gameObject.GetComponent<PetrificationCharm>() is null);
            n += 1;
        }
        if (n > 5 || organ is Brain || (!unlockTracker.spawn && !unlockTracker.spawn_clock))
        {
            return false;
        }

        Debug.Log($"{organ.name} missing");
        textLog.NewLogEntry($"The {organ.name} is missing?!");

        Actions_Surgery.RemoveOrganProcess(organ);
        Actions_Surgery.DeleteBodyPartProcess(organ);

        return true;

    }

    public bool MissingOrganHard()
    {
        Organ organ = RandomOrgan();
        int n = 0;

        while (!(organ.gameObject.GetComponent<PetrificationCharm>() is null) && n < 5)
        {
            organ = RandomOrgan();
            n += 1;
        }
        if (n > 5 || (!unlockTracker.spawn && !unlockTracker.spawn_clock))
        {
            return false;
        }

        Debug.Log($"{organ.name} missing");
        textLog.NewLogEntry($"The {organ.name} is missing?!");

        Actions_Surgery.RemoveOrganProcess(organ);
        Actions_Surgery.DeleteBodyPartProcess(organ);

        return true;
    }

    public bool RemoveOrganMedium()
    {
        Organ organ = RandomOrgan();
        int n = 0;
        bool x = false;
        x = x || (organ is Heart && !unlockTracker.charms_heart);
        x = x || (organ is Lung && !unlockTracker.charms_lung);
        x = x || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart);
        while (x && n < 5)
        {
            organ = RandomOrgan();
            x = false;
            x = x || (organ is Heart && !unlockTracker.charms_heart);
            x = x || (organ is Lung && !unlockTracker.charms_lung);
            x = x || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart);
            n += 1;
        }
        if (n > 5)
        {
            return false;
        }
        Debug.Log($"{organ.name} external");
        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");

        Actions_Surgery.RemoveOrganProcess(organ);

        return true;
    }

    public bool RemoveOrganHard()
    {
        Organ organ = RandomOrgan();
        Debug.Log($"{organ.name} external");
        textLog.NewLogEntry($"The {organ.name} requires re-implanting.");

        Actions_Surgery.RemoveOrganProcess(organ);

        return true;
    }

    public bool PetrifyOrganMedium()
    {
        Organ organ = RandomOrgan();
        int n = 0;
        bool x = false;
        x = x || (organ is Lung && !unlockTracker.charms_lung);
        x = x || (organ is Heart && !unlockTracker.charms_heart);
        x = x || (organ is Brain && (!unlockTracker.charms_heart || !unlockTracker.charms_lung));
        x = x || !(organ.GetComponent<PetrificationCharm>() is null);
        while (x && n < 5)
        {
            organ = RandomOrgan();
            n += 1;
        }
        if (n > 5)
        {
            return false;
        }
        Debug.Log($"{organ.name} petrified");
        textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");

        Actions_Charms.ApplyPetrificationCharmProcess(organ);

        return true;
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
