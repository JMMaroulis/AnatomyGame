using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkUnlockManager : MonoBehaviour
{
    public List<Button> buttons;
    public Text goldText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //assigned to button
    public void LevelStart()
    {
        FindObjectOfType<SceneTransitionManager>().SampleScene();
    }

}
