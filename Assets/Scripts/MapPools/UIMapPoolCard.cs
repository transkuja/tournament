using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMapPoolCard : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private MapDetails mapDetails1;
    
    [SerializeField] private MapDetails mapDetails2;

    [SerializeField] private string Title = "Round X";
    
    [Header("Bindings")]
    [SerializeField] private Image MapImageComponent;
    
    [SerializeField] private TextMeshProUGUI CardTitleTextComponent;

    [SerializeField] private Animator animator;


    // Start is called before the first frame update
    void Start()
    {

        
        CardTitleTextComponent.text = Title;
    }

    public void DisplayMap1()
    {
        Debug.Log("DisplayMap1");
        DisplayCard(mapDetails1);
    }
    
    public void DisplayMap2()
    {
        Debug.Log("DisplayMap2");
        DisplayCard(mapDetails2);
    }
    
    
    public void DisplayCard(MapDetails DetailsToUse)
    {
        if (DetailsToUse != null)
        {
            MapImageComponent.sprite = DetailsToUse.MapImage;
        }

        animator.SetBool("clicked", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
