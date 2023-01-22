using TMPro;
using UnityEngine;

namespace Quest
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class QuestStatusListener : MonoBehaviour
    {
        private TextMeshProUGUI textMeshProUGUI;
    
        private void Awake()
        {
            textMeshProUGUI = GetComponent<TextMeshProUGUI>();
            QuestManager.Instance.questStatusUpdated.AddListener(UpdateText);
        }

        private void UpdateText(Quest quest)
        {
            textMeshProUGUI.text = quest.description;
        }
    }
}
