using System.Linq;
using UnityEngine;

class CompassIndicator
{
    private static string[] RightMovement = { "N", "NE", "S", "SE", "E" };

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

    public static bool FaceRight(string direction)
    {
        return RightMovement.Contains(direction);
    }
}