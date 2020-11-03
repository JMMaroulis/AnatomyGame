using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Medicine
{

    public static void InjectPotion(BodyPart bodyPart, string type, float amount, float seconds, int goldCost)
    {
        StaticCoroutine.Start(InjectPotionCoroutine(bodyPart, type, amount, seconds, goldCost));
    }

    public static IEnumerator InjectPotionCoroutine(BodyPart bodyPart, string type, float amount, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            InjectPotionProcess(bodyPart.GetComponent<BodyPart>(), type, amount);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectMedicineAction();
        }
        else
        {
            clock.actionCancelFlag = false;
        }
    }

    public static void InjectPotionProcess(BodyPart bodyPart, string type, float amount)
    {

        switch (type)
        {
            case "health":
                bodyPart.healthPotion += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_health += amount;
                break;

            case "antidote":
                bodyPart.antidote += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_antidote += amount;
                break;

            case "slow poison":
                bodyPart.slowPoison += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_slowpoison += amount;
                break;

            case "stasis":
                bodyPart.stasisPotion += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_stasis += amount;
                break;

            case "haste":
                bodyPart.hastePotion += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_haste += amount;
                break;

            case "coagulant":
                bodyPart.coagulantPotion += amount;
                MonoBehaviour.FindObjectOfType<ActionTracker>().medicine_coagulant += amount;
                break;

            default:
                Debug.LogError($"Potion type '{type}' isn't listed in Actions_Medicine.cs");
                break;
        }

    }

}
