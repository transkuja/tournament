using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class DetailsDatabase : MonoBehaviour
{
    private static DetailsDatabase _instance;

    public static DetailsDatabase Instance
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
    

    private Dictionary<Round, RoundDetails> RoundDetailsData = new Dictionary<Round, RoundDetails>();

    private Round CurrentRound;


    public void ChangeCurrentRound(Round newRound)
    {
        CurrentRound = newRound;
    }


    public void GenerateCurrentRound()
    {
        if (!RoundDetailsData.ContainsKey(CurrentRound))
        {
            RoundDetails newRoundDetails = new RoundDetails();
            switch (CurrentRound)
            {
                case Round.Round1:
                    newRoundDetails = RoundDetails.GenerateDefault();
                    break;
                case Round.Round2:
                    newRoundDetails = RoundDetails.GenerateByCurrentRanking();
                    break;
                case Round.Round3:
                    newRoundDetails = RoundDetails.GenerateByCurrentRanking();
                    break;
            }
            
            RoundDetailsData.Add(CurrentRound,newRoundDetails);
        }
        



    }

    void GenerateRound2()
    {
        RoundDetails round2Details = new RoundDetails();
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

    public int GetPlayerInSlot(Round round, Match match, PlayerSlot slot)
    {
        if (RoundDetailsData.ContainsKey(round))
        {
            return RoundDetailsData[round].GetPlayerIdInSlot(match, slot);
        }

        return -1;
    }

    public int GetPlayerInSlotCurrentround(Match match, PlayerSlot slot)
    {
        return GetPlayerInSlot(CurrentRound, match, slot);
    }

    public Round GetCurrentRound()
    {
        return CurrentRound;
    }

    public List<int> GetPlayerInMatch(Round round, Match match)
    {
        if (RoundDetailsData.ContainsKey(round))
        {
            List<int> Players = new List<int>();
            return RoundDetailsData[round].GetPlayersInMatch(match);
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
