using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<int> GetPlayersInMatch(Match match)
    {
        if (MatchDetails.ContainsKey(match))
        {
            return MatchDetails[match].GetPlayersInMatch();
        }

        return new List<int>();
    }

    public void SetPlayersInSlot(List<int> playerIds)
    {
        for (int i = 0; i < playerIds.Count; i++)
        {
            Match match = (Match)(i / 4);
            PlayerSlot slot = (PlayerSlot)(i % 4);
            SetPlayerInSlot(match,slot,playerIds[i]);
        }
    }

    public static RoundDetails GenerateDefault()
    {
        RoundDetails roundDetails = new RoundDetails();
        List<int> PlayerIds = new List<int>();
        for(int i = 0;i < PlayerConst.MaxPlayer;i++)
        {
            PlayerIds.Add(i);
        }

        //Shuffle 
       // Random random = new Random();
        // PlayerIds.OrderBy(item => random.Next());

        roundDetails.SetPlayersInSlot(PlayerIds);

        return roundDetails;
    }

    public static RoundDetails GenerateByCurrentRanking()
    {
        RoundDetails roundDetails = new RoundDetails();
        List<PlayerScoreboardEntry> entries = PlayerScoreDatabase.Instance.GenerateScoreBoardEntries();

        List<int> PlayerIds = entries.Select(entry => entry.PlayerId).ToList();
        roundDetails.SetPlayersInSlot(PlayerIds);

        return roundDetails;
    }
}
