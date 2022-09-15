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
    

    private Dictionary<Round, RoundDetails> RoundDetails = new Dictionary<Round, RoundDetails>();

    private Round CurrentRound;


    public void ChangeCurrentRound(Round newRound)
    {
        CurrentRound = newRound;
    }


    public void GenerateRound1()
    {
        RoundDetails round1Details = new RoundDetails();
        List<int> PlayerIds = new List<int>();
        for(int i = 0;i < PlayerConst.MaxPlayer;i++)
        {
            PlayerIds.Add(i);
        }

        //Shuffle 
        Random random = new Random();
       // PlayerIds.OrderBy(item => random.Next());

       for (int i = 0; i < PlayerIds.Count; i++)
       {
           Match match = (Match)(i / 4);
           PlayerSlot slot = (PlayerSlot)(i % 4);
           round1Details.SetPlayerInSlot(match,slot,PlayerIds[i]);
       }
       
        
        RoundDetails.Add(Round.Round1,round1Details);
    }

    void GenerateRound2()
    {
        RoundDetails round2Details = new RoundDetails();
        RoundDetails.Add(Round.Round2,round2Details);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateRound1();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetPlayerInSlot(Round round, Match match, PlayerSlot slot)
    {
        if (RoundDetails.ContainsKey(round))
        {
            return RoundDetails[round].GetPlayerIdInSlot(match, slot);
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
}
