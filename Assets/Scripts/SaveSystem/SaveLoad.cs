using System;
using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using QuestSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public static class SaveLoad
    {
        public static void Save(string key)
        {
            PlayerPrefs.SetString(key, "");
            PlayerPrefs.SetString($"{key} activeQuests",
                JsonUtility.ToJson(new QuestsListWrapper(QuestManager.Instance.ActiveQuests.ToList())));
            PlayerPrefs.SetString($"{key} finishedQuests",
                JsonUtility.ToJson(new QuestsListWrapper(QuestManager.Instance.FinishedQuests.ToList())));
            PlayerPrefs.SetString($"{key} thoughts", 
                JsonUtility.ToJson(new ThoughtsListWrapper(QuestManager.Instance.Thoughts.ToList())));

            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                var wrapper = new InventorySlotsWrapper(inventoryStorage.Slots.ToList());
                var json = JsonUtility.ToJson(wrapper);
                PlayerPrefs.SetString($"{key} {inventoryStorage.name}", json);
            }

            var destroyed = DestroyableObjectsManager.Instance.DestroyedObjects.ToList();
            PlayerPrefs.SetString($"{key} destroyed", JsonUtility.ToJson(new StringsListWrapper(destroyed)));
            PlayerPrefs.SetString($"{key} scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
            Debug.Log($"{key} Saved");
        }

        public static bool TryLoad(string key)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                Debug.Log($"{key} Not Found");
                return false;
            }

            var activeQuests = JsonUtility.FromJson<QuestsListWrapper>(PlayerPrefs.GetString($"{key} activeQuests"));
            var finishedQuests = JsonUtility.FromJson<QuestsListWrapper>(PlayerPrefs.GetString($"{key} finishedQuests"));
            var thoughts = JsonUtility.FromJson<ThoughtsListWrapper>(PlayerPrefs.GetString($"{key} thoughts"));
            QuestManager.Instance.LoadQuests(activeQuests.quests, finishedQuests.quests, thoughts.thoughts);

            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                var slots = JsonUtility.FromJson<InventorySlotsWrapper>(
                    PlayerPrefs.GetString($"{key} {inventoryStorage.name}"));
                inventoryStorage.LoadInventory(slots.slots);
            }

            var destroyed = JsonUtility.FromJson<StringsListWrapper>(PlayerPrefs.GetString($"{key} destroyed"));
            DestroyableObjectsManager.Instance.Load(destroyed.strings.ToHashSet());
            SceneManager.LoadScene(PlayerPrefs.GetString($"{key} scene"));
            Debug.Log($"{key} Loaded");
            return true;
        }

        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.DeleteKey($"{key} activeQuests");
            PlayerPrefs.DeleteKey($"{key} finishedQuests");
            PlayerPrefs.DeleteKey($"{key} thoughts");

            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                PlayerPrefs.DeleteKey($"{key} {inventoryStorage.name}");
            }

            PlayerPrefs.DeleteKey($"{key} destroyed");
            PlayerPrefs.DeleteKey($"{key} scene");

            PlayerPrefs.Save();
            Debug.Log($"{key} Deleted");
        }


        [Serializable]
        class InventorySlotsWrapper
        {
            [SerializeField] public List<InventorySlot> slots;

            public InventorySlotsWrapper(List<InventorySlot> slots)
            {
                this.slots = slots;
            }
        }
        [Serializable]
        class StringsListWrapper
        {
            [SerializeField] public List<string> strings;

            public StringsListWrapper(List<string> strings)
            {
                this.strings = strings;
            }
        }
        
        [Serializable]
        class QuestsListWrapper
        {
            [SerializeField] public List<Quest> quests;

            public QuestsListWrapper(List<Quest> quests)
            {
                this.quests = quests;
            }
        }
        
        [Serializable]
        class ThoughtsListWrapper
        {
            [SerializeField] public List<Thought> thoughts;

            public ThoughtsListWrapper(List<Thought> thoughts)
            {
                this.thoughts = thoughts;
            }
        }
    }
}
