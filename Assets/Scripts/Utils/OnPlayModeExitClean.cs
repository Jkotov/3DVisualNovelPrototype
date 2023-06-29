using InventorySystem;
using QuestSystem;
using SaveSystem;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

[InitializeOnLoad]
public static class OnPlayModeExitClean
{
    private const string SaveKey = "NewGame";
    
    static OnPlayModeExitClean()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }

    private static void ModeChanged (PlayModeStateChange mode)
    {
        if (mode != PlayModeStateChange.ExitingPlayMode)
            return;

        var key = SaveKey;
        

        var assetNames = AssetDatabase.FindAssets("t:InventoryStorage");
        foreach (var storageName in assetNames)
        {
            var storagePath = AssetDatabase.GUIDToAssetPath(storageName);
            var inventoryStorage = AssetDatabase.LoadAssetAtPath<InventoryStorage>(storagePath);
            var slots = JsonUtility.FromJson<SaveLoad.InventorySlotsWrapper>(
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

        var destroyed = JsonUtility.FromJson<SaveLoad.StringsListWrapper>(PlayerPrefs.GetString($"{key} destroyed"));
    }
}

#endif