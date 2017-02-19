using System.Collections.Generic;
using System;

public static class Logger
{

    public static bool useInGameLogger = true;

    static LogLevel currentLogLevel = LogLevel.Warning;

    public static InGamePanel inGameLogConsole = UnityEngine.Object.FindObjectOfType<InGamePanel>();

    static LinkedList<LogMessage> log = new LinkedList<LogMessage>();

    public static LogLevel CurrentLogLevel
    {
        get
        {
            return currentLogLevel;
        }
    }

    //TODO: the idea behind this was to make it like unreals logs where entries vanish after time
    //TODO: This needs to be a priority Queue if its going to work.
    //private static LinkedList<KeyValuePair<float, LinkedListNode<LogMessage>>> toBeRemoved = new LinkedList<KeyValuePair<float, LinkedListNode<LogMessage>>>();

    public static void Log(object toPrint, object callingClass = null, LogLevel logLevel = LogLevel.Warning)//, float duration = 0)
    {

        if (currentLogLevel != LogLevel.None && currentLogLevel >= logLevel)
        {
            if (UnityEngine.Application.isEditor && !useInGameLogger)
            {
                UnityEngine.Debug.Log(toPrint);
            }
            else if (useInGameLogger && inGameLogConsole != null)
            {
                log.AddLast(new LogMessage(toPrint, callingClass != null ? callingClass.ToString() : "", logLevel));
                //if (duration > 0)
                //{
                //    //toBeRemoved.a UnityEngine.Time.time
                //}
                inGameLogConsole.UpdateLog(log);
            }

        }
    }


}

/// <summary>
/// Log Levels control if messages are printed.
/// <list type="number">
/// <listheader>Levels</listheader>
/// <item>
/// <term>None: </term>
/// <description>should never be used as no messages are printed at this level.</description>
/// </item>
/// <item>
/// <term>Error: </term>
/// <description>something has gone quite wrong.</description>
/// </item>
/// <item>
/// <term>Warning: </term>
/// <description>the default level for debuging.</description>
/// </item>
/// <item>
/// <term>Log: </term>
/// <description>for useful information.</description>
/// </item>
/// <item>
/// <term>Verbose: </term>
/// <description>for calls which occure every frame.</description>
/// </item>
/// <item>
/// <term>Facist: </term>
/// <description>for extrainious information.</description>
/// </item>
/// </list>
/// </summary>
public enum LogLevel
{
    None, Error, Warning, Log, Verbose, Facist
}
