using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMatchPlayerNameScoreSlot : UIMatchPlayerNameSlot
{

    [SerializeField]  protected TextMeshProUGUI ScoreText;
    [SerializeField] protected Image ScoreOverlay;

    [Header("Colors")] 
    [SerializeField] protected Color WinnerColor;
    [SerializeField] protected Color NormalColor;
    

    void UpdateScore()
    {
        int PlayerId = GetPlayerId();
        if (PlayerId >= 0)
        {
            Round CurrentRound = DetailsDatabase.Instance.GetCurrentRound();
            int score = PlayerScoreDatabase.Instance.GetGameScore(CurrentRound, Match, PlayerId);
            ScoreText.SetText(score.ToString());
        }
    }

    void UpdateScoreOverlay()
    {
        Round CurrentRound = DetailsDatabase.Instance.GetCurrentRound();
        ScoreOverlay.color = PlayerScoreDatabase.Instance.GetMatchWinner(CurrentRound, Match) == GetPlayerId()
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
        Round currentRound = DetailsDatabase.Instance.GetCurrentRound();
        return PlayerScoreDatabase.Instance.HasMatchEntry(currentRound, Match);
    }
}
