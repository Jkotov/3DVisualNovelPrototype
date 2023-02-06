using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : Movement
    {
        [SerializeField] float rotationSpeed;
        [SerializeField] float forwardSpeed;
        private CharacterController controller;
        private Vector2 move;
        
        public void ReadMoveInput(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }
        
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }
        
        private void FixedUpdate()
        {
            SetForwardSpeed(move.y * forwardSpeed);
            SetTurnSpeed(move.x * rotationSpeed);
        }

        protected override void SetForwardSpeed(float speed)
        {
            controller.SimpleMove(speed * transform.forward);
            forwardSpeedApplied?.Invoke(speed);
        }

        protected override void SetTurnSpeed(float speed)
        {
            transform.Rotate(Vector3.up, speed * Time.fixedDeltaTime);
            turnSpeedApplied?.Invoke(speed);
        }
    }
}