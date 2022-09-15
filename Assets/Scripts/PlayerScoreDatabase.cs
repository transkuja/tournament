using System;
using System.Collections;
using System.Collections.Generic;
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
    
    void Update()
    {
        
    }
}
