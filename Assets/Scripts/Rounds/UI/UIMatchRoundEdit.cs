using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMatchRoundEdit : MonoBehaviour
{
    private static UIMatchRoundEdit _instance;
    
    public static UIMatchRoundEdit Instance
    {
        get => _instance;
    }
    
    
    private Match match;
    
    private List<UIEditScore> EditScores = new List<UIEditScore>();
    private List<UIMatchPlayerNameSlot> PlayerNamesSlots = new List<UIMatchPlayerNameSlot>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("Two UIMatchRoundEdit exist, only one should exist");
        }
        
    }

    public void SetMatch(Match newMatch)
    {
        match = newMatch;
    }

    public UIEditScore GetEditScoreBySlot(PlayerSlot slot)
    {
        foreach(UIEditScore editScore in EditScores)
        {
            if (editScore.GetPlayerSlot() == slot)
            {
                return editScore;
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerNamesSlots.AddRange(GetComponentsInChildren<UIMatchPlayerNameSlot>());
        EditScores.AddRange(GetComponentsInChildren<UIEditScore>());
    }

    public void SaveScore()
    {
        foreach (UIEditScore editScore in EditScores)
        {
            Round currentRound = DetailsDatabase.Instance.GetCurrentRound();
            int PlayerId = DetailsDatabase.Instance.GetPlayerInSlot(currentRound, match, editScore.GetPlayerSlot());
            PlayerScoreDatabase.Instance.SetScore(currentRound,match,PlayerId,editScore.GetScore());
        }
    }
    
    
    public void Init(Match match)
    {
        this.match = match;
        foreach(UIMatchPlayerNameSlot nameSlot in PlayerNamesSlots)
        {
            nameSlot.SetMatch(match);
        }

        foreach(UIEditScore editScore in EditScores)
        {
            editScore.SetMatch(match);
            editScore.LoadScoreFromDatabase();
        }
    }
}
