using UnityEngine;
using System.Collections;

public class Player 
{
    private PlayerProfile profile;
    /// <summary>
    /// This is the number in the GameInstancesPlayersArray
    /// </summary>
    private int playerID = -1;
    /// <summary>
    /// This is which controller to get input from for this player;
    /// </summary>
    private int controllerID = -1;
    private PlayerPawn pawn;

    public PlayerProfile Profile
    {
        get
        {
            return profile;
        }
    }

    public int PlayerID
    {
        get
        {
            return playerID;
        }
    }

    public PlayerPawn Pawn
    {
        get
        {
            return pawn;
        }
    }

    public int ControllerID
    {
        get
        {
            return controllerID;
        }

        //TODO: remove this
        set
        {
            controllerID = value;
        }
    }

    public Player(PlayerProfile profile, int id, int controllerID)
    {
        UpdateProfile(profile);
        playerID = id;
        this.controllerID = controllerID;
    }

    public void UpdateProfile(PlayerProfile profile)
    {
        if (profile != null)
        {
            this.profile = profile;
        }
    }


    public void SetPawn(PlayerPawn pawn)
    {
        this.pawn = pawn;
    }
}
