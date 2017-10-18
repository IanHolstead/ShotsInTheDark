using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapScriptBaseClass : Singleton<MapScriptBaseClass> {

    [Range(1, 4)]
    public int minPlayerCount = 2;
    [Range(1, 4)]
    public int maxPlayerCount = 4;

    public GameObject GameMode;
    public GameObject DefaultPawn;


    private List<KeyValuePair<StartLocation, bool>> startLocations;

    void Awake()
    {
        Setup();
        startLocations = new List<KeyValuePair<StartLocation, bool>>(maxPlayerCount);
    }
    // Use this for initialization
    void Start () {
        StartLocation[] starts = FindObjectsOfType<StartLocation>();
        foreach (StartLocation start in starts)
        {
            if (!startLocations[start.playerNumber].Equals(null))
            {
                Logger.Log("The same player number is used twice!", this, LogLevel.Warning);
            }
            startLocations[start.playerNumber] = new KeyValuePair<StartLocation, bool>(start, false);
        }

        if (startLocations.Count < GM.GameInstance.Players.Count)
        {
            Logger.Log("Too many players for level!", this, LogLevel.Error);
        }

        foreach (Player player in GM.GameInstance.Players)
        {
            StartLocation startLocation = GetStartLocation();
            GameObject pawnGameObject = Instantiate(DefaultPawn, startLocation.transform.position, startLocation.transform.rotation);
            PlayerPawn pawn = pawnGameObject.GetComponent<PlayerPawn>();
            player.SetPawn(pawn);
            pawn.SetPlayerParent(player);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
            for (int i = 0; i < startLocations.Capacity; i++)
            {
                if (!startLocations[i].Value)
                {//TODO: ugh. I don't like this :(
                    startLocations[i] = new KeyValuePair<StartLocation, bool>(startLocations[i].Key, true);
                    return startLocations[i].Key;
                }
            }
            return null;
        }
        else
        {
            //TODO: decide what behavior this should have
            if (startLocations[number].Value)
            {
                Logger.Log("Start location was already in use!", this, LogLevel.Warning);
            }
            startLocations[number] = new KeyValuePair<StartLocation, bool>(startLocations[number].Key, true);
            return startLocations[number].Key;
        }
    }

    //Set default values here //TODO: remove?
    public abstract void Setup();
}
