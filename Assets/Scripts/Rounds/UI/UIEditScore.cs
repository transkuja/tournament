using System.Collections;
using System.Collections.Generic;
using Doozy.Editor.EditorUI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class UIEditScore : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;

    [SerializeField] private PlayerSlot PlayerSlot;

    private Match match;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        InputField.text = score.ToString();
    }

    public int GetScore()
    {
        int value;
        if (int.TryParse(InputField.text,out value))
        {
            return value;
        }

        return 0;
    }

    public PlayerSlot GetPlayerSlot()
    {
        return PlayerSlot;
    }

    public void SetMatch(Match newMatch)
    {
        match = newMatch;
    }

    public void LoadScoreFromDatabase()
    {
        Round round = DetailsDatabase.Instance.GetCurrentRound();
        int score = PlayerScoreDatabase.Instance.GetCurrentRoundPlayerSlotScore(round, match, PlayerSlot);
        SetScore(score);
    }
}
