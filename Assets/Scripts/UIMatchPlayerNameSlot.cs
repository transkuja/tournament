using System.Collections;
using System.Collections.Generic;
using Doozy.Editor.EditorUI;
using TMPro;
using UnityEngine;

public class UIMatchPlayerNameSlot : MonoBehaviour
{

    [SerializeField]
    protected  Match Match;

    [SerializeField] 
    protected  PlayerSlot PlayerSlot;

    [SerializeField]
    protected TextMeshProUGUI PlayerNameText;
    
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
        UpdatePlayerName();
    }

    protected int GetPlayerId()
    {
        return DetailsDatabase.Instance.GetPlayerInSlotCurrentround(Match, PlayerSlot);
    }

    protected void UpdatePlayerName()
    {
        int PlayerId = GetPlayerId();
        if (PlayerId >= 0)
        {
            string playerName = PlayerNameDatabase.Instance.GetPlayerName(PlayerId);
            PlayerNameText.SetText(playerName);
        }
    }

    public void SetMatch(Match newMatch)
    {
        Match = newMatch;
    }

    public virtual bool HasToBeVisible()
    {
        Round currentRound = DetailsDatabase.Instance.GetCurrentRound();
        return !PlayerScoreDatabase.Instance.HasMatchEntry(currentRound, Match);
    }
    
    
}
