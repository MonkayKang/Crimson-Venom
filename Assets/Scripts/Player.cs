using System.Collections;
using System.Collections.Generic;
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
    private float stepTimer; // How long they walked

    //UI
    public Sprite flashlightOffSprite;  // Sprite when flashlight is off
    public Sprite flashlightOnSprite;   // Sprite when flashlight is on

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

    public static bool GadgetANIM; // Coutine

    // Float
    private float originalHeight;
    public float crouchHeight = 0.5f;
    public float throwForce = 15f; // How far will the projectile go (inerthia)

    public float lightDis = 5f; // The raycast distance
    public float angle = 30f; // Angle of flashlight cone 


    void Start()
    {
        GadgetANIM = false;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for FPS-style control
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

        hasFlashlight = false; // Reusable when resetting the game
        hasGadget = false; // resting the game variable
    }

    void Update()
    {
        // Check if player is moving (on ground and not zero velocity)
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);

        if (GadgetANIM)
        {
            StartCoroutine(WaitfewSeconds());
        }

        if (isMoving)
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
        }

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

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera vertically 
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

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

        // Movement Input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
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

        if (Input.GetKeyDown(KeyCode.F) && hasFlashlight) // Flashlight 
        {
            isOn = !isOn; // The bool of which, Is it on or not

            ItemFlashlight.SetActive(isOn);

            if (isOn)
            {
                Gadget.SetActive(false); // The gadget is off
                source.PlayOneShot(clip1); // On SFX
            }
            else
            {
                source.PlayOneShot(clip2); // Off SFX
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && hasGadget)
        {
            ItemFlashlight.SetActive(false); // Gadget
            Gadget.SetActive(true);
            isOn = false;
        }

        if (Input.GetKeyDown(KeyCode.H)) // Nothing
        {
            ItemFlashlight.SetActive(false);
            Gadget.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.E) && Flashlight != null)
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
    }

    IEnumerator WaitfewSeconds()
    {
        Gadget.SetActive(false); // Disable device
        yield return new WaitForSeconds(1.2f);
        Gadget.SetActive(true); // Return Device
        GadgetANIM = false; // Turn off animation
    }

}