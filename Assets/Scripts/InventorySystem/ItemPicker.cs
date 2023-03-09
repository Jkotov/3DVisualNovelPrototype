using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace InventorySystem
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        private readonly List<PickableItem> pickableItems = new List<PickableItem>();
        private readonly List<PickableItem> pickedItems = new List<PickableItem>();

        public void TryPickItem()
        {
            foreach (var pickableItem in pickableItems)
            {
                if (inventory.TryAddItems(pickableItem.InventoryItem, pickableItem.Count))
                {
                    pickedItems.Add(pickableItem);
                    Destroy(pickableItem.gameObject);
                }
            }
            foreach (var picked in pickedItems)
            {
                DestroyableObjectsManager.Instance.MarkObjectAsDestroyed(picked);
                pickableItems.RemoveAll(item => item == picked);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PickableItem pickableItem))
            {
                pickableItems.Add(pickableItem);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PickableItem pickableItem))
            {
                pickableItems.RemoveAll(item => item.InventoryItem == pickableItem.InventoryItem);
            }
        }
    }
}