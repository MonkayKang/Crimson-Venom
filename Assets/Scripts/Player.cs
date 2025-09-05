using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game objects
    public GameObject Flashlight; 


    public float speed = 5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;
    private float xFlashRotation = 0f;

    public Transform cameraTransform; // Assign camera in the Inspector
    public Transform flashlight; // Assign Flashlight

    //Bools
    private bool nearFlashlight = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor for FPS-style control
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
        flashlight.rotation = cameraTransform.rotation;

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

        if (nearFlashlight)
        {
            Pickup();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            nearFlashlight = true; // Bool that tells the player script you are close enough
            Flashlight = other.gameObject; // "Paint the Target"
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            Flashlight = null; // On leave, "remove the paint"
            nearFlashlight = false; // Not near the flashlight
        }
    }

    public void Pickup()
    {
        if (nearFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(Flashlight); // When press E and the "Target is Painted", Destroy it
            }
        }
    }
}
