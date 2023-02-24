using System;

namespace QuestSystem
{
    [Serializable]
    public struct StatusForThought
    {
        public Thought thought;
        public QuestStatus targetStatus;
    }
}