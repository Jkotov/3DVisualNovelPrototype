using InventorySystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ShowInventoryOnClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Inventory inventory;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("pressed");
            if (!InventoryUI.Instance.IsShowing)
                InventoryUI.Instance.ShowInventory(inventory);
            else
                InventoryUI.Instance.HideInventory();
        }
    }
}