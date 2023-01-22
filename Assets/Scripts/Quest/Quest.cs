using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Quest
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]
    public class Quest : ScriptableObject
    {
        public int priority;
        [NonSerialized] public QuestStatus questStatus = QuestStatus.NotStarted;
        [SerializeField] private List<QuestTask> tasks;
        public ReadOnlyCollection<QuestTask> Tasks => tasks.AsReadOnly();

        public void StartQuest()
        {
            questStatus = QuestStatus.Started;
            QuestManager.Instance.StartQuest(this);
        }

        public void FinishQuest()
        {
            questStatus = QuestStatus.Finished;
            QuestManager.Instance.FinishQuest(this);
        }

        public void FailQuest()
        {
            questStatus = QuestStatus.Failed;
            QuestManager.Instance.FinishQuest(this);
        }
    
        public void SetActiveTasksAsFinished()
        {
            foreach (var task in tasks)
            {
                if (task.status == QuestStatus.Started)
                    task.status = QuestStatus.Finished;
            }
            QuestManager.Instance.QuestTaskUpdated(this);
        }

        public void SetActiveTask(QuestTask task)
        {
            task.status = QuestStatus.Started;
            QuestManager.Instance.QuestTaskUpdated(this);
        }

        public void SetActiveTasksAsFailed()
        {
            foreach (var task in tasks)
            {
                if (task.status == QuestStatus.Started)
                    task.status = QuestStatus.Finished;
            }
            QuestManager.Instance.QuestTaskUpdated(this);
        }
    }
}
