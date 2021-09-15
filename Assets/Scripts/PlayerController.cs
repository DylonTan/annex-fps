using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private PhotonView pv;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private Transform pivot;
    [SerializeField] private float mouseSensitivity, sprintSpeed, walkSpeed, sneakSpeed, jumpForce, leanSpeed;

    private float verticalRotationAngle = 0.0f;
    private float currentLeanAngle = 0.0f;
    private float maxLeanAngle = 12.0f;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        // Get reference to rigidbody
        rb = GetComponent<Rigidbody>();

        // Get reference to photon view
        pv = GetComponent<PhotonView>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check if photon view is not owned by local player
        if (!pv.IsMine)
        {
            // Destroy camera game object (to prevent incorrect camera view)
            Destroy(GetComponentInChildren<Camera>().gameObject);

            // Destroy rigidbody (to prevent rubber-banding)
            Destroy(rb);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if photon view is owned by local player
        if (pv.IsMine)
        {
            // Handle looking
            Look();

            // Handle movement
            Move();

            // Handle jumping
            Jump();

            // Handle leaning
            Lean();
        }
    }

    private void FixedUpdate()
    {
        // Check if photon view is owned by local player
        if (pv.IsMine)
        {
            // Calculate displacement
            Vector3 displacement = velocity * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + displacement);
        }
    }

    void Look()
    {
        // Get mouse input
        float horizontalInput = Input.GetAxisRaw("Mouse X");
        float verticalInput = Input.GetAxisRaw("Mouse Y");

        // Calculate horizontal rotation as a 3d vector
        Vector3 horizontalRotation = Vector3.up * horizontalInput * mouseSensitivity;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(horizontalRotation));

        // Calculate and set vertical rotation angle
        verticalRotationAngle += verticalInput * mouseSensitivity;

        // Clamp rotation angle within -90 and 90 degrees
        verticalRotationAngle = Mathf.Clamp(verticalRotationAngle, -90.0f, 90.0f);

        // Calculate vertical rotation as a 3d vector
        Vector3 verticalRotation = Vector3.left * verticalRotationAngle;

        // Set local euler angles as rotation vector
        cameraHolder.transform.localEulerAngles = verticalRotation;
    }

    void Move()
    {
        // Get keyboard input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float forwardInput = Input.GetAxisRaw("Vertical");

        // Calculate movement as a 3d vector
        Vector3 horizontalMovement = transform.right * horizontalInput;
        Vector3 forwardMovement = transform.forward * forwardInput;

        // Check if player is sprinting
        float speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            speed = sprintSpeed;
        } else if (Input.GetKey(KeyCode.LeftAlt) && isGrounded)
        {
            speed = sneakSpeed;
        }

        // Calculate and set velocity
        velocity = (horizontalMovement + forwardMovement).normalized * speed;
    }

    void Jump()
    {
        // Check if space key is pressed and player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Lean()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // Calculate lean angle towards left
            currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, maxLeanAngle, leanSpeed * Time.deltaTime);

        } else if (Input.GetKey(KeyCode.E))
        {
            // Calculate lean angle towards right
            currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, -maxLeanAngle, leanSpeed * Time.deltaTime);
        } else
        {
            // Calculate lean angle towards origin
            currentLeanAngle = Mathf.MoveTowardsAngle(currentLeanAngle, 0.0f, leanSpeed * Time.deltaTime);
        }

        // Set rotation to current lean angle about the forward axis
        pivot.transform.localRotation = Quaternion.AngleAxis(currentLeanAngle, Vector3.forward);
    }

    public void SetIsGrounded(bool _isGrounded)
    {
        isGrounded = _isGrounded;
    }
}
