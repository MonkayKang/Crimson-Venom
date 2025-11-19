using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public float SwitchTime = 47f; // Length of Video

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= SwitchTime)
        {
            SceneManager.LoadScene("Level");
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("Level");
        }
    }
}
