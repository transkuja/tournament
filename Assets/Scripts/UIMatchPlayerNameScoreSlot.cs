using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMatchPlayerNameScoreSlot : UIMatchPlayerNameSlot
{

    [SerializeField]  protected TextMeshProUGUI ScoreText;

    void UpdateScore()
    {
        int PlayerId = GetPlayerId();
        if (PlayerId >= 0)
        {
            Round CurrentRound = DetailsDatabase.Instance.GetCurrentRound();
            int score = PlayerScoreDatabase.Instance.GetScore(CurrentRound, Match, PlayerId);
            ScoreText.SetText(score.ToString());
        }
    }

}
