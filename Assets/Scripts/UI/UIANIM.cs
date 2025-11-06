using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIANIM : MonoBehaviour
{
    public Image targetImage;      // UI Image to change
    public Sprite[] sprites;       // Array of sprites to flip through
    public float flipInterval = 0.5f; // Time between frames in seconds

    private int currentIndex = 0;
    private float timer = 0f;

    void Update()
    {
        if (sprites.Length == 0 || targetImage == null)
            return;

        // Timer counts up
        timer += Time.deltaTime;

        // If timer exceeds interval, flip to next sprite
        if (timer >= flipInterval)
        {
            timer = 0f; // Reset timer
            currentIndex++; // Next sprite

            // Loop back to first sprite if at end
            if (currentIndex >= sprites.Length)
                currentIndex = 0;

            // Update the UI image
            targetImage.sprite = sprites[currentIndex];
        }
    }
}
