using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScore
{
    private Dictionary<int, int> PlayersScores = new Dictionary<int, int>();

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
    
    
}
