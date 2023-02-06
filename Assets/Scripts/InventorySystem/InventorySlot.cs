using System;

namespace InventorySystem
{
    [Serializable]
    public struct InventorySlot
    {
        public InventoryItem item;
        public int count;
    }
}