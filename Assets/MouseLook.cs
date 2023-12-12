using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    void Start()
    {
        // hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        // get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; // invert the mouse Y
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // clamp the rotation

        // rotate the camera

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // rotate the player body

        playerBody.Rotate(Vector3.up * mouseX);

    }
}