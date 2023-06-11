using SaveSystem;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private const string SaveKey = "NewGame";

    private void Start()
    {
        QuickSave.Instance.Save(SaveKey);
    }

    public void Load()
    {
        QuickSave.Instance.Load(SaveKey);
    }
}