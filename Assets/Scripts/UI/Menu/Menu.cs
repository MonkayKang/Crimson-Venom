using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public Animator MenuAnimation;
    public Animator ConstructionAnimation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        bool isOn = MenuAnimation.GetBool("On");
        MenuAnimation.SetBool("On", !isOn);
        Player.STOP = (!isOn);

        if (!isOn)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the mouse
            Cursor.visible = true; // Make it visible
        }

        if (isOn)
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the mouse
            Cursor.visible = false; // Make it invisible
            ConstructionAnimation.SetBool("On", false);
        } 
    }

    public void SecondTab()
    {
        bool isOn = ConstructionAnimation.GetBool("On");
        ConstructionAnimation.SetBool("On", !isOn);
    }
}
