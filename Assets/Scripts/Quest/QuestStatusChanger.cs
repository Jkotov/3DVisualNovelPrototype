using UnityEngine;

namespace Quest
{
    public class QuestStatusChanger : MonoBehaviour
    {
        [SerializeField] private Quest quest;
        [SerializeField] private QuestTask questTask;
        [SerializeField] private QuestStatus status;
        
        [ContextMenu("Start Quest")]
        public void StartQuest()
        {
            quest.StartQuest();
        }
        
        [ContextMenu("Finish Quest")]
        public void FinishQuest()
        {
            quest.FinishQuest();
        }
        
        [ContextMenu("Change Quest Task")]
        public void ChangeQuestTask()
        {
            quest.ChangeTaskStatus(questTask, status);
        }

        [ContextMenu("FinishTasks")]
        public void FinishTasks()
        {
            quest.SetActiveTasksAsFinished();
        }
    }
}
