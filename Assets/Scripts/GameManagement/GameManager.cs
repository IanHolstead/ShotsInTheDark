using System.Collections.Generic;

public static class GM { 
    static Player[] playerList = new Player[4];
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

    public static int AddPlayer(Player player)
    {
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

    public static PlayerProfile GetProfile(int playerID)
    {
        //TODO: not implemented yet
        return new PlayerProfile();
    }
}
