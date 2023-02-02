using UnityEngine;
using UnityEngine.Events;

namespace Controller
{
    public abstract class Movement : MonoBehaviour
    {
        public UnityEvent<float> forwardSpeedApplied;
        public UnityEvent<float> turnSpeedApplied;

        protected abstract void SetForwardSpeed(float speed);
        protected abstract void SetTurnSpeed(float speed);
    }
}