using Quest;
using UnityEngine;

[RequireComponent(typeof(QuestStatusChanger))]
public class QuestTrigger : EventTrigger
{
    private QuestStatusChanger questStatusChanger;
    protected override void StartEvent()
    {
        questStatusChanger.StartQuest();
    }

    private void Awake()
    {
        questStatusChanger = GetComponent<QuestStatusChanger>();
    }
}
