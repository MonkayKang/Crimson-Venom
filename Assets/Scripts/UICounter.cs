using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    public TextMeshProUGUI SCOREtext1; // First Minigame text
    public TextMeshProUGUI collectableUI;
    public TextMeshProUGUI TaskUI;

    private string words;
    public static int miniSCORE1;
    public static int collectablesCount;

    public GameObject[] obj;
    public static bool animStart = false;
    public static int taskCounter = 0;
    public float delay = 0.05f; // Delay between each character

    // TASK PREVENTION LOOP
    private bool task1On = false; // Stops loops
    private bool task2On = false;
    private bool task3On = false; // Final task, ADJUST FOR FUTURE TASK

    private bool isCoroutineRunning = false; // prevent multiple coroutines


    void Start()
    {
        miniSCORE1 = 0;
        collectablesCount = 0; // Prevents stacking
        collectableUI.enabled = false;
        words = "Task: Grab a Flashlight and Explore the Carnival";

        // Type out text at the very beginning
        StartCoroutine(TypeText(words, TaskUI));
    }

    void Update()
    {
        SCOREtext1.text = "Score: " + miniSCORE1.ToString();
        collectableUI.text = "Items Collected: " + collectablesCount.ToString();

        if (animStart && !isCoroutineRunning)
        {
            StartCoroutine(CollectableUI());
        }

        if (miniSCORE1 >= 15)
        {
            if (!task3On)
            {
                taskCounter++; // Final task ADJUST HERE
            }

            for (int i = 0; i < obj.Length; i++) // For every object in that
            {
                if (obj[i] != null)
                {
                    Destroy(obj[i]);
                    obj[i] = null; // clears the reference
                }
            }
        }

        if (taskCounter >= 1 && !task1On)
        {
            TaskUI.enabled = false; // Turn off the current task
            words = "Task: Find and Cut the Ropes (RED AND YELLOW)"; // New task is assigned
            StartCoroutine(Task());
            task1On = true;
        }

        if (taskCounter >= 2 && !task2On)
        {
            TaskUI.enabled = false; // Turn off the current task
            words = "Task: They are now free....    Complete 'Balloon POP'"; // New task is assigned
            StartCoroutine(Task());
            task2On = true;
        }

        if (taskCounter >= 3 && !task3On)
        {
            TaskUI.enabled = false; // Turn off the current task
            words = "Task: ESCAPE!"; // New task is assigned
            StartCoroutine(Task());
            task3On = true;
        }
    }

    private IEnumerator CollectableUI()
    {
        isCoroutineRunning = true; // Coroutine is running
        collectableUI.enabled = true; // Show the UI
        yield return new WaitForSeconds(3f); // wait 3 seconds
        collectableUI.enabled = false; // Remove the UI
        animStart = false; // reset trigger
        isCoroutineRunning = false; // Coroutine is over
    }

    private IEnumerator Task() // Wait to turn the task back on
    {
        yield return new WaitForSeconds(1f); // Wait one sec
        // Type out text for new task
        TaskUI.enabled = true;
        StartCoroutine(TypeText(words, TaskUI)); // Go to next countine
    }

    private IEnumerator TypeText(string fullText, TextMeshProUGUI uiText) // Start typing the words
    {
        uiText.text = ""; // Clear current text
        foreach (char c in fullText)
        {
            uiText.text += c; // Add one letter
            yield return new WaitForSeconds(delay); // Wait before next letter
        }
    }
}

