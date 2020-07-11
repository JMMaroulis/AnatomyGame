using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GoldReporting : MonoBehaviour
{
    private Text goldText;
    private GoldTracker goldTracker;

    // Start is called before the first frame update
    void Start()
    {
        goldTracker = FindObjectOfType<GoldTracker>();
        goldText = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = $"Current gold: {goldTracker.goldAccumulated - goldTracker.goldSpent}";
    }

}
