using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InjurySpawnTracker : MonoBehaviour
{
    public int startingBodyPartInjuries;
    public int startingOrganInjuries;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void IncreaseInjuryNumber()
    {
        if (startingBodyPartInjuries/2 > startingOrganInjuries)
        {
            startingOrganInjuries += 1;
        }
        else
        {
            startingBodyPartInjuries += 1;
        }
    }

}
