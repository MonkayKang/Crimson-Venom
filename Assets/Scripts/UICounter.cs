using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICounter : MonoBehaviour
{
    public TextMeshProUGUI SCOREtext1; // First Minigame text
    public static int miniSCORE1;
    // Start is called before the first frame update
    void Start()
    {
        miniSCORE1 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SCOREtext1.text = "Score: " + miniSCORE1.ToString(); // Turn the first string plus the score
    }
}
