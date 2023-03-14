using System;
using UnityEngine;

namespace InventorySystem
{
    [Serializable]
    public struct InventorySlot
    {
        [SerializeField] public InventoryItem item;
        [SerializeField] public int count;
    }
}