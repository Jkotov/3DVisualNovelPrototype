using System;
using QuestSystem;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public class QuestCondition : DialogBlockCondition
    {
        [SerializeField] private Quest quest;
        public override bool Completed => QuestManager.Instance.ActiveQuests.Contains(quest);
    }
}