using QuestSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.QuestBook
{
    public class QuestDescriptionButton : QuestBookTextElement, IPointerClickHandler
    {
        public Quest Quest
        {
            set
            {
                Text = value.name;
                quest = value;
            }
        }
        public UnityEvent<Quest> pressed;
        private Quest quest;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("pressed");
            pressed?.Invoke(quest);
        }
    }
}