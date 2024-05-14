using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    //[SerializeField] Transform groundCheck;
    //[SerializeField] LayerMask ground;
    Rigidbody rb;

    bool canJump = true; // Flag to track if the player can jump

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(hori * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);

        if (Input.GetButtonDown("Jump") && canJump) // Check if canJump is true
        {
            Jump();
            canJump = false; // Set canJump to false to prevent jumping
            StartCoroutine(EnableJump()); // Start coroutine to enable jumping after 3 seconds
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // Changed rb.velocity.y to rb.velocity.z
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(1f); // Wait for 3 seconds
        canJump = true; // Set canJump back to true to allow jumping again
    }

    /*  bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
    */
}
