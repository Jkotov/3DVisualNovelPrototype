using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]
    public class Quest : ScriptableObject
    {
        public string description;
        public int priority;
        [NonSerialized] public QuestStatus questStatus = QuestStatus.NotStarted;
        [SerializeField] private List<QuestTask> tasks;
        public ReadOnlyCollection<QuestTask> Tasks => tasks.AsReadOnly();

        public bool TryChangeQuestStatus(QuestStatus status)
        {
            if (status == questStatus)
                return false;
            questStatus = status;
            switch (status)
            {
                case QuestStatus.Started:
                    QuestManager.Instance.StartQuest(this);
                    break;
                case QuestStatus.Finished:
                    QuestManager.Instance.FinishQuest(this);
                    break;
            }
            return true;
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

        public void ChangeTaskStatus(QuestTask task, QuestStatus status)
        {
            task.status = status;
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
