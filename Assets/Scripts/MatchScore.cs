using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScore
{
    private Dictionary<int, int> PlayersScores = new Dictionary<int, int>();
    
    public static MatchScore GenerateRandomScores(Round round,Match match)
    {
        MatchScore randomMatchScore = new MatchScore();
        List<int> playerInmatch = DetailsDatabase.Instance.GetPlayerInMatch(round, match);
        foreach (int playerId in playerInmatch)
        {
            int RandomScore = Random.Range(0, 5);
            randomMatchScore.SetPlayerScore(playerId,RandomScore);
        }
        
        int randomWinner = playerInmatch[Random.Range(0, playerInmatch.Count)];
        randomMatchScore.SetPlayerScore(randomWinner,5);
        
        return randomMatchScore;
    }

    public void SetPlayerScore(int PlayerId, int score)
    {
        if (PlayersScores.ContainsKey(PlayerId))
        {
            PlayersScores[PlayerId] = score;
        }
        else
        {
            PlayersScores.Add(PlayerId,score);
        }
    }

    public int GetPlayerScore(int PlayerId)
    {
        if (PlayersScores.ContainsKey(PlayerId))
        {
            return PlayersScores[PlayerId];
        }

        return 0;
    }


    public bool HasPlayerScoreEntry(int PlayerId)
    {
        return PlayersScores.ContainsKey(PlayerId);
    }


    public int GetPlayerDiff(int playerId)
    {
        int MyScore = GetPlayerScore(playerId);

        //Search higher score other player than me
        int HigherScore = 0;
        foreach (int otherPlayerId in PlayersScores.Keys)
        {
            int otherPlayerScore = GetPlayerScore(otherPlayerId);
            if (otherPlayerId != playerId && otherPlayerScore > HigherScore)
            {
                HigherScore = otherPlayerScore;
            }
        }

        return MyScore-HigherScore;
    }

    public int GetRankPoint(int playerId)
    {
        int PlayersAboveMe = 0;
        int MyScore = GetPlayerScore(playerId);
        
        //Count the number of player that have better score than me
        foreach (int otherPlayerId in PlayersScores.Keys)
        {
            int otherPlayerScore = GetPlayerScore(otherPlayerId);
            if (otherPlayerId != playerId && otherPlayerScore > MyScore)
            {
                PlayersAboveMe++;
            }
        }

        switch (PlayersAboveMe)
        {
            case 0: return 5;
            case 1: return 3;
            case 2: return 2;
            case 3: return 1;
            default: return 1;
        }
    }
    

}

