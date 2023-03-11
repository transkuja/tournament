using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class Team
{
    public string TeamName;
    public string Player1Name;
    public string Player2Name;
    public Sprite TeamLogo;
    
    public static Team EmptyTeam = new Team() {TeamName = "Empty", Player1Name = "EmptyPlayer1", Player2Name = "EmpyPlayer2"};
}
