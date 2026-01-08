using System.Collections;
using UnityEngine;

public class LightPulse : MonoBehaviour
{
    public Light targetLight;

    public static bool Powered; // Are the lights on

    // For Pulse lights
    public float minIntensity = 1f;
    public float maxIntensity = 2f;
    public float speed = 1f;

    // Flickering Lights
    public float fullIntensity = 1f;
    public float minFlickerIntensity = 0.1f;

    public bool backgroundFlicker = true; // To seperate the two codes
    public float minTimeBetweenFlickers = 4f; // How quick (MIN) for next flicker
    public float maxTimeBetweenFlickers = 10f; // How long for the next flicker

    private bool isFlickering; // Tells the code that this light is flickering
    private Coroutine flickerRoutine; // start the courtine for flicker code

    void Start()
    {
        targetLight.intensity = fullIntensity;

        Powered = false; // Start off as false

        if (backgroundFlicker)
            flickerRoutine = StartCoroutine(FlickerController()); // Start the flicker
    }

    void Update()
    {
        // Lever overrides flickering
        if (Lever.playerLEVER)
        {
            isFlickering = false;

            float t = (Mathf.Sin(Time.time * speed) + 1f) / 2f;
            targetLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, t);
        }
        else if (!isFlickering)
        {
            targetLight.intensity = fullIntensity;
        }

        // Stop flickering permanently once powered
        if (Powered && flickerRoutine != null)
        {
            StopCoroutine(flickerRoutine);
            flickerRoutine = null;
            isFlickering = false;
            targetLight.intensity = fullIntensity;
        }
    }

    IEnumerator FlickerController()
    {
        while (true)
        {
            // Do nothing if powered
            if (Powered)
                yield break;

            float wait = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers); // The wait time is a range between the min and max
            yield return new WaitForSeconds(wait); // Wait for that long before continuing

            isFlickering = true; // Light is flickering

            int pattern = Random.Range(0, 2);
            switch (pattern)
            {
                case 0:
                    yield return StartCoroutine(QuickStutter());
                    break;

                case 1:
                    yield return StartCoroutine(DoubleBlink());
                    break;
            }

            isFlickering = false;
            targetLight.intensity = fullIntensity;
        }
    }

    IEnumerator QuickStutter()
    {
        int count = Random.Range(3, 6);

        for (int i = 0; i < count; i++)
        {
            targetLight.intensity = Random.Range(minFlickerIntensity, fullIntensity * 0.6f);
            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            targetLight.intensity = fullIntensity;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.2f));
        }
    }

    IEnumerator DoubleBlink()
    {
        for (int i = 0; i < 2; i++)
        {
            targetLight.intensity = minFlickerIntensity;
            yield return new WaitForSeconds(0.15f);

            targetLight.intensity = fullIntensity;
            yield return new WaitForSeconds(0.25f);
        }
    }
}
