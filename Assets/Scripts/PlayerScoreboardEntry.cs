using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreboardEntry
{
    public int PlayerId { get; set; }
    public int Points { get; set; }
    public int Rounds { get; set; }
    public int Diff { get; set; }

    public bool HasSameScore(PlayerScoreboardEntry other)
    {
        return Points == other.Points && Diff == other.Diff;
    }
    
}
