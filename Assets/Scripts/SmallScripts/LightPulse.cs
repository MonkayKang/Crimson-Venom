using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    public Light targetLight;      // Light Source
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float speed = 1f;       // How fast it pulses

    void Update()
    {
        if (Lever.playerLEVER == true) // If player picked up the lever
        {
            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t); // Flash the light
        }
        
    }
}
