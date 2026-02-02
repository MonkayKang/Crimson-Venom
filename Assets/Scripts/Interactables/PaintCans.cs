using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintCans : MonoBehaviour
{
    public int PaintColour; // 0=Red, 1=Blue, 2=Green, 3=Yellow, 4=Purple, 5=Orange

    private bool inRange;


    // Update is called once per frame
    void Update()
    {
        if (inRange && Input.GetButtonDown("Interact"))
        {
            if (PaintColour == 0) { 
            }
                Drawing.hasRed = true;

            if (PaintColour == 1)
                Drawing.hasBlue = true;

            if (PaintColour == 2)
                Drawing.hasGreen= true;

            if (PaintColour == 3)
                Drawing.hasYellow = true;

            if (PaintColour == 4)
                Drawing.hasPurple = true;

            if (PaintColour == 5)
            {
                Drawing.hasOrange = true;
                // FindObjectOfType<BlackScreen>().ShowBlackScreen(); // Start the fade
            }
                

            Drawing.colourCount += 1;
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRange = false;
        }
    }
}
