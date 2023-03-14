using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "ScriptableObjects/Quest")]
    [Serializable]
    public class Quest : ScriptableObject
    {
        [TextArea]
        [SerializeField] public string description;
        [SerializeField] public int priority;
        [SerializeField] public QuestStatus questStatus = QuestStatus.NotStarted;
        [SerializeField] private List<QuestTask> tasks;
        [SerializeField] private List<StatusForThought> thoughts;
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
            foreach (var thought in thoughts)
            {
                if (thought.targetStatus != status)
                    continue;
                thought.thought.AddThought();
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
            if (task.status == status)
                return;
            foreach (var thought in task.thoughts)
            {
                if (thought.targetStatus != status)
                    continue;
                thought.thought.AddThought();
            }
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
