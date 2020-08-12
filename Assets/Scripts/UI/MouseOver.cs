using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float timePassed;
    private bool timePassing;

    private float mouseoverPanelSizeX;
    private float mouseoverPanelSizeY;

    public bool mouseoverEnabled;
    public GameObject mouseoverPanel;


    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        mouseoverPanelSizeX = mouseoverPanel.GetComponent<Image>().rectTransform.rect.size.x;
        mouseoverPanelSizeY = mouseoverPanel.GetComponent<Image>().rectTransform.rect.size.y;

        if (timePassing == true)
        {
            timePassed += Time.deltaTime;
        }

        if (timePassed >= 1.0f && mouseoverEnabled)
        {
            // TODO: figure out what the hell is going on here, so I can replace the manual inputs with automatic geometry
            Vector3 newPosition = new Vector3(Input.mousePosition.x + 215, Input.mousePosition.y + 40, mouseoverPanel.transform.position.z);
            mouseoverPanel.transform.position = newPosition;
            mouseoverPanel.SetActive(true);
        }
        else
        {
            mouseoverPanel.SetActive(false);
        }
    }

    public void ResetTimer()
    {
        timePassed = 0.0f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        timePassing = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        ResetTimer();
        timePassing = false;
    }
}

