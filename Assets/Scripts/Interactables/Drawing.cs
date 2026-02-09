using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawing : MonoBehaviour
{
    // All the objects
    public GameObject RedCircle;
    public GameObject BlueCircle;
    public GameObject GreenCircle;
    public GameObject YellowCircle;
    public GameObject PurpleCircle;
    public GameObject OrangeCircle;

    // All of its meshes
    private Renderer redMAT;
    private Renderer greenMAT;
    private Renderer yellowMAT;
    private Renderer purpleMAT;
    private Renderer orangeMAT;
    private Renderer blueMAT;

    // Bool
    private bool inRange; 

    public static bool hasRed;
    public static bool hasBlue;
    public static bool hasGreen;
    public static bool hasYellow;
    public static bool hasPurple;
    public static bool hasOrange;

    // Int
    public static int colourCount;

    // Game object
    public GameObject LoadObject; // The object to be loaded
    public GameObject UnloadObject; // The object to be unloaded

    public GameObject Timeline;

    public AudioSource source;
    public AudioClip clip;


    // Start is called before the first frame update
    void Start()
    {
        redMAT = RedCircle.GetComponent<Renderer>();
        redMAT.material.color = Color.white;

        blueMAT = BlueCircle.GetComponent<Renderer>();
        blueMAT.material.color = Color.white;

        yellowMAT = YellowCircle.GetComponent<Renderer>();
        yellowMAT.material.color = Color.white;

        purpleMAT = PurpleCircle.GetComponent<Renderer>();
        purpleMAT.material.color = Color.white;

        orangeMAT = OrangeCircle.GetComponent<Renderer>();
        orangeMAT.material.color = Color.white;

        greenMAT = GreenCircle.GetComponent<Renderer>();
        greenMAT.material.color = Color.white;

        LoadObject.SetActive(false);
        Timeline.SetActive(false);
        colourCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (colourCount >= 5)
        {
            LoadObject.SetActive(true);
            UnloadObject.SetActive(false);
        }
        
        if (colourCount >= 6)
            Timeline.SetActive(true);

        if (inRange && Input.GetButtonDown("Interact"))
        {
            if (hasRed)
            {
                redMAT.material.color = Color.red;
                colourCount += 1;
                hasRed = false;
            }
                

            if (hasBlue)
            {
                blueMAT.material.color = Color.blue;
                colourCount += 1;
                hasBlue = false;
            }
                

            if (hasGreen)
            {
                greenMAT.material.color = Color.green;
                colourCount += 1;
                hasGreen = false;
            }
                

            if (hasOrange)
            {
                orangeMAT.material.color = new Color(1f, 0.5f, 0f);
                colourCount += 1;
                hasOrange = false;
            }
                

            if (hasPurple)
            {
                purpleMAT.material.color = new Color(0.5f, 0f, 0.5f);
                colourCount += 1;
                hasPurple = false;
            }
                

            if (hasYellow)
            {
                yellowMAT.material.color = Color.yellow;
                colourCount += 1;
                hasYellow = false;
            }

            source.PlayOneShot(clip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("player"))
        {
            inRange = false;
        }
    }
}
