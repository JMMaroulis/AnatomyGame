using UnityEngine;

public class UnlockTracker : MonoBehaviour
{
    public bool surgery;
    public bool medicine_poison;
    public bool medicine_speed;
    public bool medicine_blood;
    public bool charms_heart;
    public bool charms_lung;
    public bool charms_petrification;
    public bool charms_blood_regen;
    public bool blood;
    public bool spawn;
    public bool spawn_clock;

    public void Reset()
    {
        surgery = false;
        medicine_poison = false;
        medicine_speed = false;
        medicine_blood = false;
        charms_heart = false;
        charms_lung = false;
        charms_petrification = false;
        charms_blood_regen = false;
        blood = false;
        spawn = false;
        spawn_clock = false;
    }

}
