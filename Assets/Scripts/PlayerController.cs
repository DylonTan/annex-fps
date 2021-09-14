using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    GameObject cameraHolder;

    [SerializeField]
    float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    float verticalRotationAngle;
    bool isGrounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        float horizontalInput = Input.GetAxisRaw("Mouse X");
        float verticalInput = Input.GetAxisRaw("Mouse Y");

        Vector3 horizontalRotation = Vector3.up * horizontalInput * mouseSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(horizontalRotation));

        verticalRotationAngle += verticalInput * mouseSensitivity;
        verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, -90.0f, 90.0f);

        Vector3 verticalRotation = Vector3.left * verticalRotationAngle;

        cameraHolder.transform.localEulerAngles = verticalRotation;
    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float forwardInput = Input.GetAxisRaw("Vertical");

        Vector3 horizontalMovement = transform.right * horizontalInput;
        Vector3 forwardMovement = transform.forward * forwardInput;

        Vector3 velocity = (horizontalMovement + forwardMovement).normalized * walkSpeed;

        Vector3 displacement = velocity * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + displacement);
    }
}
