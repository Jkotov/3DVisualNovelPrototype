using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventoryCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent<InventoryCell> PointerEntered;
        public UnityEvent PointerExeted;
        public InventorySlot Slot
        {
            get => slot;
            set
            {
                slot = value;
                if (slot.item != null)
                    image.sprite = slot.item.sprite;
                countTextMesh.text = slot.count.ToString();
            }
        }
        [SerializeField] private TextMeshProUGUI countTextMesh;
        [SerializeField] private Image image;
        private InventorySlot slot;

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered?.Invoke(this);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExeted?.Invoke();
        }
    }
}