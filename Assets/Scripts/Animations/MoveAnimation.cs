using Controller;
using Dialog;
using UnityEngine;

namespace Animations
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Movement))]
    public class MoveAnimation : MonoBehaviour
    {
        [SerializeField] private AnimatorVarName forwardSpeed;
        [SerializeField] private AnimatorVarName turningSpeed;
        private PlayerMove movement;
        private Animator anim;

        private void Awake()
        {
            forwardSpeed.cached = Animator.StringToHash(forwardSpeed.name);
            turningSpeed.cached = Animator.StringToHash(turningSpeed.name);
            anim = GetComponent<Animator>();
            movement = GetComponent<PlayerMove>();
            movement.forwardSpeedApplied.AddListener(OnForwardMove);
            movement.turnSpeedApplied.AddListener(OnTurning);
        }

        private void OnForwardMove(float speed)
        {
            anim.SetFloat(forwardSpeed.cached, speed);
        }

        private void OnTurning(float speed)
        {
            anim.SetFloat(turningSpeed.cached, speed);
        }
    }
}
