using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public bool levelstart_surgery;
    public bool levelstart_medicine_poison;
    public bool levelstart_medicine_speed;
    public bool levelstart_medicine_blood;
    public bool levelstart_charms_heart;
    public bool levelstart_charms_lung;
    public bool levelstart_charms_petrification;
    public bool levelstart_charms_blood_regen;
    public bool levelstart_blood;
    public bool levelstart_spawn;

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
    }

    public void LevelStart()
    {
        levelstart_surgery                =   surgery;
        levelstart_medicine_poison        =   medicine_poison;
        levelstart_medicine_speed         =   medicine_speed;
        levelstart_medicine_blood         =   medicine_blood;
        levelstart_charms_heart           =   charms_heart;
        levelstart_charms_lung            =   charms_lung;
        levelstart_charms_petrification   =   charms_petrification;
        levelstart_charms_blood_regen     =   charms_blood_regen;
        levelstart_blood                  =   blood;
        levelstart_spawn                  =   spawn;
    }

    public void OnLoad()
    {
        surgery = levelstart_surgery;
        medicine_poison = levelstart_medicine_poison;
        medicine_speed = levelstart_medicine_speed;
        medicine_blood = levelstart_medicine_blood;
        charms_heart = levelstart_charms_heart;
        charms_lung = levelstart_charms_lung;
        charms_petrification = levelstart_charms_petrification;
        charms_blood_regen = levelstart_charms_blood_regen;
        blood = levelstart_blood;
        spawn = levelstart_spawn;
    }
}
