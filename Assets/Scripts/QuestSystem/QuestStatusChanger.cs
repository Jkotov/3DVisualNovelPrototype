using System;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class QuestStatusChanger
    {
        [SerializeField] private Quest quest;
        [SerializeField] private bool isChangeQuest;
        [SerializeField] private QuestStatus questStatus;
        [SerializeField] private bool isChangeQuestTask;
        [SerializeField] private QuestTask questTask;
        [SerializeField] private QuestStatus taskStatus;
        
        public void ChangeQuestStatus()
        {
            if (isChangeQuest)
                quest.TryChangeQuestStatus(questStatus);
        }

        public void ChangeQuestTask()
        {
            if (isChangeQuestTask)
                quest.ChangeTaskStatus(questTask, taskStatus);
        }
    }
}
