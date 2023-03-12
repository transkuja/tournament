using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class OCG2DetailsDatabase : MonoBehaviour
{
    private static OCG2DetailsDatabase _instance;

    public static OCG2DetailsDatabase Instance
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
    

    private Dictionary<Round, OCG2RoundDetails> RoundDetailsData = new Dictionary<Round, OCG2RoundDetails>();

    private Round CurrentRound;


    public void ChangeCurrentRound(Round newRound)
    {
        CurrentRound = newRound;
    }
    
    public int GetTeamPlayedInRoomCount(int TeamId, Room room)
    {
        int count = 0;
        foreach(Round round in Enum.GetValues(typeof(Round)))
        {
            foreach(Match match in Enum.GetValues(typeof(Match)))
            {
                if (GetTeamsInMatch(round, match).Contains(TeamId) && MatchUtils.GetMatchRoom(match) == room)
                {
                    count++;
                }
            }
        }

        return count;

    }


    public void GenerateCurrentRound()
    {
        if (!RoundDetailsData.ContainsKey(CurrentRound))
        {
            OCG2RoundDetails newRoundDetails = new OCG2RoundDetails();
            switch (CurrentRound)
            {
                case Round.Round1:
                    newRoundDetails = OCG2RoundDetails.GenerateDefault();
                    break;
                case Round.Round2:
                    newRoundDetails = OCG2RoundDetails.GenerateByCurrentRanking();
                    break;
                case Round.Round3:
                    newRoundDetails = OCG2RoundDetails.GenerateByCurrentRanking();
                    break;
            }
            
            RoundDetailsData.Add(CurrentRound,newRoundDetails);
        }
        



    }

    void GenerateRound2()
    {
        OCG2RoundDetails round2Details = new OCG2RoundDetails();
        RoundDetailsData.Add(Round.Round2,round2Details);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateCurrentRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetTeamInSlot(Round round, Match match, TeamSlot slot)
    {
        if (RoundDetailsData.ContainsKey(round))
        {
            return RoundDetailsData[round].GetTeamIdInSlot(match, slot);
        }

        return -1;
    }

    public int GetTeamInSlotCurrentround(Match match, TeamSlot slot)
    {
        return GetTeamInSlot(CurrentRound, match, slot);
    }

    public Round GetCurrentRound()
    {
        return CurrentRound;
    }

    public List<int> GetTeamsInMatch(Round round, Match match)
    {
        if (RoundDetailsData.ContainsKey(round))
        {
            List<int> Players = new List<int>();
            return RoundDetailsData[round].GetTeamsInMatch(match);
        }

        return new List<int>();
    }
    
    

    public void ChangeToNextRound()
    {
        if ((int)CurrentRound < Enum.GetValues(typeof(Round)).Length-1)
        {
            CurrentRound++;
            GenerateCurrentRound();
        }
    }

    public void ChangeToPreviousRound()
    {
        if ((int)CurrentRound > 0)
        {
            CurrentRound--;
        }
    }
}
