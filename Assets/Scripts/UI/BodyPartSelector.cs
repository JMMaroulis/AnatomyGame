using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BodyPartSelector : MonoBehaviour, IPointerClickHandler
{
    public BodyPart bodyPart;
    public Image connectedImage;
    public Image disconnectedImage;
    private ButtonActions buttonActions;

    // Start is called before the first frame update
    void Start()
    {
        buttonActions = FindObjectOfType<ButtonActions>();
    }

    // Update is called once per frame
    void Update()
    {
        //should cease to exist if the bodypart doesn't exist
        if (bodyPart == null)
        {
            FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        }

        connectedImage.enabled = bodyPart.connectedBodyParts.Count > 0;
        disconnectedImage.enabled = bodyPart.connectedBodyParts.Count == 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        buttonActions.selectedBodyPart = bodyPart;
        if (!(bodyPart is Organ))
        {
            FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        }
        FindObjectOfType<ButtonActions>().ClearAllButtons();
    }

}
