using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkStartPosition))]
public class SpawnLocation : MonoBehaviour {

    private bool isValid = true;
	// Use this for initialization
	void Start () {
		
	}
	
    //TODO: this should evaluate if this is actually a valid location
    //aka, is there anything blocking spawn and return null if invalid
    public Transform GetSpawnLocation()
    {
        if (isValid)
        {
            return transform;
        }
        return null;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
