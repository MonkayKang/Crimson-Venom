using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game objects
    private GameObject Flashlight; // The world pickup
    public GameObject ItemFlashlight; // The one the player holds 

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
    private bool isOn = false;

    // Float
    private float originalHeight;
    public float crouchHeight = 0.5f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for FPS-style control
        originalHeight = transform.localScale.y; // Store Player Standing

        // Make sure held flashlight is off at start
        if (ItemFlashlight != null)
        {
            ItemFlashlight.SetActive(false);
        }
    }

    void Update()
    {
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

        // Toggle flashlight
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F)) // When Press "F"
        {
            isOn = !isOn; // Toggle Bool to see if the light is on our off
            ItemFlashlight.SetActive(isOn); // Set the object ON or OFF
        }

        // Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (transform.localScale.y == originalHeight) // If your standing
            {
                // Crouch
                speed = 2.5f;
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z); // Height becomes 0.5
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) // If let go of Ctrl
        {
            // Stand up
            speed = 5f;
            transform.localScale = new Vector3(transform.localScale.x, originalHeight, transform.localScale.z); // Go back to standing
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
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
                Flashlight = null; // "Remove the Paint"
                nearFlashlight = false; // Outside the range
            }
        }
    }

    public void Pickup()
    {
        if (Input.GetKeyDown(KeyCode.E) && Flashlight != null)
        {
            hasFlashlight = true; // Player has Flashlight
            Destroy(Flashlight); // Remove the pickup from the world
            Flashlight = null; // Flashlight (Paint) is gone
            nearFlashlight = false; // No longer near the flashlight (Its gone)
        }
    }
}