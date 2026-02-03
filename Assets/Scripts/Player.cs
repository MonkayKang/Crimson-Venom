using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    // Game objects
    private GameObject Flashlight; // The world pickup
    public GameObject ItemFlashlight; // The one the player holds (Aka the one in their inventory)
    public GameObject Gadget; // The object that will appear and re-appear (Selections)
    public GameObject LeverItem; // The hand held lever

    // Audio
    public AudioSource source;
    public AudioSource source2;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip WalkingAudio;

    // Walking
    public float pitchMin = 0.9f;
    public float pitchMax = 1.1f;
    public float stepDelay = 0.5f; // delay between steps
    public float stepTimer; // How long they walked

    //UI
    public Sprite flashlightOffSprite;  // Sprite when flashlight is off
    public Sprite flashlightOnSprite;   // Sprite when flashlight is on

    // Inventory UI
    public Sprite blankUI; // the blank UI
    public Sprite flashlightUI; // The flashlight in that blank
    public Sprite gadgetUI; // The Gadget in the that blank
    public Sprite leverUI; // The lever UI 

    public Image firstSLOT; // First Inventory spot UI
    public Image secondSLOT; // Secondary Inventory spot UI
    public Image thirdSLOT; // Third Inventory spot UI

    // Movement settings
    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    public Transform cameraTransform; // Assign camera in the Inspector
    public Transform flashlight;      // Flashlight attached to player 

    // Bools
    private bool nearFlashlight = false;
    private bool hasFlashlight = false;
    public static bool hasGadget = false;
    private bool isOn = false;
    private bool nearDart = false;
    public static bool gadgetON;

    public static bool GadgetANIM; // Coutine
    public static bool timeSTOP; // Did time stop OLD

    public static bool STOP; // Time Stop NEW;

    // Float
    private float originalHeight;
    public float crouchHeight = 0.5f;
    public float throwForce = 15f; // How far will the projectile go (inerthia)

    public float lightDis = 5f; // The raycast distance
    public float angle = 30f; // Angle of flashlight cone 

    // Controller Camera Control
    public float controllerSensitivity = 120f; // Controller look speed
    public float stickDeadzone = 0.25f;

    // Mouse Camera Control
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;
    public float maxLookPerFrame = 8f; // prevents instant flips


    void Start()
    {
        // Make time continue 
        timeSTOP = false;

        // Both UI's will be blank
        firstSLOT.sprite = blankUI;
        secondSLOT.sprite = blankUI;
        thirdSLOT.sprite = blankUI;


        GadgetANIM = false;
        controller = GetComponent<CharacterController>();
        originalHeight = transform.localScale.y; // Store Player Standing

        // Make sure held flashlight is off at start
        if (ItemFlashlight != null)
        {
            ItemFlashlight.SetActive(false);
        }

        // Make sure gadget is off at start
        if (Gadget != null)
        {
            Gadget.SetActive(false);
        }

        if (LeverItem != null)
        {
            LeverItem.SetActive(false);
        }

        hasFlashlight = false; // Reusable when resetting the game
        hasGadget = false; // resting the game variable
        gadgetON = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (STOP)
            return; // hard stop, nothing runs

        if (timeSTOP)
            {
                Time.timeScale = 0f; // if Timestop is true, then time will stop
                if (Input.GetButtonDown("Interact"))
                {
                    // GameObject.Find("---ZOOMUI---").SetActive(false);
                    // GameObject.Find("---ZOOMUIBACKUP---").SetActive(false);
                    timeSTOP = false; // Time continues
                }
            }
            if (!timeSTOP)
            {
                Time.timeScale = 1f; // if Timestop is false, then time will continue
            }


            // UI will change if they have the objects
            if (hasFlashlight)
            {
                firstSLOT.sprite = flashlightUI;
            }

            if (hasGadget)
            {
                secondSLOT.sprite = gadgetUI;
            }

            if (Lever.playerLEVER)
            {
                thirdSLOT.sprite = leverUI;
            }

            if (!Lever.playerLEVER)
            {
                thirdSLOT.sprite = blankUI;
            }

            // Slot Sizes
            RectTransform slotTransform = firstSLOT.GetComponent<RectTransform>(); // Gets the size of the image
            RectTransform slotTransform2 = secondSLOT.GetComponent<RectTransform>(); // Gets the size of the image
            RectTransform slotTransform3 = thirdSLOT.GetComponent<RectTransform>(); // Gets the size of the image

            // Check if player is moving (on ground and not zero velocity)
            // bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);

            if (GadgetANIM)
            {
                StartCoroutine(WaitfewSeconds());
            }

            /* if (isMoving)
            {
                stepTimer -= Time.deltaTime;

                if (stepTimer <= 0f)
                {
                    source2.pitch = Random.Range(pitchMin, pitchMax);
                    source2.PlayOneShot(WalkingAudio);
                    stepTimer = stepDelay;
                }
            } 
            else
            {
                stepTimer = 0f; // reset timer when stopping
            } */

            if (isOn)
            {
                // Number of rays to show on each side
                int raysPerSide = 3;

                // Step angle between each ray
                float step = angle / raysPerSide;

                for (int i = -raysPerSide; i <= raysPerSide; i++) // For each rays
                {
                    Quaternion rotation = Quaternion.Euler(0, i * step, 0);
                    Vector3 dir = rotation * cameraTransform.forward;
                    Debug.DrawRay(transform.position + Vector3.up * .5f, dir * lightDis, Color.yellow); // Draw the rays (For my purpose only)

                    if (Physics.Raycast(transform.position + Vector3.up * .5f, dir, out RaycastHit hit, lightDis)) // Shoot a raycast
                    {
                        if (hit.collider.CompareTag("Damage")) // If it hits the tag "Damage"
                        {
                            SecAI target = hit.collider.GetComponent<SecAI>(); // Find that objects script
                            EnemyAI target2 = hit.collider.GetComponent<EnemyAI>(); // If its the other AI

                            if (target != null)
                            {
                                target.hitTIMER = Mathf.Min(target.hitTIMER + 0.2f, 100f); // Adds. MAX 
                            }

                            if (target2 != null)
                            {
                                target2.hitTIMER = Mathf.Min(target2.hitTIMER + 0.2f, 100f); // Adds. MAX 
                            }
                        }
                    }
                }
            }

            // Camera Control

            // Controller input
            float stickX = Input.GetAxis("RightStickX");
            float stickY = Input.GetAxis("RightStickY");

            // Mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

            // Deadzone for controller
            if (Mathf.Abs(stickX) < stickDeadzone) stickX = 0f;
            if (Mathf.Abs(stickY) < stickDeadzone) stickY = 0f;

            // Scale controller
            stickX *= controllerSensitivity * Time.deltaTime;
            stickY *= controllerSensitivity * Time.deltaTime;

            // Combine inputs
            float lookX = Mathf.Clamp(stickX + mouseX, -maxLookPerFrame, maxLookPerFrame);
            float lookY = Mathf.Clamp(stickY + mouseY, -maxLookPerFrame, maxLookPerFrame);

            // Rotate player (horizontal)
            transform.Rotate(Vector3.up * lookX);

            // Rotate camera (vertical)
            xRotation -= lookY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Movement Control
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            controller.Move(move * speed * Time.deltaTime);

            // Walk Audio (Keyboard + Joystick)
            bool isMoving = move.magnitude > 0.1f; // Checks if player is moving (stick deadzone included)

            //  Step Audio
            if (isMoving)
            {
                stepTimer -= Time.deltaTime; // For every movment. minus the time

                if (stepTimer <= 0f)
                {
                    source2.pitch = Random.Range(pitchMin, pitchMax); // Slight pitch variation for realism
                    source2.PlayOneShot(WalkingAudio); // Play walking sound
                    stepTimer = stepDelay; // Reset timer for next step
                }
            }
            else
            {
                stepTimer = 0f; // Reset timer when player stops moving
            }
            // Ground Check

            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // Flashlight follows camera
            if (flashlight != null)
            {
                flashlight.rotation = cameraTransform.rotation;
            }

            // Ground Check
            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Apply Gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // Pickup flashlight when nearby
            if (nearFlashlight)
            {
                Pickup();
            }

            if (nearDart)
            {
                Pickup();
            }

            if (Input.GetButtonDown("Flashlight") && hasFlashlight) // Flashlight 
            {
                isOn = !isOn; // The bool of which, Is it on or not

                ItemFlashlight.SetActive(isOn);
                if (isOn)
                {
                    gadgetON = false;
                    slotTransform.sizeDelta = new Vector2(320f, 320f); // firstSlot image size is increase 
                    Gadget.SetActive(false); // The gadget is off
                    LeverItem.SetActive(false); // The lever is off
                    slotTransform2.sizeDelta = new Vector2(250f, 250f); // SecondSlot image size is decrease
                    slotTransform3.sizeDelta = new Vector2(250f, 250f); // SecondSlot image size is decrease
                    source.PlayOneShot(clip1); // On SFX
                }
                else
                {
                    slotTransform.sizeDelta = new Vector2(250f, 250f); // firstSlot image size is decrease
                                                                       // firstSlot image size is return back
                    source.PlayOneShot(clip2); // Off SFX
                }
            }

            if (Input.GetButtonDown("Square") && hasGadget || gadgetON)
            {
                slotTransform2.sizeDelta = new Vector2(320f, 320f); // secondSlot image size is increase 
                slotTransform.sizeDelta = new Vector2(250f, 250f); // firstSlot image size is Decrease
                slotTransform3.sizeDelta = new Vector2(250f, 250f); // firstSlot image size is Decrease
                ItemFlashlight.SetActive(false); // Gadget
                Gadget.SetActive(true);
                LeverItem.SetActive(false); // The lever is off
                isOn = false;
            }

            if (Input.GetButtonDown("Triangle") && Lever.playerLEVER)  // Lever
            {
                gadgetON = false;
                slotTransform2.sizeDelta = new Vector2(250f, 250f); // firstSlot image size is decrease
                slotTransform.sizeDelta = new Vector2(250f, 250f); // firstSlot image size is Decrease
                slotTransform3.sizeDelta = new Vector2(320f, 320f); // firstSlot image size is increase
                ItemFlashlight.SetActive(false);
                Gadget.SetActive(false);
                if (Lever.playerLEVER == true)
                {
                    LeverItem.SetActive(true); // The lever is on
                }
                isOn = false;
            }

            // Crouch
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (transform.localScale.y == originalHeight) // If your standing
                {
                    // Crouch
                    speed = 3f;
                    transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z); // Height becomes 0.5
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftControl)) // If let go of Ctrl
            {
                // Stand up
                speed = 3f;
                transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z); // Go back to standing
            }
        

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Damage")) // Just in case character Controller doesnt register hits head on
        {
            SceneManager.LoadScene("Lose");
        }

        if (other.CompareTag("Flashlight"))
        {
            DialogueUI.pickRANGE = true; // UI now doesn't dissapear until they pick up the item
            nearFlashlight = true; // Bool that tells the player that they can pickup the item
            Flashlight = other.gameObject; // "Paint the Target"
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            if (Flashlight == other.gameObject)
            {
                DialogueUI.pickRANGE = false; // Player is too far away for the UI;
                Flashlight = null; // "Remove the Paint"
                nearFlashlight = false; // Outside the range
            }
        }

    }

    public void Pickup()
    {

        if (Input.GetButtonDown("Interact") && Flashlight != null)
        {
            source.PlayOneShot(clip3);
            hasFlashlight = true; // Player has Flashlight
            Destroy(Flashlight); // Remove the pickup from the world
            Flashlight = null; // Flashlight (Paint) is gone
            nearFlashlight = false; // No longer near the flashlight (Its gone)
            UICounter.taskCounter++; // New Task
            DialogueUI.pickRANGE = false; // Set it back to false for other UI
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit) // If hits the enemy
    {

        if (hit.gameObject.CompareTag("Damage"))
        {
            SceneManager.LoadScene("Lose");
        }

        Rigidbody rb = hit.collider.attachedRigidbody;

        if (rb == null || rb.isKinematic) // if it hits anything that doesnt move
            return;

        // prevent pushing downwards
        if (hit.moveDirection.y < -0.3f) 
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z); // Use the momentum to
        rb.AddForce(pushDir * 5f, ForceMode.Impulse); // Add force
    }

    IEnumerator WaitfewSeconds()
    {
        Gadget.SetActive(false); // Disable device
        yield return new WaitForSeconds(1.2f);
        Gadget.SetActive(true); // Return Device
        GadgetANIM = false; // Turn off animation
    }
}