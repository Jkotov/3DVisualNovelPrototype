using UnityEngine;

namespace InventorySystem
{
    public class InventoryTest : MonoBehaviour
    {
        [SerializeField] private int itemsCount;
        [SerializeField] private InventoryItem item;
        [SerializeField] private Inventory inventory;
        [ContextMenu("AddItems")]
        public void AddItems()
        {
            Debug.Log(inventory.TryAddItems(item, itemsCount));
        }
        [ContextMenu("RemoveItems")]
        public void RemoveItems()
        {
            Debug.Log(inventory.TryRemove(item, itemsCount));
        }
    }
}
