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
    private List<UIMatchTeamNameSlot> TeamNamesSlots = new List<UIMatchTeamNameSlot>();

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

    public UIEditScore GetEditScoreBySlot(TeamSlot slot)
    {
        foreach(UIEditScore editScore in EditScores)
        {
            if (editScore.GetTeamSlot() == slot)
            {
                return editScore;
            }
        }

        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        TeamNamesSlots.AddRange(GetComponentsInChildren<UIMatchTeamNameSlot>());
        EditScores.AddRange(GetComponentsInChildren<UIEditScore>());
    }

    public void SaveScore()
    {
        foreach (UIEditScore editScore in EditScores)
        {
            Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
            int TeamId = OCG2DetailsDatabase.Instance.GetTeamInSlot(currentRound, match, editScore.GetTeamSlot());
            TeamScoreDatabase.Instance.SetScore(currentRound,match,TeamId,editScore.GetScore());
        }
    }
    
    
    public void Init(Match match)
    {
        this.match = match;
        foreach(UIMatchTeamNameSlot nameSlot in TeamNamesSlots)
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
