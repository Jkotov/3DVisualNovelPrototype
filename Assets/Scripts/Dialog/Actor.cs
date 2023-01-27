using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "New Actor", menuName = "ScriptableObjects/Actor")]
    public class Actor : ScriptableObject
    {
        public string actorName;
        public Sprite sprite;
    }
}