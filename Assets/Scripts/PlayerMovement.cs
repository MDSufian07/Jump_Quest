using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float groundCheckDistance = 0.6f; // Adjust based on the radius of your ball
    [SerializeField] LayerMask ground;
    Rigidbody rb;

    public AudioSource jumpSound;
    public AudioSource enemyDethSound;


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
            jumpSound.Play();
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);  // Preserve horizontal movement
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            enemyDethSound.Play();
            Destroy(collision.transform.parent.gameObject);
        }
    }

    bool IsGrounded()
    {
        // Raycast from the center of the ball downward
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, ground))
        {
            return true;
        }
        return false;
    }
}
