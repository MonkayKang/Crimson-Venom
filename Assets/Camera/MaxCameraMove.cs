using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxCameraMove : MonoBehaviour
{
    public float mouseSensitivity = 200f;

    public float maxUp = 60f;
    public float maxDown = -60f;

    public float maxLeft = -70f;
    public float maxRight = 70f;

    float yawOffset = 0f;
    float pitchOffset = 0f;

    Quaternion startRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        startRotation = transform.localRotation;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yawOffset += mouseX;
        pitchOffset -= mouseY;

        yawOffset = Mathf.Clamp(yawOffset, maxLeft, maxRight);
        pitchOffset = Mathf.Clamp(pitchOffset, maxDown, maxUp);

        Quaternion yaw = Quaternion.AngleAxis(yawOffset, Vector3.up);
        Quaternion pitch = Quaternion.AngleAxis(pitchOffset, Vector3.right);

        transform.localRotation = startRotation * yaw * pitch;
    }
}
