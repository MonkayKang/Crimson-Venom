using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TextMeshProUGUI TaskText;
    public float delay = 0.05f;

    public static bool hasKey;
    public string words;

    void Start()
    {
        hasKey = false;
        words = "Objective: Find a way to enter the carnival";
        StartCoroutine(TypeText(words));
    }

    public void SetObjective(string newText)
    {
        words = newText;
        StopAllCoroutines();
        StartCoroutine(TypeText(words));
    }

    private IEnumerator TypeText(string fullText)
    {
        TaskText.text = "";

        foreach (char c in fullText)
        {
            TaskText.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}

