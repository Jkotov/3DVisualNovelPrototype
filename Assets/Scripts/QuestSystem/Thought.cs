using System;
using UnityEngine;

namespace QuestSystem
{
    [Serializable]
    public class Thought
    {
        [TextArea]
        public string description;

        public void AddThought()
        {
            QuestManager.Instance.AddThought(this);
        }
    }
}