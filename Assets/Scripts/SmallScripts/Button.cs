using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Quit"))
            Application.Quit();

        if (Input.GetButtonDown("Interact"))
            SceneManager.LoadScene("Cutscene");
    }

    public void OnButtonPress()
    {
        if (isStart)
        {
            SceneManager.LoadScene("Cutscene");
        }

        if (isQuit)
        {
            Application.Quit();
        }
    }
}
