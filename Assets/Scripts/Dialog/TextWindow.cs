using TMPro;
using UnityEngine;

namespace Dialog
{
    public class TextWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        public void UpdateText(string s)
        {
            textMeshProUGUI.text = s;
        }
    }
}