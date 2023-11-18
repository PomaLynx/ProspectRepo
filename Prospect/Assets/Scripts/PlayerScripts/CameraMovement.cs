using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("References")]
    private GameObject Player;

    [Header("Attributes")]
    public float mouseSensitivity = 500f;
    float xRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Player = transform.parent.gameObject;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        //Mouse Input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Left and Right Mouse Movement
        Player.transform.Rotate(Vector3.up * mouseX);

        //Up and Down Mouse Movement
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
