using System;
using InventorySystem;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public class InventoryDialogAction
    {
        [SerializeField] private InventoryItem inventoryItem;
        [SerializeField] private int count;

        public void ChangeInventoryItemsCount()
        {
            if (count > 0)
                DialogManager.Instance.CurrentInventory.TryAddItems(inventoryItem, count);
            else
                DialogManager.Instance.CurrentInventory.TryRemove(inventoryItem, count);
        }
    }
}