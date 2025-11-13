using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoveringUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool firstSLOT;
    public bool secondSLOT;

    public GameObject hoverImage;
    public TextMeshProUGUI text1;

    public void Start()
    {
        hoverImage.SetActive(false);
    }

    // When mouse enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverImage.SetActive(true);

        if (firstSLOT)
        {
            text1.text = "“Hey Newbie! If you’re on night duty and need to get the generator going, just punch in the circus founding year. That’s the code. Don’t worry, you’ll get the hang of this place soon.”\n \n -From one tech to another";
        }
        else if (secondSLOT)
        {
            text1.text = "A Blueprint of Twin A";
        }
    }

    // When mouse exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        hoverImage.SetActive(false);
        text1.text = ""; // optional — clears the text when you leave
    }
}