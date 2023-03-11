using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMatchTeamNameScoreSlot : UIMatchTeamNameSlot
{

    [SerializeField]  protected TextMeshProUGUI ScoreText;
    [SerializeField] protected Image ScoreOverlay;

    [Header("Colors")] 
    [SerializeField] protected Color WinnerColor;
    [SerializeField] protected Color NormalColor;
    
    

    void UpdateScore()
    {
        int TeamId = GetTeamId();
        if (TeamId >= 0)
        {
            Round CurrentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
            int score = TeamScoreDatabase.Instance.GetGameScore(CurrentRound, Match, TeamId);
            ScoreText.SetText(score.ToString());
        }
    }

    void UpdateScoreOverlay()
    {
        Round CurrentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
        ScoreOverlay.color = TeamScoreDatabase.Instance.GetMatchWinner(CurrentRound, Match) == GetTeamId()
            ? WinnerColor
            : NormalColor;
    }

    protected override void UpdateInfo()
    {
        base.UpdateInfo();
        UpdateScore();
        UpdateScoreOverlay();
    }

    public override bool HasToBeVisible()
    {
        Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
        return TeamScoreDatabase.Instance.HasMatchEntry(currentRound, Match);
    }
}
