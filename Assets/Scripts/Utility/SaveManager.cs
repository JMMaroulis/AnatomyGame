using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    private class Encoders{

        public string goldTracker;
        public string unlockTracker;
        public string perkTracker;
        public string actionTracker;
        public string randomTracker;
        public string gameSetupScenarioTracker;
    }

    public void EncodeTrackers()
    {
        GoldTracker goldTracker = FindObjectOfType<GoldTracker>();
        UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
        ActionTracker actionTracker = FindObjectOfType<ActionTracker>();
        RandomTracker randomTracker = FindObjectOfType<RandomTracker>();
        GameSetupScenarioTracker gameSetupScenarioTracker = FindObjectOfType<GameSetupScenarioTracker>();
        PerkTracker perkTracker = FindObjectOfType<PerkTracker>();

        Encoders encoders = new Encoders();
        encoders.goldTracker = JsonUtility.ToJson(goldTracker);
        encoders.unlockTracker = JsonUtility.ToJson(unlockTracker);
        encoders.actionTracker = JsonUtility.ToJson(actionTracker);
        encoders.randomTracker = JsonUtility.ToJson(randomTracker);
        encoders.perkTracker = JsonUtility.ToJson(perkTracker);
        encoders.gameSetupScenarioTracker = JsonUtility.ToJson(gameSetupScenarioTracker);

        string json = JsonUtility.ToJson(encoders);
        System.IO.File.WriteAllText(@"savegame.json", json);
    }

    public void DecodeTrackers()
    {
        using (StreamReader r = new StreamReader(@"savegame.json"))
        {
            string json = r.ReadToEnd();
            Encoders encoders = JsonUtility.FromJson<Encoders>(json);

            GoldTracker goldTracker = FindObjectOfType<GoldTracker>();
            JsonUtility.FromJsonOverwrite(encoders.goldTracker, goldTracker);

            GameSetupScenarioTracker gameSetupScenarioTracker = FindObjectOfType<GameSetupScenarioTracker>();
            JsonUtility.FromJsonOverwrite(encoders.gameSetupScenarioTracker, gameSetupScenarioTracker);
            gameSetupScenarioTracker.OnLoad();

            UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
            JsonUtility.FromJsonOverwrite(encoders.unlockTracker, unlockTracker);

            PerkTracker perkTracker = FindObjectOfType<PerkTracker>();
            JsonUtility.FromJsonOverwrite(encoders.perkTracker, perkTracker);

            ActionTracker actionTracker = FindObjectOfType<ActionTracker>();
            JsonUtility.FromJsonOverwrite(encoders.actionTracker, actionTracker);

            RandomTracker randomTracker = FindObjectOfType<RandomTracker>();
            JsonUtility.FromJsonOverwrite(encoders.randomTracker, randomTracker);
            randomTracker.OnLoad();

        }

    }
}
