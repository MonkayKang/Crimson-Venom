using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoveringUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject scrollview;
    public Image hoverImage;

    public Sprite image;
    public TextMeshProUGUI text1;

    public string String = "";

    private static bool isOpen = false;

    public void Start()
    {
        isOpen = false;
        hoverImage.enabled = false;
        scrollview.SetActive(false);
        text1.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isOpen = !isOpen;

        hoverImage.enabled = isOpen;
        scrollview.SetActive(isOpen);
        text1.enabled = isOpen;

        if (isOpen)
        {
            hoverImage.sprite = image;
            text1.text = String;
        }
        else
        {
            text1.text = "";
        }
    }

    private IEnumerator WaitSEC()
    {
        isOpen = false;
        yield return new WaitForSeconds(0.1f);
        isOpen = true;
    }
}