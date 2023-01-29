using TMPro;
using UnityEngine;

namespace Dialog
{
    public class TextWindow : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        [SerializeField] private float heightOffset = 50f;

        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        public void UpdateText(string s)
        {
            textMeshProUGUI.text = s;
            var sizeDelta = RectTransform.sizeDelta;
            sizeDelta.y = textMeshProUGUI.preferredHeight + heightOffset;
            RectTransform.sizeDelta = sizeDelta;
        }
        
        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
        }
    }
}