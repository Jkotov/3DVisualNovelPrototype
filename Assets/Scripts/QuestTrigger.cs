using Quest;
using UnityEngine;

[RequireComponent(typeof(QuestStatusChanger))]
public class QuestTrigger : MonoBehaviour
{
    private QuestStatusChanger questStatusChanger;
    public void StartQuest()
    {
        questStatusChanger.StartQuest();
    }

    private void Awake()
    {
        questStatusChanger = GetComponent<QuestStatusChanger>();
    }
}
