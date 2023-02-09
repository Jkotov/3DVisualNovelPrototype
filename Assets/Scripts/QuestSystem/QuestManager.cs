using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Events;

namespace QuestSystem
{
    public class QuestManager
    {
        public static QuestManager Instance { get; } = new QuestManager();
        public ReadOnlyCollection<Quest> ActiveQuests => activeQuests.AsReadOnly();
        public ReadOnlyCollection<Quest> FinishedQuests => activeQuests.AsReadOnly();
        public UnityEvent<Quest> questStatusUpdated = new UnityEvent<Quest>();
        public UnityEvent<Quest> questTaskUpdated = new UnityEvent<Quest>();

        private List<Quest> activeQuests = new List<Quest>();
        private List<Quest> finishedQuests = new List<Quest>();
    
        public void StartQuest(Quest quest)
        {
            if (ActiveQuests.Contains(quest) || FinishedQuests.Contains(quest))
                return;
            questStatusUpdated?.Invoke(quest);
            activeQuests.Add(quest);
        }

        public void FinishQuest(Quest quest)
        {
            if (!ActiveQuests.Contains(quest))
                return;
            questStatusUpdated?.Invoke(quest);
            finishedQuests.Add(quest);
            activeQuests.Remove(quest);
        }

        public void QuestTaskUpdated(Quest quest)
        {
            questTaskUpdated?.Invoke(quest);
        }

        public void LoadQuests(IEnumerable<Quest> active, IEnumerable<Quest> finished)
        {
            activeQuests = new List<Quest>(active);
            finishedQuests = new List<Quest>(finished);
        }
    
        static QuestManager()
        {
        }

        private QuestManager()
        {
        }
    }
}