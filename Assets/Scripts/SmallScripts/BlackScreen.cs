using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackScreen : MonoBehaviour
{
    public Image blackImage; // Assign your Image
    public float fadeDuration = 1f;
    public float visibleDuration = 2f;

    public void ShowBlackScreen()
    {
        StartCoroutine(FadeInOut());
    }

    public void ResumeTime()
    {
        Player.STOP = false; // Resume
    }

    IEnumerator FadeInOut()
    {
        Color c = blackImage.color;

        // Fade in
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration);
            blackImage.color = c;
            yield return null;
        }
        c.a = 1f;
        blackImage.color = c;

        Player.STOP = true; // Player cant do anything

        // Wait
        yield return new WaitForSeconds(visibleDuration);

        // Fade out
        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(1 - (elapsed / fadeDuration));
            blackImage.color = c;
            yield return null;
        }
        c.a = 0f;
        blackImage.color = c;
    }
}