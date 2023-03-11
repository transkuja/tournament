using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMatchGroup : MonoBehaviour
{

    [SerializeField] private Match match;

    [SerializeField] private bool HideWhenMatchScore;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Round currentRound = OCG2DetailsDatabase.Instance.GetCurrentRound();
        bool HasMatchEntry = TeamScoreDatabase.Instance.HasMatchEntry(currentRound, match);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive((HideWhenMatchScore && !HasMatchEntry) || (!HideWhenMatchScore && HasMatchEntry));
        }
    }
}
