using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class KeypadController : MonoBehaviour
{
    public GameObject lights; // Lights
    public GameObject destroyOBJ; // If we want to remove a door

    // ATTACH TO PLAYER OR CAMERA
    public CinemachineVirtualCamera virtualCam; // Virtual Cam reference
    public float interactDistance = 3f;

    public Material[] digitMaterials; // index 0–9
    public Material idleMaterial;      // X material

    public MeshRenderer[] displaySlots; // size 4 

    int[] enteredCode = new int[4];
    int inputIndex = 0;

    int[] correctCode = { 1, 9, 8, 8 };

    // Audio 
    public AudioSource source;
    public AudioClip ButtonClick;
    public AudioClip WrongInput;

    Camera mainCam; // The real camera used for raycasting

    void Start()
    {
        mainCam = Camera.main; // Get the real camera (with CinemachineBrain)

        for (int i = 0; i < displaySlots.Length; i++)
        {
            displaySlots[i].material = new Material(idleMaterial);
        }
        lights.SetActive(false); // Set the lights off
        ResetKeypad();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryPressButton();
    }

    void TryPressButton()
    {
        // Always raycast from the MAIN camera
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance)) return;

        if (!hit.collider.CompareTag("KeypadButton")) return;

        // How centered the hit is (smaller = more precise)
        float centerTolerance = 0.5f;

        Vector3 screenPos = mainCam.WorldToViewportPoint(hit.point);
        float distanceFromCenter =
            Vector2.Distance(new Vector2(0.5f, 0.5f),
                             new Vector2(screenPos.x, screenPos.y));

        if (distanceFromCenter > centerTolerance) return;

        int digit = hit.collider.GetComponent<KeypadButton>().digit;
        RegisterDigit(digit);
    }

    void RegisterDigit(int digit)
    {
        if (inputIndex >= 4) return;

        source.PlayOneShot(ButtonClick); // Play the SFX

        enteredCode[inputIndex] = digit;
        displaySlots[inputIndex].material = digitMaterials[digit];
        inputIndex++;

        if (inputIndex == 4)
            CheckCode();
    }

    void CheckCode()
    {
        for (int i = 0; i < 4; i++)
        {
            if (enteredCode[i] != correctCode[i])
            {
                ResetKeypad();
                source.PlayOneShot(WrongInput); // Play the Wrong SFX
                return;
            }
        }

        lights.SetActive(true); // Turn the lights on

        if (destroyOBJ != null)
            Destroy(destroyOBJ); // Destroy the Object

        Debug.Log(" Correct Code!");
        // unlock door / trigger event
    }

    void ResetKeypad()
    {
        inputIndex = 0;

        for (int i = 0; i < 4; i++)
            displaySlots[i].material = idleMaterial;
    }
}