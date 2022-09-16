using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerScoreDatabase : MonoBehaviour
{
    private static PlayerScoreDatabase _instance;

    public static PlayerScoreDatabase Instance
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

    

    private Dictionary<Round, RoundScores> roundScores = new Dictionary<Round, RoundScores>();

    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetScore(Round round, Match match, int PlayerId, int PlayerScore)
    {
        if (!roundScores.ContainsKey(round))
        {
            RoundScores newRoundScores = new RoundScores();
            newRoundScores.SetMatchScore(match,PlayerId,PlayerScore);
            roundScores.Add(round,newRoundScores);
        }
        else
        {
            roundScores[round].SetMatchScore(match,PlayerId,PlayerScore);
        }
    }

    public int GetGameScore(Round round, Match match, int PlayerId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchPlayerScore(match, PlayerId);
        }
        
        return 0;
    }

    public int GetRankPoint(Round round, Match match, int PlayerId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetMatchRankPoint(match, PlayerId);
        }
        
        return 0;
    }

    public int GetCurrentRoundPlayerSlotScore(Round round, Match match, PlayerSlot slot)
    {
        int PlayerId = DetailsDatabase.Instance.GetPlayerInSlot(round, match, slot);
        if (PlayerId >= 0)
        {
            return GetGameScore(round, match, PlayerId);
        }

        return 0;
    }
    
    public int GetPlayerDiffInMatch(Round round, Match match, int playerId)
    {
        if (roundScores.ContainsKey(round))
        {
            return roundScores[round].GetPlayerDiffInMatch(match, playerId);
        }

        return 0;
    }

    public List<PlayerScoreboardEntry> GenerateScoreBoardEntries()
    {
        int roundCount = 1;
        Dictionary<int, PlayerScoreboardEntry> scoreboardEntries = new Dictionary<int, PlayerScoreboardEntry>();
        
        foreach(Round round in Enum.GetValues(typeof(Round)))
        {
            foreach (Match match in Enum.GetValues(typeof(Match)))
            {
                List<int> PlayersInMatch = DetailsDatabase.Instance.GetPlayerInMatch(round, match);
                foreach (int playerId in PlayersInMatch)
                {
                    PlayerScoreboardEntry scoreboardEntry;
                    if (!scoreboardEntries.ContainsKey(playerId))
                    {
                        scoreboardEntry = new PlayerScoreboardEntry();
                        scoreboardEntry.PlayerId = playerId;
                        scoreboardEntries.Add(playerId, scoreboardEntry);

                    }
                    else
                    {
                        scoreboardEntry = scoreboardEntries[playerId];

                    }

                    scoreboardEntry.Points += GetRankPoint(round, match, playerId);
                    scoreboardEntry.Diff += GetPlayerDiffInMatch(round, match, playerId);
                    if (HasMatchEntry(round, match))
                    {
                        scoreboardEntry.Rounds++;
                    }
                }
            }
            
            roundCount++;
        }

        return scoreboardEntries.Values
            .OrderByDescending(scoreEntry => scoreEntry.Points)
            .ThenByDescending(scoreEntry => scoreEntry.Diff)
            .ThenBy(scoreEntry => scoreEntry.PlayerId)
            .ToList();
    }

    void Update()
    {
        
    }

    public bool HasPlayerSlotScoreEntry(Round round, Match match,int PlayerId)
    {
        if (roundScores.ContainsKey(round))
        {
            roundScores[round].HasPlayerSlotScoreEntry(match, PlayerId);
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
       Round currentRound = DetailsDatabase.Instance.GetCurrentRound();
       RoundScores mockupScores = RoundScores.GenerateRandomScores(currentRound);
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
