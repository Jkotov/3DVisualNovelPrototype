using System;
using UnityEngine;

namespace Quest
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Task", menuName = "ScriptableObjects/QuestTask")]
    public class QuestTask : ScriptableObject
    {
        public int priority;
        public string description;
        [NonSerialized] public QuestStatus status = QuestStatus.NotStarted;
    }
}