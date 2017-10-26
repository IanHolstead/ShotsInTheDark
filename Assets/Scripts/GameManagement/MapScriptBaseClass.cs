using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapScriptBaseClass : Singleton<MapScriptBaseClass> {

    [Range(1, GM.MAXPLAYERCOUNT)]
    public int minPlayerCount = 2;
    [Range(1, GM.MAXPLAYERCOUNT)]
    public int maxPlayerCount = 4;
    
    public GameObject DefaultPawn;

    public float matchStartDelay = 1;
    public float matchEndDelay = 3;

    private List<StartLocation> startLocations;
    private List<bool> startLocationsUsed;

    private MatchState state = MatchState.WaitingToStart;

    private float age = 0;

    public MatchState State
    {
        get
        {
            return state;
        }
    }

    void Awake()
    {
        startLocations = new List<StartLocation>(GM.MAXPLAYERCOUNT);
        startLocationsUsed = new List<bool>(GM.MAXPLAYERCOUNT);

        for (int i = 0; i < GM.MAXPLAYERCOUNT; i++)
        {
            startLocations.Add(null);
            startLocationsUsed.Add(false);
        }
    }

    protected virtual void Start ()
    {
        age = matchStartDelay;
        GM.Map = this;
        FindPlayerStarts();
        SpawnPlayers();
	}	

    protected virtual void Update()
    {
        if (age > 0)
        {
            age -= Time.deltaTime;
        }
        switch (state)
        {
            case MatchState.WaitingToStart:
                if (age <= 0)
                {
                    state = MatchState.Running;
                    Logger.Log("Starting Match");
                }
                break;
            case MatchState.Running:
                CheckForVictory();
                break;
            case MatchState.Paused:
                break;
            case MatchState.Finished:
                if (age <= 0)
                {
                    FinishMatch();
                }
                break;
            default:
                break;
        }
    }

    protected virtual void FindPlayerStarts()
    {
        StartLocation[] starts = FindObjectsOfType<StartLocation>();

        int numberOfValidStarts = 0;
        foreach (StartLocation start in starts)
        {
            if (start.numberOfPlayers == -1 || start.numberOfPlayers == GM.GameInstance.Players.Count)
            {
                if (startLocations.Count > start.playerNumber && startLocations[start.playerNumber] != null)
                {
                    Logger.Log("The same player number is used twice!", this, LogLevel.Warning);
                    numberOfValidStarts--;
                }
                numberOfValidStarts++;
                startLocations[start.playerNumber] = start;
                startLocationsUsed[start.playerNumber] = false;
            }
        }

        bool hitValidStart = false;
        for (int i = GM.MAXPLAYERCOUNT - 1; i > 0; i--)
        {
            if (startLocations[i] == null)
            {
                if (hitValidStart)
                {
                    Logger.Log("Missing start location: " + (i - 1), this, LogLevel.Warning);
                }
                else
                {
                    startLocations.RemoveAt(i);
                    startLocationsUsed.RemoveAt(i);
                }
            }
            else
            {
                hitValidStart = true;
            }
        }

        if (numberOfValidStarts < GM.GameInstance.Players.Count)
        {
            Logger.Log("Too many players for level!", this, LogLevel.Error);
        }
    }

    protected virtual void SpawnPlayers()
    {
        foreach (Player player in GM.GameInstance.Players)
        {
            StartLocation startLocation = GetStartLocation();
            GameObject pawnGameObject = Instantiate(DefaultPawn, startLocation.transform.position, Quaternion.identity);
            PlayerPawn pawn = pawnGameObject.GetComponent<PlayerPawn>();
            player.SetPawn(pawn);
            pawn.SetPlayerParent(player);
            pawn.SetInitialDirection(startLocation.transform.rotation);
        }
    }

    protected virtual void CheckForVictory()
    {
        int numberOfLivingPlayers = 0;
        Player winningPlayer = null;
        foreach (Player player in GM.GameInstance.Players)
        {
            if (player.Pawn.IsPlayerAlive)
            {
                numberOfLivingPlayers++;
                if (numberOfLivingPlayers == 1)
                {
                    winningPlayer = player;
                }
            }
        }

        if (numberOfLivingPlayers > 1)
        {
            return;
        }
        age = matchEndDelay;
        state = MatchState.Finished;
        WinnerPicked(winningPlayer);
    }

    protected virtual void WinnerPicked(Player winner)
    {
        //TODO: write this method
        Logger.Log("Winning Player: " + winner, this, LogLevel.Log);
    }

    protected virtual void FinishMatch()
    {
        //TODO: Write this method
        Logger.Log("Finished match", this, LogLevel.Log);
    }

    protected abstract void MapUpdates();

    /// <summary>
    /// Returns a start location on the map. If called with no number, the first free location is returned. If no free locations are availble it returns null.
    /// If provided a number it returns that location regardless of it it is used
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public StartLocation GetStartLocation(int number = -1)
    {
        if (number < minPlayerCount || number >= maxPlayerCount)
        {//get next unused location
            for (int i = 0; i < startLocations.Count; i++)
            {
                if (!startLocationsUsed[i])
                {
                    startLocationsUsed[i] = true;
                    return startLocations[i];
                }
            }
            return null;
        }
        else
        {
            if (startLocationsUsed[number])
            {
                Logger.Log("Start location was already in use!", this, LogLevel.Log);
            }
            startLocationsUsed[number] = true;
            return startLocations[number];
        }
    }
}

public enum MatchState
{
    WaitingToStart, Running, Paused, Finished
}
