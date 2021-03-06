﻿using System.Collections;
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
        if (GM.GameInstance.wasKickstarted)
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                GM.GameInstance.AddPlayer(i);
            }
        }
    }
}
