using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamScoreDatabase : MonoBehaviour
{
    private static TeamScoreDatabase _instance;

    public static TeamScoreDatabase Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    

    private Dictionary<Round, OCG2RoundScores> roundScores = new Dictionary<Round, OCG2RoundScores>();

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetScore(Round round, Match match, int TeamId, int TeamScore)
    {
        if (!roundScores.ContainsKey(round))
        {
            OCG2RoundScores newRoundScores = new OCG2RoundScores();
            newRoundScores.SetMatchScore(match,TeamId,TeamScore);
            roundScores.Add(round,newRoundScores);
        }
        else
        {
            roundScores[round].SetMatchScore(match,TeamId,TeamScore);
        }
    }

    public int GetGameScore(Round round, Match match, int TeamId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchTeamScore(match, TeamId);
        }
        
        return 0;
    }

    public int GetMatchWinner(Round round, Match match)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchWinner(match);
        }
        
        return -1;
    }

    public int GetRankPoint(Round round, Match match, int TeamId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchRankPoint(match, TeamId);
        }
        
        return 0;
    }

    public int GetWinCount(Round round, Match match, int TeamId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchWinCount(match, TeamId);
        }
        
        return 0;
    }
    
    private int GetLooseCount(Round round, Match match, int teamId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchLooseCount(match, teamId);
        }
        
        return 0;
    }


    public int GetCurrentRoundTeamSlotScore(Round round, Match match, TeamSlot slot)
    {
        int TeamId = OCG2DetailsDatabase.Instance.GetTeamInSlot(round, match, slot);
        if (TeamId >= 0)
        {
            return GetGameScore(round, match, TeamId);
        }

        return 0;
    }
    
    public int GetTeamDiffInMatch(Round round, Match match, int teamId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetTeamDiffInMatch(match, teamId);
        }

        return 0;
    }

    public List<TeamScoreboardEntry> GenerateScoreBoardEntries()
    {
        int roundCount = 1;
        Dictionary<int, TeamScoreboardEntry> scoreboardEntries = new Dictionary<int, TeamScoreboardEntry>();
        
        foreach(Round round in Enum.GetValues(typeof(Round)))
        {
            foreach (Match match in Enum.GetValues(typeof(Match)))
            {
                List<int> TeamsInMatch = OCG2DetailsDatabase.Instance.GetTeamsInMatch(round, match);
                foreach (int teamId in TeamsInMatch)
                {
                    TeamScoreboardEntry scoreboardEntry;
                    if (!scoreboardEntries.ContainsKey(teamId))
                    {
                        scoreboardEntry = new TeamScoreboardEntry();
                        scoreboardEntry.TeamId = teamId;
                        scoreboardEntries.Add(teamId, scoreboardEntry);

                    }
                    else
                    {
                        scoreboardEntry = scoreboardEntries[teamId];

                    }
                    
                    scoreboardEntry.Wins += GetWinCount(round, match, teamId);
                    scoreboardEntry.Looses += GetLooseCount(round, match, teamId);
                    scoreboardEntry.Diff += GetTeamDiffInMatch(round, match, teamId);
  
                }
            }
            
            roundCount++;
        }

        return scoreboardEntries.Values
            .OrderByDescending(scoreEntry => scoreEntry.Wins)
            .ThenByDescending(scoreEntry => scoreEntry.Diff)
            .ThenBy(scoreEntry => scoreEntry.TeamId)
            .ToList();
    }


    void Update()
    {
        
    }

    public bool HasTeamSlotScoreEntry(Round round, Match match,int TeamId)
    {
        if (roundScores.ContainsKey(round))
        {
            roundScores[round].HasTeamSlotScoreEntry(match, TeamId);
        }

        return false;
    }

   public  bool HasMatchEntry(Round round, Match match)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].HasMatchEntry(match);
        }

        return false;
    }
   

   public void DebugGenerateRandomScoreForCurrentRound()
   {
       Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
       OCG2RoundScores mockupScores = OCG2RoundScores.GenerateRandomScores(currentRound);
       if (roundScores.ContainsKey(currentRound))
       {
           roundScores[currentRound] = mockupScores;
       }
       else
       {
           roundScores.Add(currentRound,mockupScores);
       }
   }
}
