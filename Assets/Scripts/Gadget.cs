using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Gadget : MonoBehaviour
{
    private Animator _anim;
    private Queue<string> inputQueue = new Queue<string>();
    private bool isPlaying = false;
    public bool isHandheld; // Is this the objects that's picked up or Used


    public GameObject handheld; // What object to turn on

    // Handheld
    public Transform cameraTransform; // assign the FPS camera
    public Vector3 localPosition; // offset in front/right of camera
    public Vector3 localRotation; // optional rotation offset

    // Code Inputs
    public TextMeshProUGUI UI;
    public int codelength = 4;
    public string redCode = "1988";
    private string currentCode = "";
    private bool stopQueue = false; // Boolean to stop typing when enter wrong

    public Renderer[] displayCubes; // 4 cubes that display digits
    public Material[] numberMaterials; // 10 materials (0-9)
    public Material blankMaterial;      // Material for "X"

    public Light pointLight; 
    public Color defaultColor = Color.white; // Go back to this colour
    public Color redCODE = Color.red; // For red doors



    // Pickup Handheld
    private bool nearPlayer;


    void Start()
    {
        _anim = GetComponent<Animator>();
        
        if (handheld != null ) // prevents errors
        {
            handheld.SetActive(false); // Turn the device off
        }
        
        if (pointLight != null) // prevents errors
        {
            pointLight.color = defaultColor;
        }
        
    }

    void Update()
    {
        if (!isHandheld && nearPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                handheld.SetActive(true); // ON
                Destroy(gameObject); // Destroy itself
            }
        }
        if (isHandheld)
        {
            // Detect button presses and enqueue them
            if (Input.GetKeyDown(KeyCode.Alpha1)) inputQueue.Enqueue("button1");
            if (Input.GetKeyDown(KeyCode.Alpha2)) inputQueue.Enqueue("button2");
            if (Input.GetKeyDown(KeyCode.Alpha3)) inputQueue.Enqueue("button3");
            if (Input.GetKeyDown(KeyCode.Alpha4)) inputQueue.Enqueue("button4");
            if (Input.GetKeyDown(KeyCode.Alpha5)) inputQueue.Enqueue("button5");
            if (Input.GetKeyDown(KeyCode.Alpha6)) inputQueue.Enqueue("button6");
            if (Input.GetKeyDown(KeyCode.Alpha7)) inputQueue.Enqueue("button7");
            if (Input.GetKeyDown(KeyCode.Alpha8)) inputQueue.Enqueue("button8");
            if (Input.GetKeyDown(KeyCode.Alpha9)) inputQueue.Enqueue("button9");
            if (Input.GetKeyDown(KeyCode.Alpha0)) inputQueue.Enqueue("button0");
            // Add more buttons as needed

            // Start the animation queue if not already playing
            if (!isPlaying && inputQueue.Count > 0)
            {
                StartCoroutine(PlayQueue());
            }

            // Check numeric keys 0-9
            for (int i = 0; i <= 9; i++)
            {
                if (Input.GetKeyDown(i.ToString())) // Any numbers pressed becomes "i"
                {
                    AddDigit(i); // Add it to the string. EX: xxxx -> xxx1 -> xx19
                }
            }

            if (UI !=  null) // Prevents errors
            {
                UI.text = currentCode.ToString(); // Players know what they typed in
            }
            
        }

    }

    void LateUpdate()
    {
        if (isHandheld)
        {
            // Parent the object to the camera
            transform.SetParent(cameraTransform);

            // Set its local position/rotation
            transform.localPosition = localPosition;
            transform.localRotation = Quaternion.Euler(localRotation);

            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(WrongCode()); // Play the "anim"
            }
        }
    }

    void AddDigit(int digit)
    {
        // Stop if wrong code sequence is active
        if (stopQueue) return;

        // Add digit to current code 
        currentCode += digit.ToString();

        // Update visual display
        UpdateDisplay();

        // If code exceeds max length, reset
        if (currentCode.Length > codelength)
        {
            StartCoroutine(WrongCode());
            return;
        }

        Debug.Log("Current code: " + currentCode);

        // Check if the code matches
        if (currentCode == redCode)
        {
            if (pointLight != null)
            {
                InsertKey.rDoorON = true; // the gadget is now red
                pointLight.color = redCODE;
            }
            Debug.Log("Correct code entered!");
        }
    }

    IEnumerator WrongCode()
    {
        currentCode = "";
        Debug.Log("Code reset!");
        stopQueue = true; // Stop typing
        pointLight.color = Color.magenta; // Turn purple
        yield return new WaitForSeconds(0.2f);
        stopQueue = false; // Resume
        pointLight.color = defaultColor; // Then back to white

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        // Clear all first
        for (int i = 0; i < displayCubes.Length; i++)
        {
            displayCubes[i].material = blankMaterial; // Go back to "X"
        }

        // Apply current digits
        for (int i = 0; i < currentCode.Length && i < displayCubes.Length; i++)
        {
            int digit = int.Parse(currentCode[i].ToString());
            displayCubes[i].material = numberMaterials[digit];
        }
    }

    IEnumerator PlayQueue()
    {
        isPlaying = true;


        while (inputQueue.Count > 0)
        {
            if (stopQueue)
            {
                // Clear the queue and stop playing
                inputQueue.Clear();
                break;
            }

            string button = inputQueue.Dequeue();
            _anim.SetTrigger(button);

            // Wait until the current animation is done
            yield return new WaitForSeconds(GetAnimationLength(button));
        }

        isPlaying = false;
    }

    // Returns the length of each button animation (adjust to your clips)
    float GetAnimationLength(string button)
    {
        switch (button)
        {
            case "button1": return 0.1f;
            case "button2": return 0.1f;
            case "button3": return 0.1f;
            case "button4": return 0.1f;
            case "button5": return 0.1f;
            case "button6": return 0.1f;
            case "button7": return 0.1f;
            case "button8": return 0.1f;
            case "button9": return 0.1f;
            case "button0": return 0.1f;
            default: return 0.3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHandheld) // Pickup the device
        {
            if (other.CompareTag("player"))
            {
                nearPlayer = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isHandheld) // Pickup the device
        {
            if (other.CompareTag("player"))
            {
                nearPlayer = false;
            }
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (!isHandheld) // Pickup the device
        {
            if (other.CompareTag("player"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    handheld.SetActive(true); // ON
                    Destroy(gameObject); // Destroy itself
                }
            }
        }
    } */
}

