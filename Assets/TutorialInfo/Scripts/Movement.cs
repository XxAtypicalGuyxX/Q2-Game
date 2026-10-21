using UnityEngine;
using UnityEngine.InputSystem; // Required for the New Input System

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayerMovement : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 6f;
    public float turnSmoothTime = 0.1f;

    private CharacterController controller;
    private float turnSmoothVelocity;
    private Vector2 moveInput;
    private Vector2 lookInput;

    InputAction moveAction;
    InputAction lookAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        lookInput = lookAction.ReadValue<Vector2>();
        
        // Convert Vector2 input into a 3D movement direction
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }

        // Apply a small constant downward force so the character stays grounded on slopes
        if (!controller.isGrounded)
        {
            controller.Move(new Vector3(0, -9.81f * Time.deltaTime, 0));
        }
    }
}
