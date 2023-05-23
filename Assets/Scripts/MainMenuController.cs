using UI;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; private set; }
    public bool IsShowing { get; private set; }
    public bool sceneLoaded;

    public void Show()
    {
        if (OpenedWindowManager.Instance.CanOpen(this) == false)
            return;
        OpenedWindowManager.Instance.MarkAsOpened(this);
        IsShowing = true;
    }

    public void Hide()
    {
        if (sceneLoaded == false)
            return;
        
        OpenedWindowManager.Instance.RemoveMarkAsOpened(this);
        IsShowing = false;
    }
    
    private void Awake()
    {
        OpenedWindowManager.Instance.MarkAsOpened(this);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform);
        }
    }
}
