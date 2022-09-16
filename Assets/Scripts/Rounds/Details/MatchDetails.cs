using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchDetails
{
    
    
    private Dictionary<PlayerSlot, int> PlayerInRoom = new Dictionary<PlayerSlot, int>();

    public MatchDetails()
    {
        PlayerInRoom.Add(PlayerSlot.Player1,-1);
        PlayerInRoom.Add(PlayerSlot.Player2,-1);
        PlayerInRoom.Add(PlayerSlot.Player3,-1);
        PlayerInRoom.Add(PlayerSlot.Player4,-1);
    }

    public bool ContainPlayer(int PlayerId)
    {
        return PlayerInRoom.ContainsValue(PlayerId);
    }

    public List<int> GetPlayersInMatch()
    {
        return PlayerInRoom.Values
            .Distinct()
            .Where(value => value >= 0).ToList();
    }

   public void SetPlayerInSlot(PlayerSlot playerSlot,int playerId)
    {
        if (!ContainPlayer(playerId))
        {
            if (!PlayerInRoom.ContainsKey(playerSlot))
            {
                PlayerInRoom.Add(playerSlot,playerId);
            }
            else
            {
                PlayerInRoom[playerSlot] = playerId;
            }
        }

    }

    public int GetPlayerInSlot(PlayerSlot slot)
    {
        if (PlayerInRoom.ContainsKey(slot))
        {
            return PlayerInRoom[slot];
        }

        return -1;
    }
    

}
