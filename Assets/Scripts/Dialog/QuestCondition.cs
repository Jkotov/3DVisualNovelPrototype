using System;
using QuestSystem;
using UnityEngine;

namespace Dialog
{
    [Serializable]
    public class QuestCondition : DialogBlockCondition
    {
        [SerializeField] private Quest quest;
        [SerializeField] private QuestStatus status;
        public override bool Completed => quest.questStatus == status;
    }
}