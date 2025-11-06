using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
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
        
    }

    public void OnButtonPress()
    {
        if (isStart)
        {
            SceneManager.LoadScene("Level");
        }

        if (isQuit)
        {
            Application.Quit();
        }
    }
}
