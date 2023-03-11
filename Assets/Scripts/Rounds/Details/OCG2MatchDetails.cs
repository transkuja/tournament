using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OCG2MatchDetails
{
    
    
    private Dictionary<TeamSlot, int> TeamInRoom = new Dictionary<TeamSlot, int>();

    public OCG2MatchDetails()
    {
        TeamInRoom.Add(TeamSlot.Team1,-1);
        TeamInRoom.Add(TeamSlot.Team2,-1);
    }

    public bool ContainTeam(int TeamId)
    {
        return TeamInRoom.ContainsValue(TeamId);
    }

    public List<int> GetTeamsInMatch()
    {
        return TeamInRoom.Values
            .Distinct()
            .Where(value => value >= 0).ToList();
    }

   public void SetTeamInSlot(TeamSlot teamSlot,int playerId)
    {
        if (!ContainTeam(playerId))
        {
            if (!TeamInRoom.ContainsKey(teamSlot))
            {
                TeamInRoom.Add(teamSlot,playerId);
            }
            else
            {
                TeamInRoom[teamSlot] = playerId;
            }
        }

    }

    public int GetTeamInSlot(TeamSlot slot)
    {
        if (TeamInRoom.ContainsKey(slot))
        {
            return TeamInRoom[slot];
        }

        return -1;
    }
    

}
