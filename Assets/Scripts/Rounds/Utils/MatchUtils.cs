using System;
using System.Collections.Generic;
using UnityEngine;

public static class MatchUtils
{
   public static Room GetMatchRoom(Match match)
    {
        switch (match)
        {
            case Match.Match1:
                return Room.CinemaRoom;
            case Match.Match2:
                return Room.GamingRoom;
            case Match.Match3:
                return Room.CinemaRoom;
            case Match.Match4:
                return Room.GamingRoom;
            case Match.Match5:
                return Room.CinemaRoom;
            case Match.Match6:
                return Room.GamingRoom;
            case Match.Match7:
                return Room.CinemaRoom;
            default:
                return Room.GamingRoom;
        }
    }

    public const int TeamCountInMatch = 2;

    public static Match FindMatchInRoom(List<Match> UsedMatch, Room DesiredRoom)
    {
        foreach (Match match in Enum.GetValues(typeof(Match)))
        {
            if (UsedMatch.Contains(match))
            {
                continue;
            }

            if (GetMatchRoom(match) == DesiredRoom)
            {
                return match;
            }
        }

        //If no match found, return first match avaible
        foreach (Match match in Enum.GetValues(typeof(Match)))
        {
            if (UsedMatch.Contains(match))
            {
                continue;
            }

            return match;
        }


        return Match.Match1;
    }
}