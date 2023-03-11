using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIScoreBoardEntry : MonoBehaviour
{
    private TeamScoreboardEntry ScoreboardEntry;

    private const int WinnerCount = 6;


    [SerializeField] private TextMeshProUGUI RankText;
    [FormerlySerializedAs("PlayerNameText")] [SerializeField] private TextMeshProUGUI TeamNameText;
    [SerializeField] private TextMeshProUGUI RoundText;
    [SerializeField] private TextMeshProUGUI WinsText;
    [SerializeField] private TextMeshProUGUI LoosesText;
    [SerializeField] private TextMeshProUGUI DiffText;
    [SerializeField] private TextMeshProUGUI TeamPlayersText;
    [SerializeField] private Image TeamIconImage;
    
    [Header("Overlay")]
    [SerializeField] private List<Image> overlaydImages = new List<Image>();

    [SerializeField] private Color WinnerColor;
    [SerializeField] private Color NormalColor;

    public void SetScoreboardEntry(TeamScoreboardEntry entry, int rank)
    {
        ScoreboardEntry = entry;
        RankText.text = rank.ToString();
        Team team = TeamDatabase.Instance.GetTeamById(entry.TeamId);
        string teamName = team.TeamName;

        TeamNameText.text = teamName;
        string teamPlayersText = $"{team.Player1Name} - {team.Player2Name}";
        TeamPlayersText.text = teamPlayersText;
        //PointsText.text = entry.Points.ToString();
        RoundText.text = entry.Rounds.ToString();
        DiffText.text = entry.Diff.ToString();
        WinsText.text = entry.Wins.ToString();
        LoosesText.text = entry.Looses.ToString();

        if (team.TeamLogo != null)
        {
            TeamIconImage.sprite = team.TeamLogo;
        }

        foreach (Image overlayImage in overlaydImages)
        {
            overlayImage.color = rank <= WinnerCount ? WinnerColor : NormalColor;
        }


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
