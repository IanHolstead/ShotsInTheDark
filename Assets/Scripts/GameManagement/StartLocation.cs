using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLocation : MonoBehaviour {
    [Range(0,GM.MAXPLAYERCOUNT - 1)]
    public int playerNumber;
    [Range(-1, GM.MAXPLAYERCOUNT)]
    public int numberOfPlayers = -1;
    public float radius = .45f;


    //TODO: IAN YOU BIG DUMB DUMB YOU CAN'T REFRENCE CODE IN THE EXTENSTIONS FOLDER
    //void OnDrawGizmos()
    //{
    //    DebugExtension.DrawCircle(transform.position, Vector3.back, GetCircleColour(), radius);
    //    DebugExtension.DrawArrow(transform.position, transform.rotation * Vector3.right, GetArrowColour());
    //}

    private Color GetCircleColour()
    {
        switch (numberOfPlayers)
        {
            case (-1):
                return Color.cyan;
            case (2):
                return Color.white;
            case (3):
                return Color.grey;
            case (4):
                return Color.black;

            default:
                return Color.yellow;
        }
    }

    private Color GetArrowColour()
    {
        switch (playerNumber)
        {
            case(0):
                return Color.red;
            case (1):
                return Color.blue;
            case (2):
                return Color.magenta;
            case (3):
                return Color.green;

            default:
                return Color.yellow;
        }
    }
}
