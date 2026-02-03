using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineTimer : MonoBehaviour
{
    public float totalDuration = 5f;

    void Start()
    {
        Player.STOP = true; // stop player immediately
        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(totalDuration);
        Player.STOP = false; // resume player
    }
}
