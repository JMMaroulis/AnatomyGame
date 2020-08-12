
using UnityEngine;
using UnityEngine.UI;

public class ActionTimeBar : MonoBehaviour
{
    private Slider slider;
    private Clock clock;

    // Start is called before the first frame update
    void Start()
    {
        clock = FindObjectOfType<Clock>();
        slider = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clock.isTimePassing)
        {
            slider.value += Time.deltaTime * clock.globalTimeScalingFactor;
        }
    }

    public void Reset(float actionSeconds)
    {
        slider.value = 0;
        slider.minValue = 0;
        slider.maxValue = actionSeconds;
    }

}
