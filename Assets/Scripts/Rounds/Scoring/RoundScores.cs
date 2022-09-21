using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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

    public int GetPlayerDiffInMatch(Match match, int playerId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetPlayerDiff(playerId);
        }

        return 0;
    }

    public int GetMatchRankPoint(Match match, int playerId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetRankPoint(playerId);
        }

        return 0;
    }

    public static RoundScores GenerateRandomScores(Round round)
    {
        RoundScores randomRoundScores = new RoundScores();
        foreach (Match match in Enum.GetValues(typeof(Match)))
        {
            randomRoundScores.MatchScores.Add(match,MatchScore.GenerateRandomScores(round,match));
        }

        return randomRoundScores;
    }

    public int GetMatchWinner(Match match)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetWinner();
        }

        return -1;
    }
}
