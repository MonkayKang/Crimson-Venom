using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuslingUI : MonoBehaviour
{
    public KeyCode keyPress; // What Key is this For (Esc, E, F)
    public Image UIMove; // The UI that will move

    private bool isActive; // Activation 

    public float pulseSpeed = 2f; // How fast
    public float pulseAmount = 0.1f; // How much difference
    public Color pulseColor = Color.red; // Color of the Image

    private Vector3 originalScale; // The original size
    private Color originalColor;


    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        originalScale = UIMove.rectTransform.localScale; // Save the original size
        originalColor = UIMove.color; // Save the original colour 
    }

    // Update is called once per frame
    void Update()
    {
       if (isActive) // While it is activated, make the specific UI pulse (Make sure to place this script with the UI Trigger)
       {
            float scale = 1f + Mathf.PingPong(Time.time * pulseSpeed, pulseAmount); // Go back and forth from large to original size
            UIMove.rectTransform.localScale = originalScale * scale; // Move the UI by times the "scale" of the ping pong value

            // Color pulse
            float t = Mathf.PingPong(Time.time * pulseSpeed, 1f); // Ping pong value
            UIMove.color = Color.Lerp(originalColor, pulseColor, t); // Change the colour with the value of "t"

            if (Input.GetKeyDown(keyPress)) // Once pressed the desired key
            {
                isActive = false; // turn it off
            }
       }
       else
       {
            UIMove.rectTransform.localScale = originalScale; // Go back to original size
            UIMove.color = originalColor; // Go back to original colour
        }
    }

    public void OnTriggerEnter(Collider other) // When it collides with the player 
    {
        if (other.gameObject.CompareTag("player"))
        {
            isActive = true; // Its true
        }
    }
}
