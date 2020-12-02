using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmbeddedObjectSelector : MonoBehaviour, IPointerClickHandler
{
    public EmbeddedObject embeddedObject;
    public Image image;
    public Image borderHighlight;
    private ButtonActions buttonActions;
    private EmbeddedObjectSelectorManager embeddedObjectSelectorManager;

    // Start is called before the first frame update
    void Start()
    {
        embeddedObjectSelectorManager = FindObjectOfType<EmbeddedObjectSelectorManager>();
        buttonActions = FindObjectOfType<ButtonActions>();
        Update();
        borderHighlight.enabled = ShouldBeHighlighted(embeddedObject);
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
        buttonActions.selectedGameObject = embeddedObject.gameObject;
        embeddedObjectSelectorManager.ResetSelectors();
        FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();

        buttonActions.ClearAllButtons();
        buttonActions.UpdateMenuButtonsInteractivity(false);
        borderHighlight.enabled = ShouldBeHighlighted(embeddedObject);
    }

    private bool ShouldBeHighlighted(EmbeddedObject embeddedObject)
    {
        if (buttonActions.selectedGameObject == null)
        {
            return false;
        }

        //if embedded object is selected
        if (embeddedObject.gameObject == buttonActions.selectedGameObject)
        {
            return true;
        }

        return false;
    }

}
