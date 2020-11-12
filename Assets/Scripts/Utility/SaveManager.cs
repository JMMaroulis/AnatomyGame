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
        public string injurySpawnTracker;
        public string unlockTracker;
        public string actionTracker;
        public string randomTracker;
    }

    public void EncodeTrackers()
    {
        string encodedTrackers = "";

        GoldTracker goldTracker = FindObjectOfType<GoldTracker>();
        encodedTrackers += "\"goldTracker\": " + JsonUtility.ToJson(goldTracker) + ",";

        GameSetupScenarioTracker injurySpawnTracker = FindObjectOfType<GameSetupScenarioTracker>();
        encodedTrackers += "\"injurySpawnTracker\": " + JsonUtility.ToJson(injurySpawnTracker) + ",";

        UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
        encodedTrackers += "\"unlockTracker\": " + JsonUtility.ToJson(unlockTracker) + ",";

        ActionTracker actionTracker = FindObjectOfType<ActionTracker>();
        encodedTrackers += "\"actionTracker\": " + JsonUtility.ToJson(actionTracker);

        RandomTracker randomTracker = FindObjectOfType<RandomTracker>();
        encodedTrackers += "\"randomTracker\": " + JsonUtility.ToJson(randomTracker);


        Encoders encoders = new Encoders();
        encoders.goldTracker = JsonUtility.ToJson(goldTracker);
        encoders.injurySpawnTracker = JsonUtility.ToJson(injurySpawnTracker);
        encoders.unlockTracker = JsonUtility.ToJson(unlockTracker);
        encoders.actionTracker = JsonUtility.ToJson(actionTracker);
        encoders.randomTracker = JsonUtility.ToJson(randomTracker);

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
            goldTracker.OnLoad();

            GameSetupScenarioTracker injurySpawnTracker = FindObjectOfType<GameSetupScenarioTracker>();
            JsonUtility.FromJsonOverwrite(encoders.injurySpawnTracker, injurySpawnTracker);
            injurySpawnTracker.OnLoad();

            UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
            JsonUtility.FromJsonOverwrite(encoders.unlockTracker, unlockTracker);
            unlockTracker.OnLoad();

            ActionTracker actionTracker = FindObjectOfType<ActionTracker>();
            JsonUtility.FromJsonOverwrite(encoders.actionTracker, actionTracker);
            actionTracker.OnLoad();

            RandomTracker randomTracker = FindObjectOfType<RandomTracker>();
            JsonUtility.FromJsonOverwrite(encoders.randomTracker, randomTracker);
            randomTracker.OnLoad();

        }

    }
}
