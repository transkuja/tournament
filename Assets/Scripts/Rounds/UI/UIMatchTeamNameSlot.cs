using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIMatchTeamNameSlot : MonoBehaviour
{

    [SerializeField]
    protected  Match Match;

    [FormerlySerializedAs("PlayerSlot")] [SerializeField] 
    protected  TeamSlot TeamSlot;

    [FormerlySerializedAs("PlayerNameText")] [SerializeField]
    protected TextMeshProUGUI TeamNameText;
    
    [SerializeField]
    protected Image TeamIconImage;
    
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    protected virtual void UpdateInfo()
    {
        UpdateTeamName();
        UpdateTeamIcon();
    }

    protected int GetTeamId()
    {
        return OCG2DetailsDatabase.Instance.GetTeamInSlotCurrentround(Match, TeamSlot);
    }

    protected void UpdateTeamName()
    {
        int TeamId = GetTeamId();
        if (TeamId >= 0)
        {
            Team team = TeamDatabase.Instance.GetTeamById(TeamId);
            string teamName = team.TeamName;
            TeamNameText.SetText(teamName);
        }
        else
        {
            TeamNameText.SetText("");
        }
    }

    protected void UpdateTeamIcon()
    {
        int TeamId = GetTeamId();
        if (TeamId >= 0)
        {
            Team team = TeamDatabase.Instance.GetTeamById(TeamId);
            string teamName = team.TeamName;
            if (team.TeamLogo != null)
            {
                TeamIconImage.sprite = team.TeamLogo;
            }

        }
    }

    public void SetMatch(Match newMatch)
    {
        Match = newMatch;
    }

    public virtual bool HasToBeVisible()
    {
        Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
        return !TeamScoreDatabase.Instance.HasMatchEntry(currentRound, Match);
    }
    
    
}
