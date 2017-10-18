using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLocation : MonoBehaviour {
    public int playerNumber;
    public float radius = .45f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        DebugExtension.DrawCircle(transform.position, Vector3.back, GetColour(), radius);
        DebugExtension.DrawArrow(transform.position, transform.rotation * Vector3.right, GetColour());
    }

    private Color GetColour()
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
