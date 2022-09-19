using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapPoolUIManager : MonoBehaviour
{
    [SerializeField] 
    private List<string> StepNames = new List<string>();

    private TournamentStep CurrentStep = TournamentStep.Qualif;

    private static UIMapPoolUIManager _instance = null;
    
    public static UIMapPoolUIManager Instance
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
        }
    }

    void SetStep(TournamentStep newStep)
    {
        CurrentStep = newStep;
    }

    public void SetQualifStep()
    {
        SetStep(TournamentStep.Qualif);
    }
    
    public void SetWinnerBracketStep()
    {
        SetStep(TournamentStep.WinnerBracket);
    }
    
    public void SetLooserBracketStep()
    {
        SetStep(TournamentStep.LoserBracket);
    }
    
    public void SetFinalStep()
    {
        SetStep(TournamentStep.Final);
    }

    public string GetStepName()
    {
        return StepNames[(int)CurrentStep];
    }
}
