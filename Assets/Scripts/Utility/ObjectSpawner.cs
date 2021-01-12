using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //organ prefabs
    public GameObject clockworkHeartPrefab;
    public GameObject bombPrefab;
    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public EmbeddedObject SpawnClockworkHeart(string name)
    {
        GameObject embeddedObject = Instantiate(clockworkHeartPrefab);
        embeddedObject.name = name;
        Faff(embeddedObject);

        return embeddedObject.GetComponent<EmbeddedObject>();
    }

    public EmbeddedObject SpawnBullet(string name)
    {
        GameObject embeddedObject = Instantiate(bulletPrefab);
        embeddedObject.name = name;
        Faff(embeddedObject);

        return embeddedObject.GetComponent<EmbeddedObject>();
    }

    public EmbeddedObject SpawnBomb(string name)
    {
        GameObject embeddedObject = Instantiate(bombPrefab);
        embeddedObject.name = name;
        Faff(embeddedObject);

        return embeddedObject.GetComponent<EmbeddedObject>();
    }

    private void Faff(GameObject embeddedObject)
    {
        try
        {
            GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().NewEmbeddedObject(embeddedObject.GetComponent<EmbeddedObject>());
        }
        catch (System.Exception e)
        {
            Debug.LogError("Could not spawn embeddedobject: {e}");
        }
    }

}
