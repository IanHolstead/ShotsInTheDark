using System.Collections.Generic;

public static class GM {

    public const int MAXPLAYERCOUNT = 4;

    static PlayerPawn[] playerList = new PlayerPawn[4];
    private static GameInstance gameInstance;

    //private static GameMode gameMode;


    public static GameInstance GameInstance
    {
        get
        {
            return gameInstance;
        }

        set
        {
            if (!gameInstance)
            {
                gameInstance = value;
            }
            else
            {
                Logger.Log("Trying to set game instance twice!", null, LogLevel.Warning);
            }
        }
    }

    /// <summary>
    /// TODO: DEPRICATED
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public static int AddPlayer(PlayerPawn player)
    {
        Logger.Log("GM.AddPlayer DEPRICATED", null, LogLevel.Warning);

        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i] == null)
            {
                playerList[i] = player;
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// TODO: DEPRICATED
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public static PlayerProfile GetProfile(int playerID)
    {
        Logger.Log("GM.GetProfile DEPRICATED", null, LogLevel.Warning);
        //TODO: implemented yet
        return new PlayerProfile();
    }
}
