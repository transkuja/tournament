using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRoundTitle : MonoBehaviour
{
    [SerializeField]  protected TextMeshProUGUI ScoreText;

    // Update is called once per frame
    void Update()
    {
        Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
        string roundText;
        switch (currentRound)
        {
            case Round.Round1:
                roundText = "Round 1";
                break;
            case Round.Round2:
                roundText = "Round 2";
                break;
            case Round.Round3:
                roundText = "Round 3";
                break;
            default:
                roundText = "Round ?";
                break;
        }

        ScoreText.text = roundText;
    }
}
