using System.Collections.Generic;

public static class GM {

    public const int MAXPLAYERCOUNT = 4;
    
    private static GameInstance gameInstance;

    private static MapScriptBaseClass map;

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

    public static MapScriptBaseClass Map
    {
        get
        {
            return map;
        }

        set
        {
            map = value;
        }
    }
}
