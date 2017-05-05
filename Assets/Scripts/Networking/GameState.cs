using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameState : NetworkBehaviour
{

    //public static GameState instance;
    int localPlayerController = -1;

    //TODO: make these hashsets? make these arrays?
    internal Dictionary<int, PlayerController> activeControllers;
    internal Dictionary<int, Character> characters;

    void OnGUI()
    {
        if (GetComponent<NetworkIdentity>().isServer)
            GUILayout.Label("Running as a server");
        else
            if (GetComponent<NetworkIdentity>().isClient)
            GUILayout.Label("Running as a client");

    }

    public void Awake()
    {
        //instance = this;
        activeControllers = new Dictionary<int, PlayerController>();
        characters = new Dictionary<int, Character>();

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            foreach(KeyValuePair<int, PlayerController> temp in activeControllers)
            {
                Logger.Log("ID: " + temp.Key + ", controller" + temp.Value + " " + temp.Value.ID);
            }
        }
	}

    public void AddController(int id, PlayerController controller)
    {
        activeControllers.Add(id, controller);
    }

    public void AddCharacter(int id, Character character)
    {
        characters.Add(id, character);
    }

    public PlayerController GetPlayerController()
    {
        return GetPlayerController(localPlayerController);
    }

    public PlayerController GetPlayerController(int id)
    {
        if (activeControllers.ContainsKey(id))
        {
            return activeControllers[id];
        }
        return null;
    }

    public Character GetPlayerCharacter(int id)
    {
        if (characters.ContainsKey(id))
        {
            return characters[id];
        }
        return null;
    }
}
