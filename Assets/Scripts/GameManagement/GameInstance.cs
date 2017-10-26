using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour {
    private List<Player> players;

    public bool wasKickstarted = false;

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
    }

    void Start()
    {
        if (!wasKickstarted)
        {
            Startup();
        }
    }

    private void Startup()
    {
        players = new List<Player>();

        DontDestroyOnLoad(gameObject);
        GM.GameInstance = this;
    }

    //This should only be used for dev purposes
    public void Kickstart()
    {
        Logger.Log("Kickstarting game instance, this better not be a real level!", this, LogLevel.Warning);
        wasKickstarted = true;
        Startup();
    }
	
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
#else
            Application.Quit();
#endif
        }
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
