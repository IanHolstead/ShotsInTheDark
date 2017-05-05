using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerState : NetworkBehaviour {

    int id;
    private Character character;

    public int ID
    {
        get
        {
            return id;
        }
    }

    public Character Character
    {
        set
        {
            if (character == null)
            { 
                character = value;
            }
        }
    }

    public void SetID(int id)
    {
        if (id == -1)
        {
            this.id = id;
        }
    }

    //TODO: I'd rather this not be public
    [ClientRpc]
    public void RpcSetID(int id)
    {
        if (id == -1)
        {
            this.id = id;
            NetworkGameManager.Instance.GetGameState().AddCharacter(id, character);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
