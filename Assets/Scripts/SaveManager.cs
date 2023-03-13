using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using QuestSystem;
using UnityEditor;
using UnityEngine;

public class SaveManager
{
    public void Save(string key)
    {
        PlayerPrefs.SetString($"{key} activeQuests",JsonUtility.ToJson(QuestManager.Instance.ActiveQuests.ToList()));
        PlayerPrefs.SetString($"{key} finishedQuests",JsonUtility.ToJson(QuestManager.Instance.FinishedQuests.ToList()));
        
        var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
        foreach (var storageName in assetNames)
        {
            var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
            var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
            PlayerPrefs.SetString($"{key} {inventoryStorage.name}", JsonUtility.ToJson(inventoryStorage.Slots.ToList()));
        }
        
        PlayerPrefs.SetString($"{key} destroyed", JsonUtility.ToJson(DestroyableObjectsManager.Instance.DestroyedObjects.ToHashSet()));
        
        PlayerPrefs.Save();
    }

    public void Load(string key)
    {
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
        
        DestroyableObjectsManager.Instance.Load(JsonUtility.FromJson<HashSet<string>>(PlayerPrefs.GetString($"{key} destroyed")));
    }
}
