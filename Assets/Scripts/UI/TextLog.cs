using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour
{

    public Text text;
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void NewLogEntry(string entry)
    {
        text.text += entry + "\n";
        StartCoroutine(ForceScrollDown());
    }

    IEnumerator ForceScrollDown()
    {
        // Wait for end of frame AND force update all canvases before setting to bottom.
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

}
