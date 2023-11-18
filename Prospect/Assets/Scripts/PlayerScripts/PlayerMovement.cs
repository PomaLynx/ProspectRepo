using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    CharacterController PlayerController;

    [Header("Attributes")]
    private float playerSpeed = 7f;
    private float jumpForce = 2f;

    [Header("Gravity Parameters")]
    private float gravity = -9.81f;
    private Vector3 velocity;

    [Header("GroundCheck Parameters")]
    private Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        PlayerController = GetComponent<CharacterController>();
        groundCheck = transform.GetChild(1); 
    }

    void Update()
    {
        Movement();
        GroundCheck();
        Gravity();
        Jumping();
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 playerDirection = transform.right * moveX + transform.forward * moveZ;

        PlayerController.Move(playerDirection * playerSpeed * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        PlayerController.Move(velocity * Time.deltaTime);
    }

    void Jumping()
    {
        if (Input.GetKeyDown("space") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }
}
