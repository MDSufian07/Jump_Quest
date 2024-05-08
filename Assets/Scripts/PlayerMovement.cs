using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float hori = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(hori * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.y);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            Destroy(collision.transform.parent.gameObject);

        }

    }

    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}
