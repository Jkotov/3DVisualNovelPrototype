using SaveSystem;
using UI;
using UnityEngine;

public class SaveLoadButton : MonoBehaviour
{
    public void Save()
    {
        if (MainMenuController.Instance.sceneLoaded == false)
            return;
        QuickSave.Instance.Save(QuickSave.Key);
    }

    public void Load()
    {
        QuickSave.Instance.Load(QuickSave.Key);
    }
}
