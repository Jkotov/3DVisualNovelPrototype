using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float speed;
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
        sphereRigidbody.velocity = transform.forward * move.y * speed;
        transform.Rotate(Vector3.up, Time.fixedDeltaTime * move.x * rotationSpeed);
    }
}