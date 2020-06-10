using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        /*
        List<Head> bodyParts = FindObjectsOfType<Head>().ToList<Head>();
        List<string> encoded = new List<string>();

        foreach (Head bodyPart in bodyParts)
        {
            encoded.Add(JsonUtility.ToJson(bodyParts[0]));
            Destroy(bodyPart.gameObject);
        }

        foreach (string bodyPart in encoded)
        {
            GameObject bob = new GameObject();
            bob.AddComponent<Head>();
            JsonUtility.FromJsonOverwrite(bodyPart, bob);
        }

        */

        /*
        Head head = FindObjectOfType<Head>();
        Debug.Log(head.GetInstanceID());
        string encoded = JsonUtility.ToJson(head);
        Debug.Log(encoded);
        Destroy(head);
        GameObject bob = new GameObject();
        bob.AddComponent<Head>();
        JsonUtility.FromJsonOverwrite(encoded, bob);
        Instantiate(bob);
        Debug.Log(bob.GetInstanceID());
        */

        System.IO.File.WriteAllText(@"savegame.json", "{" + EncodeTrackers() + "}");

    }

    // Update is called once per frame
    void Update()
    {

    }

    private string EncodeTrackers()
    {
        string encodedTrackers = "";

        GoldTracker goldTracker = FindObjectOfType<GoldTracker>();
        encodedTrackers += "\"goldTracker\": " + JsonUtility.ToJson(goldTracker) + ",";

        InjurySpawnTracker injurySpawnTracker = FindObjectOfType<InjurySpawnTracker>();
        encodedTrackers += "\"injurySpawnTracker\": " + JsonUtility.ToJson(injurySpawnTracker) + ",";

        UnlockTracker unlockTracker = FindObjectOfType<UnlockTracker>();
        encodedTrackers += "\"unlockTracker\": " + JsonUtility.ToJson(unlockTracker) + ",";

        ActionTracker actionTracker = FindObjectOfType<ActionTracker>();
        encodedTrackers += "\"actionTracker\": " + JsonUtility.ToJson(actionTracker);


        //JsonUtility.FromJson(encoded);

        return encodedTrackers;
        //GameObject bob = new GameObject();
        //bob.AddComponent<GoldTracker>();
        //JsonUtility.FromJsonOverwrite(encoded, bob.GetComponent<GoldTracker>());
    }


}
