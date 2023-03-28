using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory Storage", menuName = "ScriptableObjects/Inventory Storage")]
    public class InventoryStorage : ScriptableObject
    {
        [SerializeField] private List<InventorySlot> slots;

        public void LoadInventory(List<InventorySlot> inventorySlots)
        {
            slots = inventorySlots;
        }
        
        public void AddItem(InventoryItem item, int count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                if (slot.item == item)
                {
                    if (item.maxStack >= count + slot.count)
                    {
                        slot.count += count;
                        slots[i] = slot;
                        return;
                    }
                    count -= item.maxStack - slot.count;
                    slot.count = item.maxStack;
                    slots[i] = slot;
                }
                if (slot.item == null)
                {
                    slot.item = item;
                    if (item.maxStack > count)
                    {
                        slot.count = count;
                        slots[i] = slot;
                        return;
                    }
                    else
                    {
                        slot.count = item.maxStack;
                        slots[i] = slot;
                        count -= item.maxStack;
                    }
                }
            }
        }

        public void RemoveItem(InventoryItem item, int count)
        {
            while (count > 0)
            {
                var slotPos = FindSlotWithMinItemsCount(item);
                if (slotPos == -1)
                    return;
                var slot = slots[slotPos];
                var diff = Math.Min(count, slot.count);
                count -= diff;
                slot.count -= diff;
                if (slot.count == 0)
                    slot.item = null;
                slots[slotPos] = slot;
                Debug.Log(slot.count);
            }
        }

        private int FindSlotWithMinItemsCount(InventoryItem item)
        {
            int res = -1;
            int min = item.maxStack + 1;
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].count <= min && Slots[i].item == item)
                {
                    res = i;
                    min = Slots[i].count;
                }
            }
            return res;
        }
        public int MaxSlots => Slots.Count;
        public ReadOnlyCollection<InventorySlot> Slots => slots.AsReadOnly();
    }
}