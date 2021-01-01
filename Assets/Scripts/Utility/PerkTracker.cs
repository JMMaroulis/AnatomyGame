using UnityEngine;

public class PerkTracker : MonoBehaviour
{
    public bool perk1;

    public bool levelstart_perk1;

    public void Reset()
    {
        perk1 = false;
    }

    public void LevelStart()
    {
        levelstart_perk1 = perk1;
    }

    public void OnLoad()
    {
        perk1 = levelstart_perk1;
    }
}
