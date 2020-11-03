using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EmbeddedObjectSelectorManager : MonoBehaviour
{
    public GameObject embeddedObjectSelectorsPanel;
    public GameObject externalEmbeddedObjectSelectorsPanel;

    private List<EmbeddedObjectSelector> embeddedObjectSelectors = new List<EmbeddedObjectSelector>();

    public GameObject bulletSelectorPrefab;
    public GameObject bombSelectorPrefab;

    private ButtonActions buttonActions;

    // Start is called before the first frame update
    void Start()
    {
        buttonActions = FindObjectOfType<ButtonActions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void NewEmbeddedObject(EmbeddedObject embeddedObject)
    {

        if (embeddedObject is Bullet)
        {
            NewSelector(embeddedObject, bulletSelectorPrefab, externalEmbeddedObjectSelectorsPanel);
        }

        if (embeddedObject is Bomb)
        {
            NewSelector(embeddedObject, bombSelectorPrefab, externalEmbeddedObjectSelectorsPanel);
        }

    }

    private void NewSelector(EmbeddedObject embeddedObject, GameObject selectorPrefab, GameObject notPartOfMainPanel)
    {
        if (!(embeddedObject.parentBodyPart is null))
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, embeddedObjectSelectorsPanel.transform);
            embeddedObjectSelectors.Add(selector.GetComponent<EmbeddedObjectSelector>());
            selector.GetComponent<EmbeddedObjectSelector>().embeddedObject = embeddedObject;
        }
        else
        {
            GameObject selector = GameObject.Instantiate(selectorPrefab, notPartOfMainPanel.transform);
            embeddedObjectSelectors.Add(selector.GetComponent<EmbeddedObjectSelector>());
            selector.GetComponent<EmbeddedObjectSelector>().embeddedObject = embeddedObject;
        }
    }



    public void ResetSelectors()
    {
        ClearAllSelectors();
        GameObject selectedGameObject = FindObjectOfType<ButtonActions>().selectedGameObject;

        //make selector for each embeddedobject in currently selected bodypart
        if (!(selectedGameObject == null))
        {
            if (!(selectedGameObject.GetComponent<BodyPart>() is null))
            {
                foreach (EmbeddedObject embeddedObject in selectedGameObject.GetComponent<BodyPart>().embeddedObjects)
                {
                    NewEmbeddedObject(embeddedObject);
                }
            }
        }

      
        //make selector for all embeddedobjects not currently embedded in something
        foreach (EmbeddedObject embeddedObject in FindObjectsOfType<EmbeddedObject>())
        {
            if (embeddedObject.parentBodyPart is null)
            {
                NewEmbeddedObject(embeddedObject);
            }
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
