using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.Common.Extensions;
using JetBrains.Annotations;
using UnityEngine;

public class TeamDatabase : MonoBehaviour
{
    private static TeamDatabase _instance;
    
    [SerializeField]
    List<Team> ManualTeams = new List<Team>(PlayerConst.MaxPlayer);
    
    
    public static TeamDatabase Instance
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
    

    private Dictionary<int, Team> Teams = new Dictionary<int, Team>();



    private void LoadManualTeams()
    {
        for (int i = 0; i < ManualTeams.Count; i++)
        {
            Teams.Add(i,ManualTeams[i]);
        }
    }

    private void Start()
    {
        LoadManualTeams();
    }

    public Team GetTeamById(int TeamId)
    {
        if (Teams.ContainsKey(TeamId))
        {
            return Teams[TeamId];
        }

        return Team.EmptyTeam;
    }
    
}