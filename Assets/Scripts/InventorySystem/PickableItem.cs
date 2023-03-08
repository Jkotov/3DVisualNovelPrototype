using System;
using UnityEngine;

namespace InventorySystem
{
    public class PickableItem : MonoBehaviour
    {
        public InventoryItem InventoryItem => inventoryItem;
        public int Count => count;
        
        [SerializeField] private InventoryItem inventoryItem;
        [SerializeField] private int count;
    }
}
