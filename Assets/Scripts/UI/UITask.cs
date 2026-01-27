using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI TaskText;
    public float delay = 0.05f; // Delay between letters

    public static bool hasKey;

    private string words;

    void Start()
    {
        hasKey = false; // For replayability
        words = "Objective: Find a way to escape the carnival";
        StartCoroutine(TypeText(words, TaskText));
    }

    private IEnumerator TypeText(string fullText, TextMeshProUGUI uiText)
    {
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}

