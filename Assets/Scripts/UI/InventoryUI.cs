using System;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public static InventoryUI Instance { get; private set; }
        public bool IsShowing { get; private set; }
        [SerializeField] private GameObject inventoryWindow;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private TextMeshProUGUI itemDescription;
        private InventoryCell[] cells;
        private void Awake()
        {
            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }
            
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
            foreach (var cell in cells)
            {
                cell.PointerExeted.AddListener(HideItemTooltip);
                cell.PointerEntered.AddListener(ShowItemTooltip);
            }
        }

        private void ShowItemTooltip(InventoryCell cell)
        {
            if (cell.Slot.item == null)
                return;
            itemName.text = cell.Slot.item.itemName;
            itemDescription.text = cell.Slot.item.description;
        }

        private void HideItemTooltip()
        {
            itemName.text = "";
            itemDescription.text = "";
        }
        
        public void ShowInventory(Inventory inventory)
        {
            if (OpenedWindowManager.Instance.CanOpen(this) == false)
                return;
            OpenedWindowManager.Instance.MarkAsOpened(this);
            IsShowing = true;
            Debug.Log(cells.Length);
            Debug.Log(inventory.Slots.Count);
            inventoryWindow.SetActive(true);
            for (int i = 0; i < inventory.MaxSlots; i++)
            {
                cells[i].Slot = inventory.Slots[i];
            }
        }

        public void HideInventory()
        {
            OpenedWindowManager.Instance.RemoveMarkAsOpened(this);
            IsShowing = false;
            inventoryWindow.SetActive(false);
        }
    }
}