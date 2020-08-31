using UnityEngine;
using UnityEngine.UI;

public class TextLog : MonoBehaviour
{

    public Text text;
    public Scrollbar scrollbar;


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
        scrollbar.value = 0;
    }
}
