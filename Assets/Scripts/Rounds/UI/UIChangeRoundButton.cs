using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIChangeRoundButton : MonoBehaviour
{
    public enum UIChangeButtonType
    {
        NextRound,
        PreviousRound
    }

    [SerializeField] private UIChangeButtonType ChangeType;


    public void ChangeRound()
    {
        switch (ChangeType)
        {
            case UIChangeButtonType.NextRound:
                OCG2DetailsDatabase.Instance.ChangeToNextRound();
                break;
            case UIChangeButtonType.PreviousRound:
                OCG2DetailsDatabase.Instance.ChangeToPreviousRound();
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
