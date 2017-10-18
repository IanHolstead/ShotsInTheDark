using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {
    private List<Player> players;

    public List<Player> Players
    {
        get
        {
            return players;
        }
    }

    void Awake()
    {
        //only allow one of these
        if (FindObjectsOfType<GameInstance>().Length != 1)
        {
            DestroyImmediate(this);
            return;
        }

        players = new List<Player>();

        DontDestroyOnLoad(gameObject);
        GM.GameInstance = this;

    }
	
	void Update () {
		
	}

    public Player GetPlayer(int id)
    {
        return players[id];
    }

    public int AddPlayer(int controllerID)
    {
        players.Add(new Player(new PlayerProfile(), players.Count, controllerID));
        return players.Count - 1;
    }
}
