﻿using System.Collections;
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
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            bodyPartObject.SeverAllConnections();
            MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_amputations += 1;
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;

            GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
            GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
            buttonActions.SelectSurgeryAction();
            UpdateAllBodyPartHeartConnections();
        }

    }

    public static void RemoveOrgan(Organ organObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(RemoveOrganCoroutine(organObject, seconds, goldCost));
    }

    public static IEnumerator RemoveOrganCoroutine(Organ organ, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            RemoveOrganProcess(organ);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();

        }

    }

    public static void RemoveOrganProcess(Organ organ)
    {
        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();

        //disconnect
        BodyPart organParent = organ.connectedBodyParts[0];
        organ.SeverAllConnections();
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors(organParent);
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        //remove from being child of bodypart
        organ.transform.SetParent(organ.transform.parent.parent);

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organremovals += 1;
    }

    public static void RemoveEmbeddedObject(EmbeddedObject embeddedObject, float seconds, int goldCost)
    {
        StaticCoroutine.Start(RemoveEmbeddedObjectCoroutine(embeddedObject, seconds, goldCost));
    }

    public static IEnumerator RemoveEmbeddedObjectCoroutine(EmbeddedObject embeddedObject, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            RemoveEmbeddedObjectProcess(embeddedObject);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void RemoveEmbeddedObjectProcess(EmbeddedObject embeddedObject)
    {
        //disconnect
        embeddedObject.Remove();
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        //remove from being child of bodypart
        embeddedObject.transform.SetParent(MonoBehaviour.FindObjectOfType<EmbeddedObjectSelectorManager>().transform);

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_remove_implants += 1;
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
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            ImplantOrganProcess(organ, bodyPart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void ImplantOrganProcess(Organ organ, BodyPart bodyPart)
    {
        //connect
        organ.CreateConnection(bodyPart);
        bodyPart.AddContainedOrgan(organ);
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        //make organ child of bodypart
        organ.transform.SetParent(bodyPart.transform);

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organtransplant += 1;
    }

    public static void EmbedObject(EmbeddedObject embeddedObject, BodyPart bodypart, float seconds, int goldCost)
    {

        if (!(embeddedObject.parentBodyPart is null))
        {
            Debug.Log("That object is already inside something! Don't do that!");
            return;
        }

        if (!embeddedObject.EmbedValidity(bodypart))
        {
            Debug.Log("That is not a valid bodypart to implant that object into!");
            return;
        }

        StaticCoroutine.Start(EmbedObjectCoroutine(embeddedObject, bodypart, seconds, goldCost));
    }

    public static IEnumerator EmbedObjectCoroutine(EmbeddedObject embeddedObject, BodyPart bodypart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            EmbedObjectProcess(embeddedObject, bodypart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void EmbedObjectProcess(EmbeddedObject embeddedObject, BodyPart bodypart)
    {
        //connect
        embeddedObject.Embed(bodypart);
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_implants += 1;
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
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {

        }ConnectBodyPartCoroutine(bodyPart1, bodyPart2);
        GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
        buttonActions.SelectSurgeryAction();

    }

    public static void ConnectBodyPartCoroutine(BodyPart bodyPart1, BodyPart bodyPart2)
    {
        bodyPart1.CreateConnection(bodyPart2);
        bodyPart2.CreateConnection(bodyPart1);
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_attachments += 1;
    }

    public static void DeleteBodyPart(BodyPart bodyPart, float seconds, int goldCost)
    {
        StaticCoroutine.Start(DeleteBodyPartCoroutine(bodyPart, seconds, goldCost));
    }

    public static IEnumerator DeleteBodyPartCoroutine(BodyPart bodyPart, float seconds, int goldCost)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            DeleteBodyPartProcess(bodyPart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void DeleteBodyPartProcess(BodyPart bodyPart)
    {
        foreach (Organ organ in bodyPart.containedOrgans)
        {
            GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(organ);
            GameObject.FindObjectOfType<BodyPartManager>().bodyParts.Remove(organ);
            GameObject.Destroy(organ.gameObject);
        }

        foreach (EmbeddedObject embeddedObject in bodyPart.embeddedObjects)
        {
            GameObject.Destroy(embeddedObject.gameObject);
        }

        bodyPart.SeverAllConnections();
        GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(bodyPart);
        GameObject.FindObjectOfType<BodyPartManager>().bodyParts.Remove(bodyPart);

        if (bodyPart is Organ)
        {
            GameObject.FindObjectOfType<BodyPartManager>().organs.Remove((Organ)bodyPart);
        }

        GameObject.Destroy(bodyPart.gameObject);
        GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
        GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();

        UpdateAllBodyPartHeartConnections();

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_destroyed += 1;
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
