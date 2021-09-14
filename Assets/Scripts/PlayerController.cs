using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerControllerUtils))]
public class PlayerController : MonoBehaviour
{
    private PlayerControllerUtils utils;

    [SerializeField]
    private float speed = 5.0f;

    private float sensitivity = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        utils = GetComponent<PlayerControllerUtils>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get keyboard input
        float _horizontalKeyboardInput = Input.GetAxisRaw("Horizontal");
        float _verticalKeyboardInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction as a 3d vector
        Vector3 _horizontalMovement = Vector3.right * _horizontalKeyboardInput;
        Vector3 _verticalMovement = Vector3.forward * _verticalKeyboardInput;

        // Calculate movement velocity as a 3d vector
        Vector3 _velocity = (_horizontalMovement + _verticalMovement).normalized * speed;

        // Apply movement
        utils.Move(_velocity);

        // Get horizontal mouse input
        float _horizontalMouseInput = Input.GetAxisRaw("Mouse X");

        Vector3 _playerRotation = Vector3.up * _horizontalMouseInput * sensitivity;

        // Apply rotation
        utils.RotatePlayer(_playerRotation);

        // Get vertical mouse input
        float _verticalMouseInput = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = Vector3.right * _verticalMouseInput * sensitivity;

        // Apply rotation
        utils.RotateCamera(_cameraRotation);
    }
}
