using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private InventoryStorage inventory;
        private InventoryCell[] cells;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            cells = GetComponentsInChildren<InventoryCell>();
        }

        public void ShowInventory()
        {
            for (int i = 0; i < inventory.MaxSlots; i++)
            {
                cells[i].Slot = inventory.Slots[i];
            }
        }
    }
}