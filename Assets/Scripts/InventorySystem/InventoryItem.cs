using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item")]
    public class InventoryItem : ScriptableObject
    {
        public Sprite sprite;
        public string itemName = "New item";
        public string description = "Description";
        public int maxStack = 64;
    }
}