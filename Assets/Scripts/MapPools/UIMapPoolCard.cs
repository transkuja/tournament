using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMapPoolCard : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private MapDetails mapDetails;

    [SerializeField] private string Title = "Round X";
    
    [Header("Bindings")]
    [SerializeField] private Image MapImageComponent;

    [SerializeField] private Image MapIconComponent;

    [SerializeField] private TextMeshProUGUI MapNameTextComponent;

    [SerializeField] private TextMeshProUGUI CardTitleTextComponent;

    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        if (mapDetails)
        {
            MapImageComponent.sprite = mapDetails.MapImage;
            MapIconComponent.sprite = mapDetails.MapIcon;
            MapNameTextComponent.text = mapDetails.MapName;

        }
        
        CardTitleTextComponent.text = Title;
    }

    public void DisplayCard()
    {
        animator.SetBool("clicked", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
