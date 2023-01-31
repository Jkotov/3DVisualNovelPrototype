using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody sphereRigidbody;
    private PlayerInput playerInput;

    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        PlayerInputActions playerinputActions = new PlayerInputActions();
        playerinputActions.Player.Enable();
        playerinputActions.Player.Movement.performed += Movement_performed;
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        
        Vector2 inputVector = context.ReadValue<Vector2>();
        float speed = 5f;
        sphereRigidbody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
        Debug.Log(inputVector);
    }
}
