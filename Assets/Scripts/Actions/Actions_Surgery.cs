using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Actions_Surgery
{

    public static void RemoveBodyPart(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(RemoveBodyPartCoroutine(bodyPartObject, seconds, goldCost));
    }

    public static IEnumerator RemoveBodyPartCoroutine(BodyPart bodyPartObject, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            bodyPartObject.GetComponent<BodyPart>().SeverAllConnections();
            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_amputations += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            UpdateAllBodyPartHeartConnections();
        }

    }



    public static void RemoveOrgan(Organ organObject, float seconds, int goldCost)
    {

        StaticCoroutine.Start(RemoveOrganCoroutine(organObject, seconds, goldCost));
    }

    public static IEnumerator RemoveOrganCoroutine(Organ organObject, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            //disconnect
            organObject.GetComponent<Organ>().SeverAllConnections();
            UpdateAllBodyPartHeartConnections();

            //remove from being child of bodypart
            organObject.transform.SetParent(organObject.transform.parent.parent);

            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organremovals += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;

        }

    }

    public static void ImplantOrgan(Organ organ, BodyPart bodyPart, float seconds, int goldCost)
    {

        if (organ.connectedBodyParts.Count != 0)
        {
            Debug.Log("That organ is already inside something! Don't do that!");
            return;
        }

        StaticCoroutine.Start(ImplantOrganCoroutine(organ, bodyPart, seconds, goldCost));
    }

    public static IEnumerator ImplantOrganCoroutine(Organ organ, BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            //connect
            organ.CreateConnection(bodyPart);
            bodyPart.AddContainedOrgan(organ);
            UpdateAllBodyPartHeartConnections();

            //make organ child of bodypart
            organ.transform.SetParent(bodyPart.transform);

            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organtransplant += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;

        }

    }


    public static void ConnectBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, float seconds, int goldCost)
    {
        StaticCoroutine.Start(ConnectBodyPartCoroutine(bodyPart1, bodyPart2, seconds, goldCost));
    }

    public static IEnumerator ConnectBodyPartCoroutine(BodyPart bodyPart1, BodyPart bodyPart2, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            bodyPart1.CreateConnection(bodyPart2);
            bodyPart2.CreateConnection(bodyPart1);
            UpdateAllBodyPartHeartConnections();

            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_attachments += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        }

    }

    public static void DeleteBodyPart(BodyPart bodyPart, float seconds, int goldCost)
    {
        GameObject.FindObjectOfType<Clock>().StartClockUntil(seconds);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        StaticCoroutine.Start(DeleteBodyPartCoroutine(bodyPart, seconds, goldCost));
    }

    public static IEnumerator DeleteBodyPartCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        buttonActions.DisableAllButtons();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.EnableAllButtons();
        if (!clock.actionCancelFlag)
        {
            foreach (Organ organ in bodyPart.containedOrgans)
            {
                GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(organ);
                GameObject.Destroy(organ.gameObject);
            }
            GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(bodyPart);
            GameObject.FindObjectOfType<BodyPartManager>().bodyParts.Remove(bodyPart);
            GameObject.Destroy(bodyPart.gameObject);
            UpdateAllBodyPartHeartConnections();

            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_destroyed += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;

        }

    }

    private static void UpdateAllBodyPartHeartConnections()
    {
        List<BodyPart> bodyParts = GameObject.FindObjectsOfType<BodyPart>().ToList();
        foreach(BodyPart bodyPart in bodyParts)
        {
            bodyPart.UpdateHeartConnections();
        }
    }

}
