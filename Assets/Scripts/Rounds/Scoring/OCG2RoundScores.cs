using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class OCG2RoundScores
{
    private Dictionary<Match, OCG2MatchScore> MatchScores = new Dictionary<Match, OCG2MatchScore>();

    public void SetMatchScore(Match match, int TeamId, int score)
    {
        if (!MatchScores.ContainsKey(match))
        {
            OCG2MatchScore newMatchScore = new OCG2MatchScore();
            newMatchScore.SetTeamScore(TeamId,score);
            MatchScores.Add(match,newMatchScore);
        }
        else
        {
            MatchScores[match].SetTeamScore(TeamId,score);
        }
    }

    public int GetMatchTeamScore(Match match, int teamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetTeamScore(teamId);
        }

        return 0;
        
    }

    public bool HasTeamSlotScoreEntry(Match match, int TeamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].HasTeamScoreEntry(TeamId);
        }

        return false;
    }

    public bool HasMatchEntry(Match match)
    {
        return MatchScores.ContainsKey(match);
    }

    public int GetTeamDiffInMatch(Match match, int TeamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetTeamDiff(TeamId);
        }

        return 0;
    }

    public int GetMatchRankPoint(Match match, int TeamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            return MatchScores[match].GetRankPoint(TeamId);
        }

        return 0;
    }

    public int GetMatchWinCount(Match match, int teamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            if (MatchScores[match].GetWinner() == teamId)
            {
                return 1;
            }
        }

        return 0;
    }
    
    public int GetMatchLooseCount(Match match, int teamId)
    {
        if (MatchScores.ContainsKey(match))
        {
            if (MatchScores[match].HasTeamScoreEntry(teamId) && MatchScores[match].GetWinner() != teamId)
            {
                return 1;
            }
        }

        return 0;
    }

    public static OCG2RoundScores GenerateRandomScores(Round round)
    {
        OCG2RoundScores randomRoundScores = new OCG2RoundScores();
        foreach (Match match in Enum.GetValues(typeof(Match)))
        {
            randomRoundScores.MatchScores.Add(match,OCG2MatchScore.GenerateRandomScores(round,match));
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
