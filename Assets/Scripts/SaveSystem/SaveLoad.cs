using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Destroyables;
using InventorySystem;
using Moveables;
using QuestSystem;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public static class SaveLoad
    {
        private static bool isLoading { get; set; } = false;
        
        public static IEnumerator Save(string key)
        {
            if (isLoading)
                yield break;
            isLoading = true;
            PlayerPrefs.SetString(key, "");
            PlayerPrefs.SetString($"{key} activeQuests",
                JsonUtility.ToJson(new QuestsListWrapper(QuestManager.Instance.ActiveQuests.ToList())));
            PlayerPrefs.SetString($"{key} finishedQuests",
                JsonUtility.ToJson(new QuestsListWrapper(QuestManager.Instance.FinishedQuests.ToList())));
            PlayerPrefs.SetString($"{key} thoughts", 
                JsonUtility.ToJson(new ThoughtsListWrapper(QuestManager.Instance.Thoughts.ToList())));
            
            var asyncOperationInventory = Addressables.LoadAssetsAsync<InventoryStorage>("InventoryStorage", null);
            var asyncOperationQuest = Addressables.LoadAssetsAsync<Quest>("Quest", null);
            var asyncOperationQuestTask = Addressables.LoadAssetsAsync<QuestTask>("Quest", null);
            
            while (!asyncOperationInventory.IsDone)
            {
                yield return null;
            }
            foreach (var storage in asyncOperationInventory.Result)
            {
                var wrapper = new InventorySlotsWrapper(storage.Slots.ToList());
                var json = JsonUtility.ToJson(wrapper);
                PlayerPrefs.SetString($"{key} {storage.name}", json);
            }
            
            
            while (!asyncOperationQuest.IsDone)
            {
                yield return null;
            }
            foreach (var quest in asyncOperationQuest.Result)
            {
                var json = JsonUtility.ToJson(quest.questStatus);
                PlayerPrefs.SetString($"{key} {quest.name}", json);
            }

            
            while (!asyncOperationQuestTask.IsDone)
            {
                yield return null;
            }
            foreach (var questTask in asyncOperationQuestTask.Result)
            {
                var json = JsonUtility.ToJson(questTask.status);
                PlayerPrefs.SetString($"{key} {questTask.name}", json);
            }

            var destroyed = DestroyableObjectsManager.Instance.DestroyedObjects.ToList();
            PlayerPrefs.SetString($"{key} destroyed", JsonUtility.ToJson(new StringsListWrapper(destroyed)));
            
            var moveable = JsonUtility.ToJson(new MoveablesManagerAdapter(new Dictionary<string, PositionRotation>(MoveableManager.Instance.Positions)));
            Debug.Log(moveable);
            PlayerPrefs.SetString($"{key} moveable", moveable);

            PlayerPrefs.SetString($"{key} scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            isLoading = false;
            Debug.Log($"{key} Saved");
        }

        public static IEnumerator TryLoad(string key)
        {
            if (PlayerPrefs.HasKey(key) == false || isLoading)
            {
                Debug.Log($"{key} Not Found");
                yield break;
            }

            isLoading = true;
            var currentScene = SceneManager.GetActiveScene();
            var asyncOperation = SceneManager.LoadSceneAsync("LoadingScene");
            
            var asyncOperationInventory = Addressables.LoadAssetsAsync<InventoryStorage>("InventoryStorage", null);
            var asyncOperationQuest = Addressables.LoadAssetsAsync<Quest>("Quest", null);
            var asyncOperationQuestTask = Addressables.LoadAssetsAsync<QuestTask>("Quest", null);
           
            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            if (currentScene.isLoaded)
            {       asyncOperation = SceneManager.UnloadSceneAsync(currentScene);
                while (!asyncOperation.isDone)
                { 
                    yield return null;
                }
            }
            
            while (!asyncOperationInventory.IsDone)
            {
                yield return null;
            }
            foreach (var storage in asyncOperationInventory.Result)
            {
                var slots = JsonUtility.FromJson<InventorySlotsWrapper>(
                    PlayerPrefs.GetString($"{key} {storage.name}"));
                storage.LoadInventory(slots.slots);
            }
            
            
            while (!asyncOperationQuest.IsDone)
            {
                yield return null;
            }
            foreach (var quest in asyncOperationQuest.Result)
            {
                var status = JsonUtility.FromJson<QuestStatus>(
                    PlayerPrefs.GetString($"{key} {quest.name}"));
                quest.questStatus = status;
            }

            while (!asyncOperationQuestTask.IsDone)
            {
                yield return null;
            }
            foreach (var questTask in asyncOperationQuestTask.Result)
            {
                var status = JsonUtility.FromJson<QuestStatus>(
                    PlayerPrefs.GetString($"{key} {questTask.name}"));
                questTask.status = status;
            }

            var destroyed = JsonUtility.FromJson<StringsListWrapper>(PlayerPrefs.GetString($"{key} destroyed"));
            DestroyableObjectsManager.Instance.Load(destroyed.strings.ToHashSet()); 
            MoveableManager.Instance.Load(JsonUtility.FromJson<MoveablesManagerAdapter>(PlayerPrefs.GetString($"{key} moveable")).Dict);
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString($"{key} scene"));

            var activeQuests = JsonUtility.FromJson<QuestsListWrapper>(PlayerPrefs.GetString($"{key} activeQuests"));
            var finishedQuests = JsonUtility.FromJson<QuestsListWrapper>(PlayerPrefs.GetString($"{key} finishedQuests"));
            var thoughts = JsonUtility.FromJson<ThoughtsListWrapper>(PlayerPrefs.GetString($"{key} thoughts"));
            QuestManager.Instance.LoadQuests(activeQuests.quests, finishedQuests.quests, thoughts.thoughts);
            MainMenuController.Instance.sceneLoaded = true;
            isLoading = false;
            MainMenuController.Instance.Hide();
            Debug.Log($"{key} Loaded");
        }

        public static IEnumerator Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.DeleteKey($"{key} activeQuests");
            PlayerPrefs.DeleteKey($"{key} finishedQuests");
            PlayerPrefs.DeleteKey($"{key} thoughts");
            
            var asyncOperationInventory = Addressables.LoadAssetsAsync<InventoryStorage>("InventoryStorage", null);
            var asyncOperationQuest = Addressables.LoadAssetsAsync<Quest>("Quest", null);
            var asyncOperationQuestTask = Addressables.LoadAssetsAsync<QuestTask>("Quest", null);
            
            while (!asyncOperationInventory.IsDone)
            {
                yield return null;
            }
            foreach (var storage in asyncOperationInventory.Result)
            {
                PlayerPrefs.DeleteKey($"{key} {storage.name}");
            }
            
            while (!asyncOperationQuest.IsDone)
            {
                yield return null;
            }
            foreach (var quest in asyncOperationQuest.Result)
            {
                PlayerPrefs.DeleteKey($"{key} {quest.name}");
            }

            while (!asyncOperationQuestTask.IsDone)
            {
                yield return null;
            }
            foreach (var questTask in asyncOperationQuestTask.Result)
            {
                PlayerPrefs.DeleteKey($"{key} {questTask.name}");
            }

            PlayerPrefs.DeleteKey($"{key} destroyed");
            PlayerPrefs.DeleteKey($"{key} moveable");
            PlayerPrefs.DeleteKey($"{key} scene");

            PlayerPrefs.Save();
            Debug.Log($"{key} Deleted");
        }


        [Serializable]
        internal class InventorySlotsWrapper
        {
            [SerializeField] public List<InventorySlot> slots;

            public InventorySlotsWrapper(List<InventorySlot> slots)
            {
                this.slots = slots;
            }
        }
        [Serializable]
        internal class StringsListWrapper
        {
            [SerializeField] public List<string> strings;

            public StringsListWrapper(List<string> strings)
            {
                this.strings = strings;
            }
        }
        
        [Serializable]
        internal class QuestsListWrapper
        {
            [SerializeField] public List<Quest> quests;

            public QuestsListWrapper(List<Quest> quests)
            {
                this.quests = quests;
            }
        }
        
        [Serializable]
        internal class ThoughtsListWrapper
        {
            [SerializeField] public List<Thought> thoughts;

            public ThoughtsListWrapper(List<Thought> thoughts)
            {
                this.thoughts = thoughts;
            }
        }

        [Serializable]
        class MoveablesManagerAdapter
        {
            public Dictionary<string, PositionRotation> Dict
            {
                get
                {
                    if (dict == null)
                    {
                        dict = new Dictionary<string, PositionRotation>();
                        foreach (var item in list)
                        {
                            dict.Add(item.id, item.pos);
                        }
                    }

                    return dict;
                }
            }
            [SerializeField] public List<PosRotId> list;
            private Dictionary<string, PositionRotation> dict;

            public MoveablesManagerAdapter(Dictionary<string, PositionRotation> dictionary)
            {
                dict = dictionary;
                list = new List<PosRotId>();
                foreach (var pos in dict)
                {
                    list.Add(new PosRotId()
                    {
                        id = pos.Key,
                        pos = pos.Value,
                    });
                }
            }
        }

        [Serializable]
        struct PosRotId
        {
            [SerializeField] public string id;
            [SerializeField] public PositionRotation pos;
        }
    }
}
