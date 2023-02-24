using System;
using System.Collections.Generic;
using QuestSystem;
using TMPro;
using UnityEngine;

namespace UI.QuestBook
{
    public class QuestPage : MonoBehaviour
    {
        [SerializeField] private QuestBookTextElement questName;
        [SerializeField] private QuestBookTextElement questDescription;
        private QuestTaskText[] questTasks;

        public void SetQuest(Quest quest)
        {
            questName.Text = quest.name;
            questDescription.Text = quest.description;
            for (int i = 0; i < quest.Tasks.Count; i++)
            {
                questTasks[i].Text = quest.Tasks[i].description;
                questTasks[i].gameObject.SetActive(true);
            }

            for (int i = quest.Tasks.Count; i < questTasks.Length; i++)
            {
                questTasks[i].gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            questTasks = GetComponentsInChildren<QuestTaskText>();
        }
    }
}