using System;
using System.Collections.Generic;

public delegate void SwithToOtherPlayer(FigureTeamType figureTeamType);

public static class EventHandler
{
    public static event SwithToOtherPlayer SwithToOtherPlayerTeam;

    public static void CallSwithToOtherPlayerTeamEvent(FigureTeamType figureTeamType)
    {
        if (SwithToOtherPlayerTeam != null)
        {
            SwithToOtherPlayerTeam(figureTeamType);
        }
    }


}