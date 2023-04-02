using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using Moveables;
using QuestSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveSystem
{
    public static class SaveLoad
    {
        private static bool isLoading = false;
        
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


            assetNames = AssetDatabase.FindAssets("t:Quest");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<Quest>(assetPath);
                var json = JsonUtility.ToJson(asset.questStatus);
                PlayerPrefs.SetString($"{key} {asset.name}", json);
            }


            assetNames = AssetDatabase.FindAssets("t:QuestTask");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<QuestTask>(assetPath);
                var json = JsonUtility.ToJson(asset.status);
                PlayerPrefs.SetString($"{key} {asset.name}", json);
            }

            var destroyed = DestroyableObjectsManager.Instance.DestroyedObjects.ToList();
            PlayerPrefs.SetString($"{key} destroyed", JsonUtility.ToJson(new StringsListWrapper(destroyed)));
            
            var moveable = JsonUtility.ToJson(new MoveablesManagerAdapter(new Dictionary<string, PositionRotation>(MoveableManager.Instance.Positions)));
            Debug.Log(moveable);
            PlayerPrefs.SetString($"{key} moveable", moveable);

            PlayerPrefs.SetString($"{key} scene", SceneManager.GetActiveScene().name);
            PlayerPrefs.Save();
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


            assetNames = AssetDatabase.FindAssets("t:Quest");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<Quest>(assetPath);
                var status = JsonUtility.FromJson<QuestStatus>(
                    PlayerPrefs.GetString($"{key} {asset.name}"));
                asset.questStatus = status;
            }


            assetNames = AssetDatabase.FindAssets("t:QuestTask");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<QuestTask>(assetPath);
                var status = JsonUtility.FromJson<QuestStatus>(
                    PlayerPrefs.GetString($"{key} {asset.name}"));
                asset.status = status;
            }

            var destroyed = JsonUtility.FromJson<StringsListWrapper>(PlayerPrefs.GetString($"{key} destroyed"));
            DestroyableObjectsManager.Instance.Load(destroyed.strings.ToHashSet()); 
            MoveableManager.Instance.Load(JsonUtility.FromJson<MoveablesManagerAdapter>(PlayerPrefs.GetString($"{key} moveable")).Dict);
            SceneManager.LoadSceneAsync(PlayerPrefs.GetString($"{key} scene"));
            isLoading = false;
            Debug.Log($"{key} Loaded");
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

            assetNames = AssetDatabase.FindAssets("t:Quest");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<Quest>(assetPath);
                PlayerPrefs.DeleteKey($"{key} {asset.name}");
            }


            assetNames = AssetDatabase.FindAssets("t:QuestTask");
            foreach (var assetName in assetNames)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(assetName);
                var asset = AssetDatabase.LoadAssetAtPath<QuestTask>(assetPath);
                PlayerPrefs.DeleteKey($"{key} {asset.name}");
            }

            PlayerPrefs.DeleteKey($"{key} destroyed");
            PlayerPrefs.DeleteKey($"{key} moveable");
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
