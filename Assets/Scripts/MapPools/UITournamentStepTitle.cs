using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITournamentStepTitle : MonoBehaviour
{
    

    private TMP_Text TextComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        TextComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TextComponent)
        {
            TextComponent.text = UIMapPoolUIManager.Instance.GetStepName();
        }
    }
}
