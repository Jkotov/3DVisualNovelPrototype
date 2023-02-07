using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryCell : MonoBehaviour
    {
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
    }
}