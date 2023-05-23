using System;
using System.Collections;
using SaveSystem;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    private const string SaveKey = "NewGame";

    private void Start()
    {
        SaveLoad.Save(SaveKey);
    }

    public void Load()
    {
        StartCoroutine(LoadRoutine());
    }
    
    private IEnumerator LoadRoutine()
    {
        yield return StartCoroutine(SaveLoad.TryLoad(SaveKey));
        MainMenuController.Instance.Hide();
    }
}