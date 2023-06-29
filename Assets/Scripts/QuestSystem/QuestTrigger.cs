using QuestSystem;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private QuestStatusChanger questStatusChanger;
    public void StartQuest()
    {
        questStatusChanger.ChangeQuestStatus();
    }
}
