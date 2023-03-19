using Moveables;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public string Scene => scene;
    [SerializeField] private string scene;
    public PositionRotation PositionRotation => positionRotation;
    [SerializeField] private PositionRotation positionRotation;
}