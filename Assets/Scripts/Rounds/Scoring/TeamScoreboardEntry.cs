using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamScoreboardEntry
{
    public int TeamId { get; set; }
    
    public int Wins { get; set; }
    
    public int Looses { get; set; }
    public int Diff { get; set; }
    
    public int Rounds
    {
        get => Wins + Looses;
    }

    public bool HasSameScore(TeamScoreboardEntry other)
    {
        return Wins == other.Wins && Diff == other.Diff;
    }
    
}
