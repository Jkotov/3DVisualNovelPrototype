using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public List<QuestTask> tasks;
    public int CurrentTask { get; private set; }
    public QuestStatus questStatus;
    public void StartQuest()
    {
        questStatus = QuestStatus.Started;
        QuestManager.Instance.StartQuest(this);
    }

    public void FinishQuest()
    {
        questStatus = QuestStatus.Finished;
        QuestManager.Instance.FinishQuest(this);
    }

    public void ChangeTask()
    {
        
    }
}
