using System;
using System.Collections.Generic;
using InventorySystem;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance { get; private set; }
        public bool IsShowing { get; private set; }
        [SerializeField] private GameObject inventoryWindow;
        private InventoryCell[] cells;
        private void Awake()
        {
            
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            cells = inventoryWindow.GetComponentsInChildren<InventoryCell>();
        }

        public void ShowInventory(Inventory inventory)
        {
            IsShowing = true;
            Debug.Log(cells.Length);
            Debug.Log(inventory.Slots.Count);
            inventoryWindow.SetActive(true);
            for (int i = 0; i < inventory.MaxSlots; i++)
            {;
                cells[i].Slot = inventory.Slots[i];
            }
        }

        public void HideInventory()
        {
            IsShowing = false;
            inventoryWindow.SetActive(false);
        }
    }
}