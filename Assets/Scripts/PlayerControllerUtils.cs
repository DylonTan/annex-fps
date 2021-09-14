using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControllerUtils : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody rb;

    private Vector3 velocity = Vector3.zero;
    private Vector3 playerRotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void RotatePlayer(Vector3 _playerRotation)
    {
        playerRotation = _playerRotation;
    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            // Calculate displacement per unit time as a 3d vector
            Vector3 _displacement = velocity * Time.fixedDeltaTime;

            rb.MovePosition(rb.position + _displacement);
        }
    }

    private void PerformRotation()
    {
        // Calculate rotation angle as a quaternion
        Quaternion angle = rb.rotation * Quaternion.Euler(playerRotation);

        rb.MoveRotation(angle);

        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }
}
