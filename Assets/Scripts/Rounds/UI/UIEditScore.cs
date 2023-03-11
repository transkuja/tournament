
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIEditScore : MonoBehaviour
{
    [SerializeField] private TMP_InputField InputField;

    [FormerlySerializedAs("PlayerSlot")] [SerializeField] private TeamSlot TeamSlot;

    private Match match;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int score)
    {
        InputField.text = score.ToString();
    }

    public int GetScore()
    {
        int value;
        if (int.TryParse(InputField.text,out value))
        {
            return value;
        }

        return 0;
    }

    public TeamSlot GetTeamSlot()
    {
        return TeamSlot;
    }

    public void SetMatch(Match newMatch)
    {
        match = newMatch;
    }

    public void LoadScoreFromDatabase()
    {
        Round round = OCG2DetailsDatabase.Instance.GetCurrentRound();
        int score = TeamScoreDatabase.Instance.GetCurrentRoundTeamSlotScore(round, match, TeamSlot);
        SetScore(score);
    }
}
