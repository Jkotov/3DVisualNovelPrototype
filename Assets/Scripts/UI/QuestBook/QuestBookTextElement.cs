using System;
using TMPro;
using UnityEngine;

namespace UI.QuestBook
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    [RequireComponent(typeof(RectTransform))]
    public class QuestBookTextElement : MonoBehaviour
    {
        public RectTransform RectTransform { get; private set; }
        private TextMeshProUGUI textMeshProUGUI;
        public string Text
        {
            get => textMeshProUGUI.text;
            set
            {
                textMeshProUGUI.text = value;
                CalcRect();
            }
        }
        
        private void CalcRect()
        {
            RectTransform.sizeDelta = new Vector2(RectTransform.rect.width, textMeshProUGUI.preferredHeight);
        }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }
    }
}