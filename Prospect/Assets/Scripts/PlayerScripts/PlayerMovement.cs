using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    Rigidbody playerRigidbody;

    [Header("Attributes")]
    public float playerSpeed = 7;

    [Header("Jump Parameters")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool canJump = true;

    [Header("Groundcheck Parameters")]
    public float groundDrag = 1;
    public float playerHeight = 1.05f;
    public LayerMask groundMask;
    public bool isGrounded;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckIfGrounded();
        SpeedControl();
        Movement();
        Jumping();
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, groundMask);

        if (isGrounded)
        {
            playerRigidbody.drag = groundDrag;
        }
        else
        {
            playerRigidbody.drag = 0;
        }
    }

    void SpeedControl()
    {
        //Gets current velocity
        Vector3 flatVelocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);

        //Limits to set player speed
        if (flatVelocity.magnitude > playerSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * playerSpeed;
            playerRigidbody.velocity = new Vector3(limitedVelocity.x, playerRigidbody.velocity.y, limitedVelocity.z);
        }
    }

    void Movement()
    {
        //Gets keyboard input
        float xMovement = Input.GetAxisRaw("Horizontal");
        float zMovement = Input.GetAxisRaw("Vertical");

        //Gets the players forward direction
        Vector3 playerDirection = transform.right * xMovement + transform.forward * zMovement;

        //Moves the player
        if (isGrounded)
        {
            playerRigidbody.AddForce(playerDirection.normalized * playerSpeed, ForceMode.Force);
        }
        else
        {
            playerRigidbody.AddForce(playerDirection.normalized * playerSpeed * airMultiplier, ForceMode.Force);
        }
    }

    void Jumping()
    {
        if (Input.GetKeyDown("space") && canJump && isGrounded)
        {
            canJump = false;

            //The actual jump
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void ResetJump()
    {
        canJump = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * playerHeight);
    }
}
