using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundScores
{
    private Dictionary<Match, MatchScore> MatchScores = new Dictionary<Match, MatchScore>();

    public void SetMatchScore(Match match, int Playerid, int score)
    {
        if (!MatchScores.ContainsKey(match))
        {
            MatchScore newMatchScore = new MatchScore();
            newMatchScore.SetPlayerScore(Playerid,score);
            MatchScores.Add(match,newMatchScore);
        }
        else
        {
            MatchScores[match].SetPlayerScore(Playerid,score);
        }
    }

    public int GetMatchPlayerScore(Match match, int playerId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetPlayerScore(playerId);
        }

        return 0;
        
    }

    public bool HasPlayerSlotScoreEntry(Match match, int PlayerId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].HasPlayerScoreEntry(PlayerId);
        }

        return false;
    }

    public bool HasMatchEntry(Match match)
    {
        return MatchScores.ContainsKey(match);
    }
}
