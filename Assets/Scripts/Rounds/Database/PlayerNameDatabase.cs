using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Common.Extensions;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerNameDatabase : MonoBehaviour
{
    private static PlayerNameDatabase _instance;
    
    
    public static PlayerNameDatabase Instance
    {
        get => _instance;
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }
    

    private Dictionary<int, string> PlayersNames = new Dictionary<int, string>();

    [SerializeField] private List<string> ManualPlayerNames = new List<string>(PlayerConst.MaxPlayer);

    private void LoadDefaultPlayerNames()
    {
        for (int i = 0; i < PlayerConst.MaxPlayer; i++)
        {
            PlayersNames.Add(i,$"Player {i+1}");
        }
    }

    private void LoadManualPayerNames()
    {
        for (int i = 0; i < ManualPlayerNames.Count; i++)
        {
            string playerName = ManualPlayerNames[i].Trim();
            if (!playerName.IsNullOrEmpty())
            {
                PlayersNames[i] = playerName;
            }
        }
    }

    private void Start()
    {
        LoadDefaultPlayerNames();
        LoadManualPayerNames();
    }

    public String GetPlayerName(int PlayerId)
    {
        if (PlayersNames.ContainsKey(PlayerId))
        {
            return PlayersNames[PlayerId];
        }

        return $"Player {PlayerId+1}";
    }
}
