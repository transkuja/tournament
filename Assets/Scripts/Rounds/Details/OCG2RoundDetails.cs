using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OCG2RoundDetails
{
    private Dictionary<Match, OCG2MatchDetails> MatchDetails = new Dictionary<Match, OCG2MatchDetails>();
    
    
    public void SetTeamInSlot(Match match, TeamSlot slot, int TeamId)
    {
        if (!MatchDetails.ContainsKey(match))
        {
            OCG2MatchDetails newDetails = new OCG2MatchDetails();
            newDetails.SetTeamInSlot(slot,TeamId);
            MatchDetails.Add(match,newDetails);
        }
        else
        {
            MatchDetails[match].SetTeamInSlot(slot,TeamId);
        }
    }
    
    

    public int GetTeamIdInSlot(Match match, TeamSlot playerSlot)
    {
        if (MatchDetails.ContainsKey(match))
        {
            return MatchDetails[match].GetTeamInSlot(playerSlot);
        }

        return -1;
    }

    public List<int> GetTeamsInMatch(Match match)
    {
        if (MatchDetails.ContainsKey(match))
        {
            return MatchDetails[match].GetTeamsInMatch();
        }

        return new List<int>();
    }
    
    public bool HasTeamInMatch(Match match, int TeamId)
    {
        if (MatchDetails.ContainsKey(match))
        {
            if (MatchDetails[match].GetTeamsInMatch().Contains(TeamId))
            {
                return true;
            }
        }

        return false;
    }
    


    public void SetTeamsInSlot(List<int> teamIds)
    {
        List<Match> UsedMatch = new List<Match>();
        List<KeyValuePair<int,int>> TeamPairs = new List<KeyValuePair<int, int>>();
        //Priorisation de la salle ciné
        for(int i = 0;i < teamIds.Count-1;i+=2)
        {
            TeamPairs.Add(new KeyValuePair<int, int>(teamIds[i],teamIds[i+1]));
        }
        
        //Cas mika
        KeyValuePair<int, int> MikaPair = new KeyValuePair<int, int>(-1,-1);
        foreach (var teamPair in TeamPairs)
        {
            if(teamPair.Key == 7 || teamPair.Value == 7)
            {
                MikaPair = teamPair;
                Match matchToUse = MatchUtils.FindMatchInRoom(UsedMatch,Room.CinemaRoom);
                UsedMatch.Add(matchToUse);
                SetTeamInSlot(matchToUse,TeamSlot.Team1,MikaPair.Key);
                SetTeamInSlot(matchToUse,TeamSlot.Team2,MikaPair.Value);
                break;
            }
        }
        
        TeamPairs.Remove(MikaPair);

        foreach (var teamPair in TeamPairs)
        {
            Match matchToUse;
            int team1Id = teamPair.Key;
            int team2id = teamPair.Value;
            
            int Team1Count = OCG2DetailsDatabase.Instance.GetTeamPlayedInRoomCount(team1Id, Room.CinemaRoom);
            int Team2Count = OCG2DetailsDatabase.Instance.GetTeamPlayedInRoomCount(team2id, Room.CinemaRoom);
            Debug.Log($"Team {team1Id} : {Team1Count} | Team {team2id} : {Team2Count}");
            //if TeamPair has not match in cinema room search for a match in cinema room that can be used
            if (Team1Count <= 0 ||
                Team2Count <= 0)
            {
                matchToUse = MatchUtils.FindMatchInRoom(UsedMatch,Room.CinemaRoom);
            }
            else
            {
                matchToUse = MatchUtils.FindMatchInRoom(UsedMatch,Room.GamingRoom);
            }
            
            UsedMatch.Add(matchToUse);
            SetTeamInSlot(matchToUse,TeamSlot.Team1,team1Id);
            SetTeamInSlot(matchToUse,TeamSlot.Team2,team2id);
        }
        


        
       /* for (int i = 0; i < teamIds.Count; i++)
        {
            Match match = (Match)(i / MatchUtils.TeamCountInMatch);
            TeamSlot slot = (TeamSlot)(i % MatchUtils.TeamCountInMatch);
            SetTeamInSlot(match,slot,teamIds[i]);
        }*/
    }

    public static OCG2RoundDetails GenerateDefault()
    {
        OCG2RoundDetails roundDetails = new OCG2RoundDetails();
        List<int> TeamIds = new List<int>();
        for(int i = 0;i < TeamConst.TeamCount;i++)
        {
            TeamIds.Add(i);
        }

        //Shuffle 
       // Random random = new Random();
        // PlayerIds.OrderBy(item => random.Next());

        roundDetails.SetTeamsInSlot(TeamIds);

        return roundDetails;
    }

    public static OCG2RoundDetails GenerateByCurrentRanking()
    {
        OCG2RoundDetails roundDetails = new OCG2RoundDetails();
        
        List<TeamScoreboardEntry> entries = TeamScoreDatabase.Instance.GenerateScoreBoardEntries();

        List<int> TeamIds = entries.Select(entry => entry.TeamId).ToList();
        roundDetails.SetTeamsInSlot(TeamIds);

        return roundDetails;
    }
}
