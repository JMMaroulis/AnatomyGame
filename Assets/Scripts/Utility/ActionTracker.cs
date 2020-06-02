using System.Collections;
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

    public void Reset()
    {
        blood_injected = 0;
        blood_extracted = 0;
        blood_bandages = 0;
        blood_lettings = 0;
        surgery_amputations = 0;
        surgery_attachments = 0;
        surgery_organremovals = 0;
        surgery_organtransplant = 0;
        surgery_destroyed = 0;
        spawn_spawned = 0;
        medicine_health = 0;
        medicine_antidote = 0;
        medicine_slowpoison = 0;
        medicine_stasis = 0;
        medicine_haste = 0;
        medicine_coagulant = 0;
        charm_heart = 0;
        charm_lung = 0;
        charm_petrification = 0;
        charm_bloodregen = 0;
    }
}
