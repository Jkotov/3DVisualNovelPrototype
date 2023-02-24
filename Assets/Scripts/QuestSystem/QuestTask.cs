using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Task", menuName = "ScriptableObjects/QuestTask")]
    public class QuestTask : ScriptableObject
    {
        public int priority;
        public string description;
        public List<StatusForThought> thoughts;
        [NonSerialized] public QuestStatus status = QuestStatus.NotStarted;
    }
}