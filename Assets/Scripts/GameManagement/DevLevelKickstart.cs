using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevLevelKickstart : MonoBehaviour {
    [Range(1,GM.MAXPLAYERCOUNT)]
    public int numberOfPlayers = 2;

    void Awake()
    {
        //this is a bit of a hack, but I mean, this whole script is a hack
        if (GM.GameInstance == null)
        {
            FindObjectOfType<GameInstance>().Kickstart();
        }
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GM.GameInstance.AddPlayer(i);
        }
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
