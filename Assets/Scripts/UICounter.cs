using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    public TextMeshProUGUI SCOREtext1; // First Minigame text
    public TextMeshProUGUI collectableUI;
    public static int miniSCORE1;
    public static int collectablesCount;

    public static bool animStart = false;

    private bool isCoroutineRunning = false; // prevent multiple coroutines

    void Start()
    {
        miniSCORE1 = 0;
        collectablesCount = 0; // Prevents stacking
        collectableUI.enabled = false;
    }

    void Update()
    {
        SCOREtext1.text = "Score: " + miniSCORE1.ToString();
        collectableUI.text = "Items Collected: " + collectablesCount.ToString();

        if (animStart && !isCoroutineRunning)
        {
            StartCoroutine(CollectableUI());
        }
    }

    private IEnumerator CollectableUI()
    {
        isCoroutineRunning = true; // Coroutine is running
        collectableUI.enabled = true; // Show the UI
        yield return new WaitForSeconds(3f); // wait 3 seconds
        collectableUI.enabled = false; // Remove the UI
        animStart = false; // reset trigger
        isCoroutineRunning = false; // Corutine is over
    }
}
