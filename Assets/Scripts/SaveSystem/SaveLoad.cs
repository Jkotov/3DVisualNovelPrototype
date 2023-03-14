using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using QuestSystem;
using UnityEditor;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveLoad
    {
        public static void Save(string key)
        {
            PlayerPrefs.SetString(key, "");
            PlayerPrefs.SetString($"{key} activeQuests",JsonUtility.ToJson(QuestManager.Instance.ActiveQuests.ToList()));
            PlayerPrefs.SetString($"{key} finishedQuests",JsonUtility.ToJson(QuestManager.Instance.FinishedQuests.ToList()));
        
            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                var data = inventoryStorage.Slots.ToList();
                PlayerPrefs.SetString($"{key} {inventoryStorage.name}", JsonUtility.ToJson(data));
            }

            var destroyed = DestroyableObjectsManager.Instance.DestroyedObjects.ToList();
            PlayerPrefs.SetString($"{key} destroyed", JsonUtility.ToJson(destroyed));
        
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
            var activeQuests = JsonUtility.FromJson<List<Quest>>(PlayerPrefs.GetString($"{key} activeQuests"));
            var finishedQuests = JsonUtility.FromJson<List<Quest>>(PlayerPrefs.GetString($"{key} finishedQuests"));
            QuestManager.Instance.LoadQuests(activeQuests, finishedQuests);
        
            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                var slots = JsonUtility.FromJson<List<InventorySlot>>(PlayerPrefs.GetString($"{key} {inventoryStorage.name}"));
                inventoryStorage.LoadInventory(slots);
            }

            var destroyed = JsonUtility.FromJson<List<string>>(PlayerPrefs.GetString($"{key} destroyed"));
            DestroyableObjectsManager.Instance.Load(destroyed.ToHashSet());
            Debug.Log($"{key} Loaded");
            return true;
        }

        public static void Delete(string key)
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.DeleteKey($"{key} activeQuests");
            PlayerPrefs.DeleteKey($"{key} finishedQuests");
        
            var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
            foreach (var storageName in assetNames)
            {
                var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
                var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
                PlayerPrefs.DeleteKey($"{key} {inventoryStorage.name}");
            }
        
            PlayerPrefs.DeleteKey($"{key} destroyed");
        
            PlayerPrefs.Save();
            Debug.Log($"{key} Deleted");
        }
    }
}
