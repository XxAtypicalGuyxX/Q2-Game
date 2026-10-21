using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement (WASD)")]
    public float moveSpeed = 5f;
    public float gravity = 9.81f;

    [Header("Rotation (Arrow Keys)")]
    public float rotationSpeed = 120f;
    public float maxVerticalAngle = 80f;

    [Header("References")]
    public Transform playerCamera;

    private CharacterController controller;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Auto-assign the camera if left empty in the Inspector
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>()?.transform;
        }
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    void HandleRotation()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Explicitly check Arrow Keys for looking around
        if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) verticalInput = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) verticalInput = -1f;

        // 1. Turn the entire Player body left/right
        float rotateY = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotateY);

        // 2. Tilt only the child Camera up/down (clamped to prevent flipping)
        float rotateX = verticalInput * rotationSpeed * Time.deltaTime;
        verticalRotation -= rotateX;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    void HandleMovement()
    {
        float moveX = 0f;
        float moveZ = 0f;

        // Explicitly check WASD keys for moving
        if (Input.GetKey(KeyCode.W)) moveZ = 1f;
        if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = 1f;

        // Calculate direction relative to where the player is currently facing
        Vector3 moveDirection = (transform.forward * moveZ) + (transform.right * moveX);

        // Normalize vector so diagonal movement isn't faster
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection *= moveSpeed;

        // Apply basic gravity physics
        if (controller.isGrounded)
        {
            verticalVelocity = -0.5f; // Slight downward force to stay glued to slopes/steps
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        moveDirection.y = verticalVelocity;

        // Execute the movement
        controller.Move(moveDirection * Time.deltaTime);
    }
}
