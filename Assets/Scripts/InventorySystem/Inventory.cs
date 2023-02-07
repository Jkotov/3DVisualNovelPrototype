using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private InventoryStorage storage;
        public ReadOnlyCollection<InventorySlot> Slots => storage.Slots;
        public int MaxSlots => storage.MaxSlots;

        public bool TryAddItems(InventoryItem item, int count)
        {
            if (!CheckSlots(item, count))
                return false;
            storage.AddItem(item, count);
            return true;
        }

        public bool TryRemove(List<Tuple<InventoryItem, int>> itemsForRemove)
        {
            if (CheckItemsExist(itemsForRemove) == false)
                return false;
            foreach (var item in itemsForRemove)
            {
                TryRemove(item.Item1, item.Item2);
            }
            return true;
        }

        public bool TryRemove(InventoryItem itemForRemove, int count)
        {
            if (CheckItemExist(itemForRemove, count) == false)
                return false;
            storage.RemoveItem(itemForRemove, count);
            return true;
        }

        public bool CheckItemsExist(List<Tuple<InventoryItem, int>> items)
        {
            foreach (var item in items)
            {
                if (CheckItemExist(item.Item1, item.Item2) == false)
                    return false;
            }
            return true;
        }

        public bool CheckItemExist(InventoryItem item, int count)
        {
            int sum = 0;
            foreach (var slot in Slots)
            {
                if (slot.item == item)
                {
                    sum += slot.count;
                }
            }
            return sum >= count;
        }

        private bool CheckSlots(InventoryItem item, int count)
        {
            var freeSlots = 0;
            if (freeSlots > count)
                return true;
            foreach (var slot in Slots)
            {
                if (slot.item == item)
                {
                    freeSlots += item.maxStack - slot.count;
                }
                if (slot.item == null)
                {
                    freeSlots += item.maxStack;
                }
            }
            return freeSlots > count;
        }
    }
}