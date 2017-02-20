using System.Collections.Generic;

public static class GameManager {
    static Player[] playerList = new Player[4];

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
	
}

//public enum LogLevel
//{
//    None, Low, Medium, High, Verbose, Facist
//}
