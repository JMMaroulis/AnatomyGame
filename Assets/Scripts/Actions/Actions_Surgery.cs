using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Actions_Surgery
{

    public static void RemoveBodyPart(BodyPart bodyPart, float seconds, int goldCost, float damageInduced, float bloodLossRateInduced)
    {
        StaticCoroutine.Start(RemoveBodyPartCoroutine(bodyPart, seconds, goldCost, damageInduced, bloodLossRateInduced));
    }

    public static IEnumerator RemoveBodyPartCoroutine(BodyPart bodyPart, float seconds, int goldCost, float damageInduced, float bloodLossRateInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);
        buttonActions.ActionInProgress();



        while (clock.isTimePassing)
        {
            RemoveBodyPartDuring(bodyPart, clock, seconds, bloodLossRateInduced, damageInduced);
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            RemoveBodyPartProcess(bodyPart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void RemoveBodyPartDuring(BodyPart bodyPart, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        float bloodLossPerSecond = bloodLossRateInduced / seconds;
        float damagePerSecond = damageInduced / seconds;

        float bloodLossFrame = bloodLossPerSecond * Time.deltaTime * clock.clockSlider.value;
        float damageFrame = damagePerSecond * Time.deltaTime * clock.clockSlider.value;

        bodyPart.bloodLossRate += bloodLossFrame;
        bodyPart.damage += damageFrame;

        foreach (BodyPart connectedBodyPart in bodyPart.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += bloodLossFrame;
            connectedBodyPart.damage += damageFrame;
        }
    }

    public static void RemoveBodyPartProcess(BodyPart bodyPart)
    {
        bodyPart.SeverAllConnections();
        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_amputations += 1;
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
                MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"RemoveBodyPartProcess: {e}");
        }

        UpdateAllBodyPartHeartConnections();
    }

    public static void RemoveOrgan(Organ organObject, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        StaticCoroutine.Start(RemoveOrganCoroutine(organObject, seconds, goldCost, bloodLossRateInduced, damageInduced));
    }

    public static IEnumerator RemoveOrganCoroutine(Organ organ, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            RemoveOrganDuring(organ, clock, seconds, bloodLossRateInduced, damageInduced);
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

    public static void RemoveOrganDuring(Organ organ, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        float bloodLossPerSecond = bloodLossRateInduced / seconds;
        float damagePerSecond = damageInduced / seconds;

        float bloodLossFrame = bloodLossPerSecond * Time.deltaTime * clock.clockSlider.value;
        float damageFrame = damagePerSecond * Time.deltaTime * clock.clockSlider.value;

        foreach (BodyPart connectedBodyPart in organ.connectedBodyParts)
        {
            connectedBodyPart.bloodLossRate += bloodLossFrame;
            connectedBodyPart.damage += damageFrame;
        }
    }

    public static void RemoveOrganProcess(Organ organ)
    {
        //disconnect
        BodyPart organParent = organ.connectedBodyParts[0];
        organ.SeverAllConnections();
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors(organParent);
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
                MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

        UpdateAllBodyPartHeartConnections();

        //remove from being child of bodypart
        organ.transform.SetParent(organ.transform.parent.parent);

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organremovals += 1;
    }

    public static void RemoveEmbeddedObject(EmbeddedObject embeddedObject, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        StaticCoroutine.Start(RemoveEmbeddedObjectCoroutine(embeddedObject, seconds, goldCost, bloodLossRateInduced, damageInduced));
    }

    public static IEnumerator RemoveEmbeddedObjectCoroutine(EmbeddedObject embeddedObject, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            RemoveEmbeddedObjectDuring(embeddedObject.parentBodyPart, clock, seconds, bloodLossRateInduced, damageInduced);
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

    public static void RemoveEmbeddedObjectDuring(BodyPart bodyPart, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        float bloodLossPerSecond = bloodLossRateInduced / seconds;
        float damagePerSecond = damageInduced / seconds;

        float bloodLossFrame = bloodLossPerSecond * Time.deltaTime * clock.clockSlider.value;
        float damageFrame = damagePerSecond * Time.deltaTime * clock.clockSlider.value;

        bodyPart.bloodLossRate += bloodLossFrame;
        bodyPart.damage += damageFrame;

        if (bodyPart is Organ && bodyPart.connectedBodyParts.Count() > 0)
        {
            bodyPart.connectedBodyParts[0].bloodLossRate += bloodLossFrame;
            bodyPart.connectedBodyParts[0].damage += damageFrame;
        }

    }

    public static void RemoveEmbeddedObjectProcess(EmbeddedObject embeddedObject)
    {
        //disconnect
        embeddedObject.Remove();
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"RemoveEmbeddedObjectProcess: {e}");
        }

        UpdateAllBodyPartHeartConnections();

        //remove from being child of bodypart
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
        {
            embeddedObject.transform.SetParent(MonoBehaviour.FindObjectOfType<EmbeddedObjectSelectorManager>().transform);
        }

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_remove_implants += 1;
    }

    public static void ImplantOrgan(Organ organ, BodyPart bodyPart, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {

        if (organ.connectedBodyParts.Count != 0)
        {
            Debug.Log("That organ is already inside something! Don't do that!");
            return;
        }

        StaticCoroutine.Start(ImplantOrganCoroutine(organ, bodyPart, seconds, goldCost, bloodLossRateInduced, damageInduced));
    }

    public static IEnumerator ImplantOrganCoroutine(Organ organ, BodyPart bodyPart, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            ImplantOrganDuring(bodyPart, clock, seconds, bloodLossRateInduced, damageInduced);
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

    public static void ImplantOrganDuring(BodyPart bodyPart, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        float bloodLossPerSecond = bloodLossRateInduced / seconds;
        float damagePerSecond = damageInduced / seconds;

        float bloodLossFrame = bloodLossPerSecond * Time.deltaTime * clock.clockSlider.value;
        float damageFrame = damagePerSecond * Time.deltaTime * clock.clockSlider.value;

        bodyPart.bloodLossRate += bloodLossFrame;
        bodyPart.damage += damageFrame;
    }

    public static void ImplantOrganProcess(Organ organ, BodyPart bodyPart)
    {
        //connect
        organ.CreateConnection(bodyPart);
        bodyPart.AddContainedOrgan(organ);
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
                MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"ImplantOrganProcess: {e}");
        }

        UpdateAllBodyPartHeartConnections();

        //make organ child of bodypart
        organ.transform.SetParent(bodyPart.transform);

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_organtransplant += 1;
    }

    public static void EmbedObject(EmbeddedObject embeddedObject, BodyPart bodypart, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
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

        StaticCoroutine.Start(EmbedObjectCoroutine(embeddedObject, bodypart, seconds, goldCost, bloodLossRateInduced, damageInduced));
    }

    public static IEnumerator EmbedObjectCoroutine(EmbeddedObject embeddedObject, BodyPart bodyPart, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            EmbedObjectDuring(bodyPart, clock, seconds, bloodLossRateInduced, damageInduced);
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            EmbedObjectProcess(embeddedObject, bodyPart);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void EmbedObjectDuring(BodyPart bodyPart, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        ImplantOrganDuring(bodyPart, clock, seconds, bloodLossRateInduced, damageInduced);
    }

    public static void EmbedObjectProcess(EmbeddedObject embeddedObject, BodyPart bodypart)
    {
        //connect
        embeddedObject.Embed(bodypart);
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
                MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"EmbedObjectProcess: {e}");
        }

        UpdateAllBodyPartHeartConnections();

        MonoBehaviour.FindObjectOfType<ActionTracker>().surgery_implants += 1;
    }

    public static void ConnectBodyParts(BodyPart bodyPart1, BodyPart bodyPart2, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        StaticCoroutine.Start(ConnectBodyPartCoroutine(bodyPart1, bodyPart2, seconds, goldCost, bloodLossRateInduced, damageInduced));
    }

    public static IEnumerator ConnectBodyPartCoroutine(BodyPart bodyPart1, BodyPart bodyPart2, float seconds, int goldCost, float bloodLossRateInduced, float damageInduced)
    {
        Clock clock = MonoBehaviour.FindObjectOfType<Clock>();
        clock.StartClockUntil(seconds);

        ButtonActions buttonActions = MonoBehaviour.FindObjectOfType<ButtonActions>();
        buttonActions.UpdateMenuButtonsInteractivity(false);

        buttonActions.ActionInProgress();
        while (clock.isTimePassing)
        {
            ConnectBodyPartDuring(bodyPart1, bodyPart2, clock, seconds, bloodLossRateInduced, damageInduced);
            yield return null;
        }
        buttonActions.ActionFinished();
        if (!clock.actionCancelFlag)
        {
            ConnectBodyPartProcess(bodyPart1, bodyPart2);
            GameObject.FindObjectOfType<GoldTracker>().goldSpent += goldCost;
            MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void ConnectBodyPartDuring(BodyPart bodyPart1, BodyPart bodyPart2, Clock clock, float seconds, float bloodLossRateInduced, float damageInduced)
    {
        float bloodLossPerSecond = bloodLossRateInduced / seconds;
        float damagePerSecond = damageInduced / seconds;

        float bloodLossFrame = bloodLossPerSecond * Time.deltaTime * clock.clockSlider.value;
        float damageFrame = damagePerSecond * Time.deltaTime * clock.clockSlider.value;

        bool x = false;
        x = x || bodyPart1 is ClockworkHead;
        x = x || bodyPart1 is ClockworkTorso;
        x = x || bodyPart1 is ClockworkLeftArm;
        x = x || bodyPart1 is ClockworkLeftLeg;
        x = x || bodyPart1 is ClockworkRightArm;
        x = x || bodyPart1 is ClockworkRightLeg;
        x = x || bodyPart1 is ClockworkTorso;
        x = x || bodyPart1 is ClockworkLiver;
        x = x || bodyPart1 is ClockworkHeart;
        x = x || bodyPart1 is ClockworkLeftEye;
        x = x || bodyPart1 is ClockworkRightEye;
        x = x || bodyPart1 is ClockworkBrain;
        x = x || bodyPart1 is ClockworkStomach;
        x = x || bodyPart1 is ClockworkLeftLung;
        x = x || bodyPart1 is ClockworkRightLung;
        if (!x)
        {
            bodyPart1.bloodLossRate += bloodLossFrame;
            bodyPart1.damage += damageFrame;
        }

        bool y = false;
        y = y || bodyPart2 is ClockworkHead;
        y = y || bodyPart2 is ClockworkTorso;
        y = y || bodyPart2 is ClockworkLeftArm;
        y = y || bodyPart2 is ClockworkLeftLeg;
        y = y || bodyPart2 is ClockworkRightArm;
        y = y || bodyPart2 is ClockworkRightLeg;
        y = y || bodyPart2 is ClockworkTorso;
        y = y || bodyPart2 is ClockworkLiver;
        y = y || bodyPart2 is ClockworkHeart;
        y = y || bodyPart2 is ClockworkLeftEye;
        y = y || bodyPart2 is ClockworkRightEye;
        y = y || bodyPart2 is ClockworkBrain;
        y = y || bodyPart2 is ClockworkStomach;
        y = y || bodyPart2 is ClockworkLeftLung;
        y = y || bodyPart2 is ClockworkRightLung;
        if (!y)
        {
            bodyPart2.bloodLossRate += bloodLossFrame;
            bodyPart2.damage += damageFrame;
        }

    }

    public static void ConnectBodyPartProcess(BodyPart bodyPart1, BodyPart bodyPart2)
    {
        bodyPart1.CreateConnection(bodyPart2);
        bodyPart2.CreateConnection(bodyPart1);
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"ConnectBodyPartProcess: {e}");
        }

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
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                MonoBehaviour.FindObjectOfType<BodyPartStatusManager>().UpdateStatusCollection();
            }
            buttonActions.SelectSurgeryAction();
        }

    }

    public static void DeleteBodyPartProcess(BodyPart bodyPart)
    {
        //destroy all organs contained in the bodypart
        foreach (Organ organ in bodyPart.containedOrgans)
        {
            try
            {
                GameObject.FindObjectOfType<BodyPartManager>().bodyParts.Remove(organ);
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
                {
                    GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(organ);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }

            GameObject.Destroy(organ.gameObject);
        }

        //destroy all objects embedded in the bodypart
        foreach (EmbeddedObject embeddedObject in bodyPart.embeddedObjects)
        {
            GameObject.Destroy(embeddedObject.gameObject);
        }

        //remove bodypart from tracking
        try
        {
            bodyPart.SeverAllConnections();
            GameObject.FindObjectOfType<BodyPartManager>().bodyParts.Remove(bodyPart);
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartStatusManager>().RemoveStatus(bodyPart);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

        if (bodyPart is Organ)
        {
            BodyPartManager x = GameObject.FindObjectOfType<BodyPartManager>();
            GameObject.FindObjectOfType<BodyPartManager>().organs.Remove((Organ)bodyPart);
        }

        GameObject.Destroy(bodyPart.gameObject);
        try
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "SampleScene")
            {
                GameObject.FindObjectOfType<BodyPartSelectorManager>().ResetSelectors();
                GameObject.FindObjectOfType<EmbeddedObjectSelectorManager>().ResetSelectors();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

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
