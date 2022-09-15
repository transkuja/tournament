using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundDetails
{
    private Dictionary<Match, MatchDetails> MatchDetails = new Dictionary<Match, MatchDetails>();
    
    
    public void SetPlayerInSlot(Match match, PlayerSlot slot, int PlayerId)
    {
        if (!MatchDetails.ContainsKey(match))
        {
            MatchDetails newDetails = new MatchDetails();
            newDetails.SetPlayerInSlot(slot,PlayerId);
            MatchDetails.Add(match,newDetails);
        }
        else
        {
            MatchDetails[match].SetPlayerInSlot(slot,PlayerId);
        }
    }
    
    

    public int GetPlayerIdInSlot(Match match, PlayerSlot playerSlot)
    {
        if (MatchDetails.ContainsKey(match))
        {
            return MatchDetails[match].GetPlayerInSlot(playerSlot);
        }

        return -1;
    }
}
