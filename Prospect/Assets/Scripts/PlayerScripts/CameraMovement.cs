using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    public Transform player;

    [Header("Attributes")]
    public float mouseSensitivity;
    float xRotation = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        CamMovement();
    }

    void CamMovement()
    {
        //Takes mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotates left and right
        player.Rotate(Vector3.up * mouseX);

        //Rotates up and down 
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
