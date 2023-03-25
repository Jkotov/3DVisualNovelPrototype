using System;
using System.Collections.Generic;
using QuestSystem;
using UnityEngine;
using System.Linq;

namespace Dialog
{
    [CreateAssetMenu(fileName = "New Dialog Block", menuName = "ScriptableObjects/Dialog Block")]
    public class DialogBlock : ScriptableObject
    {
        public string BlockText
        { 
            get
            {
                foreach (var text in overridedText)
                {
                    if (text.quest.questStatus == text.status)
                        return text.blockText;
                }
                return blockText;
            }
        }
        [SerializeField] private string blockText;
        public List<Actor> actors;
        public List<Answer> Answers => answers.FindAll(answer => answer.IsActive);
        [SerializeField] private List<Answer> answers;
        [SerializeField] private List<QuestStatusChanger> statusChangers;
        [SerializeField] private List<InventoryDialogAction> inventoryActions;
        [SerializeField] private List<TextForStatus> overridedText;

        public void DoActions()
        {
            foreach (var statusChanger in statusChangers)
            {
                statusChanger.ChangeQuestStatus();
                statusChanger.ChangeQuestTask();
            }

            foreach (var inventoryAction in inventoryActions)
            {
                inventoryAction.ChangeInventoryItemsCount();
            }
        }
        
        [Serializable]
        public struct TextForStatus
        {
            [SerializeField] public string blockText;
            [SerializeField] public Quest quest;
            [SerializeField] public QuestStatus status;
        }
    }
    
    [Serializable]
    public class Answer
    {
        public string text;
        public DialogBlock nextDialogBlock;
        [SerializeField] private List<QuestCondition> questConditions;
        [SerializeField] private List<ItemCondition> itemConditions;
        public bool IsActive => questConditions.TrueForAll(condition => condition.Completed) &&
                                itemConditions.TrueForAll(condition => condition.Completed);
    }

}