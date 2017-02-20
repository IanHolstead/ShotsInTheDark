using UnityEngine;
using System.Collections;
using ControlWrapping;

public class Test : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Logger.Log("test");
        }
    }
}
