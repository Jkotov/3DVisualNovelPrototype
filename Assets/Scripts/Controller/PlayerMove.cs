using System.Linq;
using Dialog;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : Movement
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float forwardSpeed;
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
            if (CanMove == false)
                move = Vector2.zero;
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

        private bool CanMove => !OpenedWindowManager.Instance.Opened.Any();
    }
}