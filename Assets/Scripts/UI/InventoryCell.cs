using System;
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
                image.sprite = slot.item != null ? slot.item.sprite : clearSprite;
                countTextMesh.text = slot.count.ToString();
            }
        }
        [SerializeField] private TextMeshProUGUI countTextMesh;
        [SerializeField] private Image image;
        private InventorySlot slot;
        private Sprite clearSprite;

        private void Awake()
        {
            clearSprite = image.sprite;
        }

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