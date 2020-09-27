using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmbeddedObjectSelectorManager : MonoBehaviour
{
    public GameObject embeddedObjectSelectorsPanel;

    private List<EmbeddedObjectSelector> embeddedObjectSelectors = new List<EmbeddedObjectSelector>();

    public GameObject bulletSelectorPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NewEmbeddedObject(EmbeddedObject embeddedObject)
    {

        if (embeddedObject is Bullet)
        {
            NewSelector(embeddedObject, bulletSelectorPrefab);
        }

    }

    private void NewSelector(EmbeddedObject embeddedObject, GameObject selectorPrefab)
    {
        GameObject selector = GameObject.Instantiate(selectorPrefab, embeddedObjectSelectorsPanel.transform);
        embeddedObjectSelectors.Add(selector.GetComponent<EmbeddedObjectSelector>());
        selector.GetComponent<EmbeddedObjectSelector>().embeddedObject = embeddedObject;
    }

    public void ResetSelectors()
    {
        ClearAllSelectors();
        BodyPart selectedBodyPart = FindObjectOfType<ButtonActions>().selectedBodyPart;

        //make new bodypart selectors
        foreach (EmbeddedObject embeddedObject in selectedBodyPart.embeddedObjects)
        {
             NewEmbeddedObject(embeddedObject);
        }
    }

    private void ClearAllSelectors()
    {
        embeddedObjectSelectors = ClearSelectorsFromList(embeddedObjectSelectors);
    }

    private List<EmbeddedObjectSelector> ClearSelectorsFromList(List<EmbeddedObjectSelector> selectors)
    {
        for (int i = 0; i < selectors.Count; i++)
        {
            Destroy(selectors[i].gameObject);
        }
        return new List<EmbeddedObjectSelector>();
    }

}
