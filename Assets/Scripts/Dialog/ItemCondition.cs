using System;
using InventorySystem;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public class ItemCondition : DialogBlockCondition
    {
        [SerializeField] private InventoryItem item;
        [SerializeField] private int count;
        public override bool Completed => DialogManager.Instance.CurrentInventory.CheckItemExist(item, count);
    }
}