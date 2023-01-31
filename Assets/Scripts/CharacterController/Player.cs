using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody sphereRigidbody;
    private Vector2 move;
    private void Awake()
    {
        sphereRigidbody = GetComponent<Rigidbody>();
    }

    public void Movement(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        sphereRigidbody.velocity = new Vector3(move.x, 0, move.y);
    }
}
