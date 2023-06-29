using Destroyables;
using UnityEngine;

namespace InventorySystem
{
    public class PickableItem : Destroyable
    {
        public InventoryItem InventoryItem => inventoryItem;
        public int Count => count;
        [SerializeField] private InventoryItem inventoryItem;
        [SerializeField] private int count;
    }
}
