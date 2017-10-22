using System.Collections.Generic;

public static class GM {

    public const int MAXPLAYERCOUNT = 4;
    
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
}
