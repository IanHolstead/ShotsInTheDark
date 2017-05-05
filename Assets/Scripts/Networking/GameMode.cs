using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMode : NetworkBehaviour
{
    public SpawnLocation spawnLocation;

    private int nextPlayerID = 0;
    private int nextCharacterID = 0;

    // Use this for initialization
    void Awake () {
        Logger.Log("GameModeAwake");
        spawnLocation = FindObjectOfType<SpawnLocation>();
    }


    // Update is called once per frame
    void Update () {
		
	}

    [Server]
    public void UpdateConnectingClient(PlayerController connectingController)
    {
        Logger.Log("Updating controller: " + connectingController.ID);
        foreach(KeyValuePair<int, PlayerController> controller in NetworkGameManager.Instance.GetGameState().activeControllers)
        {
            if (controller.Value != connectingController)
            {
                controller.Value.TargetSetID(connectingController.GetComponent<NetworkIdentity>().connectionToClient, controller.Key);
            }
        }
    }

    [Server]
    public void CreateNewCharacter(PlayerController controller)
    {
        //TODO: I'd like to use SpawnLocation.GetSpawnLocation() so it can check if the spawn is valid at runtime
        GameObject newCharacter = Instantiate(NetworkGameManager.Instance.characterPrefab, ((MyNetworkManager)NetworkManager.singleton).startPositions[0]);
        NetworkServer.Spawn(newCharacter);
        Logger.Log("Character Being spawned");
        if (NetworkGameManager.Instance.autoPossessCharacters)
        {
            Logger.Log("Character Attempt Possession");
            controller.Possess(newCharacter.GetComponent<Character>());
        }
    }

    [Server]
    public int RegisterNewCharacter(Character character)
    {
        Logger.Log("Registering new character: " + character);
        int assignedID = nextCharacterID;
        nextCharacterID++;

        NetworkGameManager.Instance.GetGameState().AddCharacter(assignedID, character);

        return assignedID;
    }

    //TODO: this isn't called
    [Server]
    public bool RemoveCharacter(int id)
    {
        return NetworkGameManager.Instance.GetGameState().characters.Remove(id);
    }

    [Server]
    public int RegisterNewPlayer(PlayerController controller)
    {
        Logger.Log("Registering new Controller: " + controller);
        int assignedID = nextPlayerID;
        nextPlayerID++;

        NetworkGameManager.Instance.GetGameState().AddController(assignedID, controller);

        if (NetworkGameManager.Instance.autoCreateCharacters)
        {
            CreateNewCharacter(controller);
        }

        return assignedID;
    }

    //TODO: this isn't called
    [Server]
    public bool RemovePlayer(int id)
    {
        return NetworkGameManager.Instance.GetGameState().activeControllers.Remove(id);
    }
}
