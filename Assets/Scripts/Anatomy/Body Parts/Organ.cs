
public class Organ : BodyPart
{
    //I *REALLY* don't like this solution, but until I can think of a better answer, we're going to insist that all organs only even have one object in the bodypart connection lists
    //And by insist, I mean don't do otherwise, cause it's not going to force you not to, but it will probably break things
    //At this point, the Organ object type only exists so that unity can find all the organs easily and differentiate them from non-organs

    void Update()
    {
        if (connectedBodyParts.Count != 0)
        {
            isPartOfMainBody = connectedBodyParts[0].isPartOfMainBody;
        }
        else
        {
            isPartOfMainBody = false;
        }
    }
}
