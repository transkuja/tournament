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

    

    private Dictionary<Round, RoundScores> RoundScores = new Dictionary<Round, RoundScores>();



    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetScore(Round round, Match match, int PlayerId, int PlayerScore)
    {
        if (!RoundScores.ContainsKey(round))
        {
            RoundScores newRoundScores = new RoundScores();
            newRoundScores.SetMatchScore(match,PlayerId,PlayerScore);
            RoundScores.Add(round,newRoundScores);
        }
        else
        {
            RoundScores[round].SetMatchScore(match,PlayerId,PlayerScore);
        }
    }

    public int GetScore(Round round, Match match, int PlayerId)
    {
        if (RoundScores.ContainsKey(round))
        {
            return RoundScores[round].GetMatchPlayerScore(match, PlayerId);
        }
        
        return 0;
        
    }

    public int GetCurrentRoundPlayerSlotScore(Round round, Match match, PlayerSlot slot)
    {
        int PlayerId = DetailsDatabase.Instance.GetPlayerInSlot(round, match, slot);
        if (PlayerId >= 0)
        {
            return GetScore(round, match, PlayerId);
        }

        return 0;
    }
    
    public int GetPlayerDiffInMatch(Round round, Match match, int PlayerId)
    {
        return 0;
    }

    public List<PlayerScoreboardEntry> GenerateScoreBoardEntries()
    {
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
                    scoreboardEntry.Rounds++;
                    scoreboardEntry.Points += GetScore(round, match, playerId);
                }
            }
        }

        return scoreboardEntries.Values.OrderByDescending(scoreEntry1 => scoreEntry1.Points).ToList();
    }

    void Update()
    {
        
    }

    public bool HasPlayerSlotScoreEntry(Round round, Match match,int PlayerId)
    {
        if (RoundScores.ContainsKey(round))
        {
            RoundScores[round].HasPlayerSlotScoreEntry(match, PlayerId);
        }

        return false;
    }

   public  bool HasMatchEntry(Round round, Match match)
    {
        if (RoundScores.ContainsKey(round))
        {
            return RoundScores[round].HasMatchEntry(match);
        }

        return false;
    }
}
