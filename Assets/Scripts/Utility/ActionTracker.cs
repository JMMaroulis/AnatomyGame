﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ActionTracker : MonoBehaviour
{
    public int blood_injected;
    public int blood_extracted;
    public int blood_bandages;
    public int blood_lettings;
    public int surgery_amputations;
    public int surgery_attachments;
    public int surgery_organremovals;
    public int surgery_organtransplant;
    public int surgery_destroyed;
    public int spawn_spawned;
    public int medicine_health;
    public int medicine_antidote;
    public int medicine_slowpoison;
    public int medicine_stasis;
    public int medicine_haste;
    public int medicine_coagulant;
    public int charm_heart;
    public int charm_lung;
    public int charm_petrification;
    public int charm_bloodregen;

    public int levelstart_blood_injected;
    public int levelstart_blood_extracted;
    public int levelstart_blood_bandages;
    public int levelstart_blood_lettings;
    public int levelstart_surgery_amputations;
    public int levelstart_surgery_attachments;
    public int levelstart_surgery_organremovals;
    public int levelstart_surgery_organtransplant;
    public int levelstart_surgery_destroyed;
    public int levelstart_spawn_spawned;
    public int levelstart_medicine_health;
    public int levelstart_medicine_antidote;
    public int levelstart_medicine_slowpoison;
    public int levelstart_medicine_stasis;
    public int levelstart_medicine_haste;
    public int levelstart_medicine_coagulant;
    public int levelstart_charm_heart;
    public int levelstart_charm_lung;
    public int levelstart_charm_petrification;
    public int levelstart_charm_bloodregen;

    public string ActionSummary()
    {
        string output = "";

        output += "Blood injected: ${blood_injected} units\n";
        output += "Blood extracted: ${blood_extracted}\n";
        output += "Bandages applied: ${blood_bandages}\n";
        output += "Blood lettings: ${ blood_lettings}\n";
        output += "Limb amputations: ${ surgery_amputations}\n";
        output += "Limb attatchments:${ surgery_attachments}\n";
        output += "Organs removed: ${ surgery_organremovals}\n";
        output += "Organs transplanted: ${ surgery_organtransplant}\n";
        output += "Body parts destroyed: ${ surgery_destroyed}\n";
        output += "Body parts ordered: ${ spawn_spawned}\n";
        output += "Health potion injected: ${ medicine_health}\n";
        output += "Antidote injected: ${ medicine_antidote}\n";
        output += "Slow poison injected: ${ medicine_slowpoison}\n";
        output += "Stasis potion injected: ${ medicine_stasis}\n";
        output += "Haste potion injected: ${ medicine_haste}\n";
        output += "Coagulant potion injected: ${ medicine_coagulant}\n";
        output += "Heart charms applied: ${ charm_heart}\n";
        output += "Lung charms applied: ${ charm_lung}\n";
        output += "Petrification charms applied: ${ charm_petrification}\n";
        output += "Blood regen charms applied: ${ charm_bloodregen}\n";

        return output;
    }

    public void LevelStart()
    {
        levelstart_blood_injected =  blood_injected;
        levelstart_blood_extracted = blood_extracted;
        levelstart_blood_bandages = blood_bandages;
        levelstart_blood_lettings = blood_lettings;
        levelstart_surgery_amputations = surgery_amputations;
        levelstart_surgery_attachments = surgery_attachments;
        levelstart_surgery_organremovals = surgery_organremovals;
        levelstart_surgery_organtransplant = surgery_organtransplant;
        levelstart_surgery_destroyed = surgery_destroyed;
        levelstart_spawn_spawned = spawn_spawned;
        levelstart_medicine_health = medicine_health;
        levelstart_medicine_antidote = medicine_antidote;
        levelstart_medicine_slowpoison = medicine_slowpoison;
        levelstart_medicine_stasis = medicine_stasis;
        levelstart_medicine_haste = medicine_haste;
        levelstart_medicine_coagulant = medicine_coagulant;
        levelstart_charm_heart = charm_heart;
        levelstart_charm_lung = charm_lung;
        levelstart_charm_petrification = charm_petrification;
        levelstart_charm_bloodregen = charm_bloodregen;
    }

    public void OnLoad()
    {
        blood_injected = levelstart_blood_injected;
        blood_extracted = levelstart_blood_extracted;
        blood_bandages = levelstart_blood_bandages;
        blood_lettings = levelstart_blood_lettings;
        surgery_amputations = levelstart_surgery_amputations;
        surgery_attachments = levelstart_surgery_attachments;
        surgery_organremovals = levelstart_surgery_organremovals;
        surgery_organtransplant = levelstart_surgery_organtransplant;
        surgery_destroyed = levelstart_surgery_destroyed;
        spawn_spawned = levelstart_spawn_spawned;
        medicine_health = levelstart_medicine_health;
        medicine_antidote = levelstart_medicine_antidote;
        medicine_slowpoison = levelstart_medicine_slowpoison;
        medicine_stasis = levelstart_medicine_stasis;
        medicine_haste = levelstart_medicine_haste;
        medicine_coagulant = levelstart_medicine_coagulant;
        charm_heart = levelstart_charm_heart;
        charm_lung = levelstart_charm_lung;
        charm_petrification = levelstart_charm_petrification;
        charm_bloodregen = levelstart_charm_bloodregen;
    }
}