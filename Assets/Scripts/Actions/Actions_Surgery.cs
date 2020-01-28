using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Actions_Surgery
{

    public static void RemoveBodyPart(GameObject bodyPartObject)
    {
        bodyPartObject.GetComponent<BodyPart>().SeverAllConnections();
    }

    public static void ConnectBodyParts(GameObject bodyPartObject1, GameObject bodyPartObject2)
    {
        bodyPartObject1.GetComponent<BodyPart>().CreateConnection(bodyPartObject2);
        bodyPartObject2.GetComponent<BodyPart>().CreateConnection(bodyPartObject1);
    }

    public static void DeleteBodyPart(GameObject bodyPartObject)
    {
        RemoveBodyPart(bodyPartObject);
        GameObject.Destroy(bodyPartObject);
    }

}
