using System;
using System.Collections;
using System.Collections.Generic;
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
    
    private void LoadMockupPlayerNames()
    {
        for (int i = 0; i < PlayerConst.MaxPlayer; i++)
        {
            PlayersNames.Add(i,$"Player_{i}");
        }
    }

    private void Start()
    {
        LoadMockupPlayerNames();
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
