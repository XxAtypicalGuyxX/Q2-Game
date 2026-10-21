using UnityEngine;

public class ArrowKeyRotation : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 120f;
    public float maxVerticalAngle = 80f;

    [Header("References")]
    public Transform playerCamera; // Drag your child Camera object here in the Inspector

    private float verticalRotation = 0f;

    void Start()
    {
        // Automatically find the camera if not assigned
        if (playerCamera == null && transform.childCount > 0)
        {
            playerCamera = GetComponentInChildren<Camera>()?.transform;
        }
    }

    void Update()
    {
        // 1. Read Arrow Key Inputs
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.RightArrow)) horizontalInput = 1f;
        if (Input.GetKey(KeyCode.UpArrow)) verticalInput = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) verticalInput = -1f;

        // 2. Scale by speed and time
        float rotateY = horizontalInput * rotationSpeed * Time.deltaTime;
        float rotateX = verticalInput * rotationSpeed * Time.deltaTime;

        // 3. Rotate the whole Player body horizontally (Left / Right)
        transform.Rotate(Vector3.up * rotateY);

        // 4. Calculate, clamp, and apply vertical rotation ONLY to the child Camera (Up / Down)
        verticalRotation -= rotateX;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        if (playerCamera != null)
        {
            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
