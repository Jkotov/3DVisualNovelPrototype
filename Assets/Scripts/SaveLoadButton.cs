using System.Collections;
using SaveSystem;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour
{
    public void Save()
    {
        if (MainMenuController.Instance.sceneLoaded == false)
            return;
        SaveLoad.Save(QuickSave.Key);
    }

    public void Load()
    {
        StartCoroutine(LoadRoutine());
    }
    
    private IEnumerator LoadRoutine()
    {
        yield return StartCoroutine(SaveLoad.TryLoad(QuickSave.Key));
        MainMenuController.Instance.Hide();
    }
}
