using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager
{
    public static QuestManager Instance { get; } = new QuestManager();
    public List<Quest> ActiveQuests { get; private set; }
    public List<Quest> FinishedQuests { get; private set; }
    public UnityEvent<Quest> questStatusUpdated;
    public UnityEvent<Quest> questTaskUpdated;

    public void StartQuest(Quest quest)
    {
        questStatusUpdated?.Invoke(quest);
        ActiveQuests.Add(quest);
    }

    public void FinishQuest(Quest quest)
    {
        questStatusUpdated?.Invoke(quest);
        FinishedQuests.Add(quest);
        ActiveQuests.Remove(quest);
    }

    public void QuestTaskUpdated(Quest quest)
    {
        questTaskUpdated?.Invoke(quest);
    }
    
    static QuestManager()
    {
    }

    private QuestManager()
    {
    }
}
