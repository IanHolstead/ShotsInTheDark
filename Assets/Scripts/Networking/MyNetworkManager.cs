using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        //GameObject gameModeRef = Instantiate(gameModePrefab);
        //currentGameMode = (GameMode)gameModeRef.AddComponent(gameMode.GetType());
        //gameModeRef.SetActive(true);
        ////NetworkServer.Spawn(gameModeRef);
        
        //GameObject gameStateRef = Instantiate(gameStatePrefab);
        //currentGameState = (GameState)gameStateRef.AddComponent(gameState.GetType());
        ////NetworkServer.Spawn(gameStateRef);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        foreach(UnityEngine.Networking.PlayerController controller in conn.playerControllers)
        {
            //controller.gameObject.GetComponent<PlayerController>().TargetSetID(conn, )
        }
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
    }
}
