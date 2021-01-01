using UnityEngine;

//TODO: Actually maintain this; it's not actually used by anything at the moment, so it's fine. If it is, though, chances are it's wildy out of date.

public class ActionTracker : MonoBehaviour
{
    public float blood_injected;
    public float blood_extracted;
    public int blood_bandages;
    public int blood_lettings;
    public int surgery_amputations;
    public int surgery_attachments;
    public int surgery_organremovals;
    public int surgery_organtransplant;
    public int surgery_destroyed;
    public int surgery_implants;
    public int surgery_remove_implants;
    public int spawn_spawned;
    public float medicine_health;
    public float medicine_antidote;
    public float medicine_slowpoison;
    public float medicine_stasis;
    public float medicine_haste;
    public float medicine_coagulant;
    public int charm_heart;
    public int charm_lung;
    public int charm_petrification;
    public int charm_bloodregen;

    public float levelstart_blood_injected;
    public float levelstart_blood_extracted;
    public int levelstart_blood_bandages;
    public int levelstart_blood_lettings;
    public int levelstart_surgery_amputations;
    public int levelstart_surgery_attachments;
    public int levelstart_surgery_organremovals;
    public int levelstart_surgery_organtransplant;
    public int levelstart_surgery_destroyed;
    public int levelstart_surgery_implants;
    public int levelstart_surgery_remove_implants;
    public int levelstart_spawn_spawned;
    public float levelstart_medicine_health;
    public float levelstart_medicine_antidote;
    public float levelstart_medicine_slowpoison;
    public float levelstart_medicine_stasis;
    public float levelstart_medicine_haste;
    public float levelstart_medicine_coagulant;
    public int levelstart_charm_heart;
    public int levelstart_charm_lung;
    public int levelstart_charm_petrification;
    public int levelstart_charm_bloodregen;

    public string ActionSummary()
    {
        string output = "";

        output += $"Blood injected: {blood_injected} units\n";
        output += $"Blood extracted: {blood_extracted}\n";
        output += $"Bandages applied: {blood_bandages}\n";
        output += $"Blood lettings: { blood_lettings}\n";
        output += $"Limb amputations: { surgery_amputations}\n";
        output += $"Limb attatchments:{ surgery_attachments}\n";
        output += $"Organs removed: { surgery_organremovals}\n";
        output += $"Organs transplanted: { surgery_organtransplant}\n";
        output += $"Body parts destroyed: { surgery_destroyed}\n";
        output += $"Body parts ordered: { spawn_spawned}\n";
        output += $"Health potion injected: { medicine_health}\n";
        output += $"Antidote injected: { medicine_antidote}\n";
        output += $"Slow poison injected: { medicine_slowpoison}\n";
        output += $"Stasis potion injected: { medicine_stasis}\n";
        output += $"Haste potion injected: { medicine_haste}\n";
        output += $"Coagulant potion injected: { medicine_coagulant}\n";
        output += $"Heart charms applied: { charm_heart}\n";
        output += $"Lung charms applied: { charm_lung}\n";
        output += $"Petrification charms applied: { charm_petrification}\n";
        output += $"Blood regen charms applied: { charm_bloodregen}\n";
        output += $"Objects implanted: ${ surgery_implants}\n";
        output += $"Objects removed: ${ surgery_remove_implants}\n";

        return output;
    }

}
