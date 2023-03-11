using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OCG2MatchScore
{
    private Dictionary<int, int> TeamScores = new Dictionary<int, int>();
    
    public static OCG2MatchScore GenerateRandomScores(Round round,Match match)
    {
        OCG2MatchScore randomMatchScore = new OCG2MatchScore();
        List<int> teamInmatch = OCG2DetailsDatabase.Instance.GetTeamsInMatch(round, match);
        if (teamInmatch.Count > 0)
        {
            foreach (int teamId in teamInmatch)
            {
                int RandomScore = Random.Range(0, 5);
                randomMatchScore.SetTeamScore(teamId, RandomScore);
            }

            int randomWinner = teamInmatch[Random.Range(0, teamInmatch.Count)];
            randomMatchScore.SetTeamScore(randomWinner, 5);


        }
        return randomMatchScore;
    }

    public void SetTeamScore(int TeamId, int score)
    {
        if (TeamScores.ContainsKey(TeamId))
        {
            TeamScores[TeamId] = score;
        }
        else
        {
            TeamScores.Add(TeamId,score);
        }
    }

    public int GetTeamScore(int TeamId)
    {
        if (TeamScores.ContainsKey(TeamId))
        {
            return TeamScores[TeamId];
        }

        return 0;
    }


    public bool HasTeamScoreEntry(int TeamId)
    {
        return TeamScores.ContainsKey(TeamId);
    }


    public int GetTeamDiff(int TeamId)
    {
        int MyScore = GetTeamScore(TeamId);

        //Search higher score other player than me
        int HigherScore = 0;
        foreach (int otherTeamId in TeamScores.Keys)
        {
            int otherTeamScore = GetTeamScore(otherTeamId);
            if (otherTeamId != TeamId && otherTeamScore > HigherScore)
            {
                HigherScore = otherTeamScore;
            }
        }

        return MyScore-HigherScore;
    }

    public int GetRankPoint(int teamId)
    {
        int TeamAboveMe = 0;
        int MyScore = GetTeamScore(teamId);
        
        //Count the number of player that have better score than me
        foreach (int otherTeamId in TeamScores.Keys)
        {
            int otherTeamScore = GetTeamScore(otherTeamId);
            if (otherTeamId != teamId && otherTeamScore > MyScore)
            {
                TeamAboveMe++;
            }
        }

        switch (TeamAboveMe)
        {
            case 0: return 5;
            case 1: return 3;
            case 2: return 2;
            case 3: return 1;
            default: return 1;
        }
    }


    public int GetWinner()
    {
        int WinnerId = -1;
        int BiggestScore = -1;
        foreach (int otherTeamId in TeamScores.Keys)
        {
            int otherTeamScore = GetTeamScore(otherTeamId);
            if (otherTeamScore > BiggestScore)
            {
                BiggestScore = otherTeamScore;
                WinnerId = otherTeamId;
            }
        }

        return WinnerId;
    }


}

