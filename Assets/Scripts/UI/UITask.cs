using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public CanvasGroup canvasGroup;

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
        canvasGroup.alpha = 1f; // Fully visible
        TaskText.text = "";

        foreach (char c in fullText)
        {
            TaskText.text += c;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(2f);

        float fadeTime = 1f;
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeTime);
            yield return null;
        }
    }
}

