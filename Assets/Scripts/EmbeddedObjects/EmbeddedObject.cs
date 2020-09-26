using UnityEngine;

public class EmbeddedObject : MonoBehaviour
{
    public BodyPart parentBodyPart;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Embed(BodyPart bodypart)
    {
        parentBodyPart = bodypart;
        bodypart.embeddedObjects.Add(this);
        this.gameObject.transform.SetParent(bodypart.transform);
    }

    public void Remove()
    {
        parentBodyPart.embeddedObjects.Remove(this);
        parentBodyPart = null;
    }

}
