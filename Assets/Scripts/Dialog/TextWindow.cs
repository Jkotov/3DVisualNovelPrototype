using TMPro;
using UnityEngine;

namespace Dialog
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextWindow : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
        public void UpdateText(string s)
        {
            textMeshProUGUI.text = s;
        }
        
        protected void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
    }
}