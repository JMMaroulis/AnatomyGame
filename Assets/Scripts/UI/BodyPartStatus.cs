using UnityEngine;
using UnityEngine.UI;

public class BodyPartStatus : MonoBehaviour
{

    public BloodBar bloodBar;
    public OxygenBar oxygenBar;
    public HealthBar healthBar;
    public Text partName;
    public BodyPart bodyPart;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //should cease to exist if the bodypart doesn't exist
        if (bodyPart is null)
        {
            Destroy(this);
        }
    }

}
