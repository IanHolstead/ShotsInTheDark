using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGameManager : Singleton<NetworkGameManager>
{
    [Header("Gameplay Prefabs")]
    [Tooltip("Remember to add to this to the spawnable prefabs list!")]
    public GameObject gameModePrefab;
    [Tooltip("Remember to add to this to the spawnable prefabs list")]
    public GameObject gameStatePrefab;
    [Tooltip("Remember to add to this to the spawnable prefabs list")]
    public GameObject characterPrefab;

    [Header("Other Settings")]
    public bool autoCreateCharacters = true;
    public bool autoPossessCharacters = true;
    public bool destroyCharacterIfControllerDisconnects = true;

    private GameMode currentGameMode;
    private GameState currentGameState;

    public void Awake()
    {
        Logger.Log("GameManagerAwake");

        GameObject gameModeRef = Instantiate(gameModePrefab);
        currentGameMode = (GameMode)gameModeRef.AddComponent(typeof(GameMode));
        //NetworkServer.Spawn(gameModeRef);
        //TODO: this doesn't look like it will be replicated nicely

        GameObject gameStateRef = Instantiate(gameStatePrefab);
        currentGameState = (GameState)gameStateRef.AddComponent(typeof(GameState));
        //NetworkServer.Spawn(gameStateRef);
    }

    void Start()
    {
        Logger.Log("GameManager Start");
    }

    public GameMode GetGameMode()
    {
        return currentGameMode;
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }
}
