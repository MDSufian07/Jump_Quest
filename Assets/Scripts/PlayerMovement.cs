using S_Camera;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask ground;
    private Rigidbody _rb;

    [SerializeField] private InputActionAsset inputActions; // Use InputActionAsset

    private InputAction _move;
    private InputAction _jump;
    private Vector2 _moveInput;

    public AudioSource jumpSound; // Jump to sound
    public AudioSource enemyDeathSound; // Corrected typo from "enemyDeathSound"

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        var playerActions = inputActions.FindActionMap("Player"); // Ensure correct Action Map
        _move = playerActions.FindAction("Move");
        _jump = playerActions.FindAction("Jump");
    }

    private void OnEnable()
    {
        if (this == null) return; // Prevent executing when object is destroyed

        _move.Enable();
        _jump.Enable();

        // Subscribe to the action events
        _move.performed += OnMovePerformed;
        _move.canceled += OnMoveCanceled;
        _jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        if (_move != null)
        {
            _move.performed -= OnMovePerformed; // Proper event unsubscription
            _move.canceled -= OnMoveCanceled;   // Proper event unsubscription
            _move.Disable();
        }

        if (_jump != null)
        {
            _jump.performed -= OnJumpPerformed;  // Proper event unsubscription
            _jump.Disable();
        }
    }

    private void FixedUpdate()
    {
        if (this == null) return; // Prevent executing when object is destroyed
        Move();
    }

    private void Move()
    {
        if (this == null || SwitchCamera360.ActiveCameraTransform == null) return; // Prevent NullReferenceException

        Vector3 camForward = SwitchCamera360.ActiveCameraTransform.forward;
        Vector3 camRight = SwitchCamera360.ActiveCameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = (camForward * _moveInput.y + camRight * _moveInput.x) * movementSpeed;
        _rb.velocity = new Vector3(moveDirection.x, _rb.velocity.y, moveDirection.z);
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        Jump();
    }

    private void Jump()
    {
        if (this == null || !IsGrounded()) return; // Prevent executing when object is destroyed

        if (jumpSound != null)
            jumpSound.Play();

        _rb.velocity = new Vector3(_rb.velocity.x, jumpForce, _rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            if (enemyDeathSound != null) // Corrected typo
                enemyDeathSound.Play();

            Destroy(collision.transform.parent.gameObject);
        }
    }

    private bool IsGrounded()
    {
        if (this == null) return false; // Prevent executing when object is destroyed

        return Physics.Raycast(transform.position, Vector3.down, 0.6f, ground);
    }
}
