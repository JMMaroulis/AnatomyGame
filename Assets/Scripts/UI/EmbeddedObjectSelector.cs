using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmbeddedObjectSelector : MonoBehaviour, IPointerClickHandler
{
    public EmbeddedObject embeddedObject;
    public Image image;
    private ButtonActions buttonActions;
    private EmbeddedObjectSelectorManager embeddedObjectSelectorManager;

    // Start is called before the first frame update
    void Start()
    {
        embeddedObjectSelectorManager = FindObjectOfType<EmbeddedObjectSelectorManager>();
        buttonActions = FindObjectOfType<ButtonActions>();
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        //should cease to exist if the bodypart doesn't exist
        if (embeddedObject == null)
        {
            embeddedObjectSelectorManager.ResetSelectors();
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        embeddedObjectSelectorManager.ResetSelectors();
        buttonActions.ClearAllButtons();
    }

}
