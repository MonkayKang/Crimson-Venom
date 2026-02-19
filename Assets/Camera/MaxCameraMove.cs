using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCameraMove : MonoBehaviour
{
    public float mouseSensitivity = 200f; // How fast

    public float maxUp = 60f; // How far UP
    public float maxDown = -60f; // How far DOWN

    public float maxLeft = -70f; // How far left
    public float maxRight = 70f; // How far Right

    float yawOffset = 0f; // Start at 0 (Left and Right For those who doesn't know what yaw means)
    float pitchOffset = 0f; // Start at 0 (Up or down)

    Quaternion startRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the mouse
        startRotation = transform.localRotation; // The starting Rotation is the object of wher it is at
                                                    // Ensure it stays still
    }

    void Update()
    {
        // Get the mouse input and scale it by sensitivity and frame time
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update yaw and pitch offsets based on mouse movement (Offset = change from original position)
        // yawOffset = left/right rotation, pitchOffset = up/down rotation
        yawOffset += mouseX;
        pitchOffset -= mouseY;

        // The limit of the camera "Clamp"
        yawOffset = Mathf.Clamp(yawOffset, maxLeft, maxRight);
        pitchOffset = Mathf.Clamp(pitchOffset, maxDown, maxUp);

        // Angle movement
        Quaternion yaw = Quaternion.AngleAxis(yawOffset, Vector3.up);
        Quaternion pitch = Quaternion.AngleAxis(pitchOffset, Vector3.right);

        // Move the camera based on yaw and pitch
        transform.localRotation = startRotation * yaw * pitch;
    }
}
