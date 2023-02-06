using System;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using TMPro;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private int itemsCount;
    [SerializeField] private InventoryItem item;
    [SerializeField] private Inventory inventory;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [ContextMenu("AddItems")]
    public void AddItems()
    {
        Debug.Log(inventory.TryAddItems(item, itemsCount));
    }
    [ContextMenu("RemoveItems")]
    public void RemoveItems()
    {
        Debug.Log(inventory.TryRemove(item, itemsCount));
    }

    private void Awake()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            AddItems();
            textMeshProUGUI.text = "";
            foreach (var slot in inventory.Slots)
            {
                textMeshProUGUI.text += slot.count.ToString() + ' ';
            }
        }
    }
}
