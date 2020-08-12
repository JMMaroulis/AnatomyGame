using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    private Slider slider;
    public BodyPart bodyPart;
    private int frameCount;

    // Start is called before the first frame update
    void Start()
    {
        frameCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (frameCount == 3)
        {
            frameCount = 0;
            slider.value = bodyPart.oxygen;
        }
        else
        {
            frameCount += 1;
        }
    }

    public void MinMax()
    {
        slider = this.gameObject.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = bodyPart.oxygenMax;
    }
}
