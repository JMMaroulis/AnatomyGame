using UnityEngine;

public class Liver : Organ
{

    public float bloodProcessingRate;

    void BloodProcessing(float deltaTime)
    {
        this.slowPoison -= bloodProcessingRate * deltaTime * efficiency;
    }


    public override void UpdateBodyPart(float deltaTime)
    {
        BloodProcessing(deltaTime);
        UpdateSelf(deltaTime);
    }

    public void SeverConnection(GameObject connectedBodyPart)
    {
        throw new System.NotImplementedException();
    }

    public void SeverAllConnections()
    {
        throw new System.NotImplementedException();
    }



}
