using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhysicalInjuryGenerator : MonoBehaviour
{
    private TextLog textLog;
    private BodyPartManager bodyPartManager;
    private UnlockTracker unlockTracker;
    private GameSetupScenarioTracker gameSetupTracker;

    public GameObject bullet;
    public GameObject bomb;

    public List<int> randomNumbers;
    private List<int> randomNumbersReturned;

    public int randomIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        randomNumbersReturned = new List<int>();
    }

    public void GenerateInjuries()
    {
        textLog = FindObjectOfType<TextLog>();
        bodyPartManager = FindObjectOfType<BodyPartManager>();
        unlockTracker = FindObjectOfType<UnlockTracker>();
        gameSetupTracker = FindObjectOfType<GameSetupScenarioTracker>();

        EasyInjuries(gameSetupTracker.easyInjuries);
        MediumInjuries(gameSetupTracker.mediumInjuries);
        HardInjuries(gameSetupTracker.hardInjuries);
        randomIndex = 0;
        randomNumbersReturned.Add(77777);

        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
            }
        }
        catch(System.Exception e)
        {
            Debug.LogError($"PhysicalInjuryGenertor: Error resetting selectors: {e}");
        }

    }

    public void GenerateRandomNumbers()
    {
        randomNumbers = new List<int>();
        var x = Random.state;
        for (int i = 0; i < 100000; i++)
        {
            randomNumbers.Add(Mathf.RoundToInt(Random.value * 1000.0f));
        }
        randomIndex = 0;
    }

    private int RandomNumber(int max)
    {
        int x = randomNumbers[randomIndex] % max;
        randomIndex += 1;
        randomNumbersReturned.Add(x);
        return x;
    }

    private Organ RandomOrgan()
    {
        bodyPartManager.OrderLists();

        int index = RandomNumber(bodyPartManager.organs.Count);
        Organ organ = bodyPartManager.organs[index];
        return organ;
    }

    private BodyPart RandomLimb()
    {
        bodyPartManager.OrderLists();

        BodyPart limb = bodyPartManager.bodyParts[RandomNumber(bodyPartManager.bodyParts.Count)];
        while (limb is Organ)
        {
            limb = bodyPartManager.bodyParts[RandomNumber(bodyPartManager.bodyParts.Count)];
        }
        return limb;
    }

    //select n random bodyparts
    private void EasyInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries > 0)
        {

            int i = 0;
            while (i < numberOfInjuries)
            {
                //select and apply random injury
                int injuryNumber = RandomNumber(5);
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

    private void MediumInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries > 0)
        {

            int i = 0;
            while (i < numberOfInjuries)
            {
                //apply injury
                //int injuryNumber = RandomNumber(8);

                int injuryNumber = 1;
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

    private void HardInjuries(int numberOfInjuries)
    {
        if (numberOfInjuries > 0)
        {

            int i = 0;
            while (i < numberOfInjuries)
            {
                //selected bodypart to injure, and injury to apply
                int injuryNumber = RandomNumber(4);
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
    private bool StabOrgan()
    {
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

    private bool StabLimb()
    {
        BodyPart limb = RandomLimb();

        Debug.Log($"Stabbed {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been stabbed.");

        limb.damage = Mathf.Clamp(limb.damage + 30, 0, limb.damageMax);
        limb.bloodLossRate += 10;

        return true;
    }

    private bool ShootLimb()
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

    private bool ShootOrgan()
    {

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

            EmbeddedObject newBullet = FindObjectOfType<ObjectSpawner>().SpawnBullet("bullet");
            newBullet.Embed(organ);

        }

        return true;

    }

    private bool ImplantBomb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Bomb implanted in {limb.name}");
        textLog.NewLogEntry($"There's an explosive device in the {limb.name}");

        EmbeddedObject newBomb = FindObjectOfType<ObjectSpawner>().SpawnBomb("bomb");
        newBomb.Embed(limb);

        return true;
    }

    private bool CrushLimb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Crushed {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been crushed.");

        limb.damage = Mathf.Min(limb.damage + 30, limb.damageMax);
        limb.bloodLossRate += 10;

        return true;
    }

    private bool SnakeBite()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Snake bit {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been bitten by a venomous snake.");

        limb.damage = Mathf.Min(limb.damage + 10, limb.damageMax);
        limb.bloodLossRate += 3;
        limb.slowPoison += 50;

        return true;
    }

    private bool SeverTorso()
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

    private bool SeverHead()
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

    private bool SeverLimbMedium()
    {
        BodyPart limb = RandomLimb();

        if (!(limb is Head) && !(limb.connectedBodyParts.Count() == 0))
        {
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

        return false;
    }

    private bool SlowPoisonOrgan()
    {

        Organ organ = RandomOrgan();
        Debug.Log($"Poisoned {organ.name}");
        textLog.NewLogEntry($"The {organ.name} has been poisoned.");

        organ.slowPoison += 100;

        return true;
    }

    private bool SlowPoisonLimb()
    {
        BodyPart limb = RandomLimb();
        Debug.Log($"Poisoned {limb.name}");
        textLog.NewLogEntry($"The {limb.name} has been poisoned.");

        limb.slowPoison += 100;

        return true;
    }

    private bool MissingOrganMedium()
    {
        Organ organ = RandomOrgan();

        if (!(organ is Brain) & !(organ.gameObject.GetComponent<PetrificationCharm>() is null))
        {
            Debug.Log($"{organ.name} missing");
            textLog.NewLogEntry($"The {organ.name} is missing?!");

            Actions_Surgery.RemoveOrganProcess(organ);
            Actions_Surgery.DeleteBodyPartProcess(organ);

            return true;
        }

        return false;

    }

    private bool MissingOrganHard()
    {
        Organ organ = RandomOrgan();
        if (organ.connectedBodyParts.Count() > 0 && organ.gameObject.GetComponent<PetrificationCharm>() is null)
        {
            Debug.Log($"{organ.name} missing");
            textLog.NewLogEntry($"The {organ.name} is missing?!");

            Actions_Surgery.RemoveOrganProcess(organ);
            Actions_Surgery.DeleteBodyPartProcess(organ);

            return true;
        }

        return false;
    }

    private bool RemoveOrganMedium()
    {
        Organ organ = RandomOrgan();
        // int n = 0;
        // bool x = false;
        // //x = x || (organ is Heart && !unlockTracker.charms_heart);
        // //x = x || (organ is Lung && !unlockTracker.charms_lung);
        // //x = x || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart);
        // while (x && n < 5)
        // {
        //     organ = RandomOrgan();
        //     x = false;
        //     //x = x || (organ is Heart && !unlockTracker.charms_heart);
        //     //x = x || (organ is Lung && !unlockTracker.charms_lung);
        //     //x = x || (organ is Brain && !unlockTracker.charms_lung && !unlockTracker.charms_heart);
        //     n += 1;
        // }
        // if (n > 5)
        // {
        //     return false;
        // }

        if (organ.connectedBodyParts.Count() > 0)
        {
            Debug.Log($"{organ.name} external");
            textLog.NewLogEntry($"The {organ.name} requires re-implanting.");

            Actions_Surgery.RemoveOrganProcess(organ);
            return true;
        }

        return false;

    }

    private bool RemoveOrganHard()
    {
        Organ organ = RandomOrgan();
        if (organ.connectedBodyParts.Count() > 0)
        {
            Debug.Log($"{organ.name} external");
            textLog.NewLogEntry($"The {organ.name} requires re-implanting.");

            Actions_Surgery.RemoveOrganProcess(organ);
            return true;
        }

        return false;
    }

    private bool PetrifyOrganMedium()
    {
        Organ organ = RandomOrgan();
        if (organ.GetComponent<PetrificationCharm>() is null)
        {
            Debug.Log($"{organ.name} petrified");
            textLog.NewLogEntry($"The {organ.name} has been temporarily petrified.");

            Actions_Charms.ApplyPetrificationCharmProcess(organ);

            return true;
        }

        return false;

    }

}
