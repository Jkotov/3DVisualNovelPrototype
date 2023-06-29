using Moveables;
using UnityEngine;

namespace PlayerActions
{
    public class SceneLoadData : MonoBehaviour
    {
        public string Scene => scene;
        [SerializeField] private string scene;
        public PositionRotation PositionRotation => positionRotation;
        [SerializeField] private PositionRotation positionRotation;
    }
}