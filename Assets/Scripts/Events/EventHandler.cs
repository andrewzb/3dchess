using System;
using System.Collections.Generic;

public delegate void SwithToOtherPlayer(FigureTeamType figureTeamType);
public delegate void WriteTurnChanges(TurnChange currentTurnChange);

public delegate void PassGameEvent(GameEvent GameEvent);

public static class EventHandler
{
    public static event SwithToOtherPlayer SwithToOtherPlayerTeamEvent;
    public static event WriteTurnChanges WriteTurnChangesEvent;
    public static event PassGameEvent EmitGameEvent;

    public static void CallSwithToOtherPlayerTeamEvent(FigureTeamType figureTeamType)
    {
        if (SwithToOtherPlayerTeamEvent != null)
        {
            SwithToOtherPlayerTeamEvent(figureTeamType);
        }
    }

    public static void CallWriteTurnChangesEvent(TurnChange currentTurnChange)
    {
        if (WriteTurnChangesEvent != null)
        {
            WriteTurnChangesEvent(currentTurnChange);
        }
    }    
    public static void CallGameEvent(GameEvent gameEvent)
    {
        if (EmitGameEvent != null)
        {
            EmitGameEvent(gameEvent);
        }
    }


}