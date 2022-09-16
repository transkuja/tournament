using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScoreBoardEntry : MonoBehaviour
{
    private PlayerScoreboardEntry ScoreboardEntry;


    [SerializeField] private TextMeshProUGUI RankText;
    [SerializeField] private TextMeshProUGUI PlayerNameText;
    [SerializeField] private TextMeshProUGUI PointsText;
    [SerializeField] private TextMeshProUGUI RoundText;
    [SerializeField] private TextMeshProUGUI DiffText;

    public void SetScoreboardEntry(PlayerScoreboardEntry entry, int rank)
    {
        ScoreboardEntry = entry;
        RankText.text = rank.ToString();
        string playerName = PlayerNameDatabase.Instance.GetPlayerName(entry.PlayerId);
        PlayerNameText.text = playerName;
        PointsText.text = entry.Points.ToString();
        RoundText.text = entry.Rounds.ToString();
        DiffText.text = entry.Diff.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
