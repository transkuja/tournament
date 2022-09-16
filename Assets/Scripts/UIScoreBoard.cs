using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Common.Extensions;
using UnityEngine;

public class UIScoreBoard : MonoBehaviour
{
    private const int PlayerCountByColumn = 12;

    [SerializeField]
    private GameObject FirstColumn;
    
    [SerializeField]
    private GameObject SecondColumn;

    [SerializeField] private UIScoreBoardEntry ScoreBoardEntryPrefab;

    public void GenerateScoreBoardEntries()
    {
        FirstColumn.transform.DestroyChildren();
        SecondColumn.transform.DestroyChildren();

        int EntryCount = 0;
        
        List<PlayerScoreboardEntry> scoreboardEntries = PlayerScoreDatabase.Instance.GenerateScoreBoardEntries();
        foreach (PlayerScoreboardEntry scoreEntry in scoreboardEntries)
        {
            GameObject ColumnToUse = EntryCount < 12 ? FirstColumn : SecondColumn;
            UIScoreBoardEntry newUIScoreBoardEntry = Instantiate(ScoreBoardEntryPrefab, ColumnToUse.transform);
            newUIScoreBoardEntry.SetScoreboardEntry(scoreEntry,EntryCount+1);
            EntryCount++;
        }
    }
}
