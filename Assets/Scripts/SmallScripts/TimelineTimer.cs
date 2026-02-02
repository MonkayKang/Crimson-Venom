using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTimer : MonoBehaviour
{
    public int totalDuration = 5; // Duration in seconds, set in Inspector

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        // Wait for the set duration
        yield return new WaitForSeconds(totalDuration);

        Player.STOP = false;
    }
}
