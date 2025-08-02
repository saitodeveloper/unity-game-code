using UnityEngine;

class CompassIndicator
{
    public static string WalkingDirection(Vector3 currentPossition, Vector3 movingTo)
    {
        if (currentPossition.x == movingTo.x)
        {
            if (currentPossition.y == movingTo.y) return "STOP";
            else if (currentPossition.y < movingTo.y) return "N";
            else return "S";
        }
        else if (currentPossition.x > movingTo.x)
        {
            if (currentPossition.y == movingTo.y) return "W";
            else if (currentPossition.y < movingTo.y) return "NW";
            else return "SW";
        }

        if (currentPossition.y == movingTo.y) return "E";
        else if (currentPossition.y < movingTo.y) return "NE";
        else if (currentPossition.y > movingTo.y) return "SE";
        else return "SE";
    }
}