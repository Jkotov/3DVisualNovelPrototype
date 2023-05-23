using SaveSystem;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; private set; }
    [SerializeField] private GameObject menu;
    public bool IsShowing { get; private set; }
    public bool sceneLoaded;

    public void ShowHide(InputAction.CallbackContext callbackContext)
    {
        if (!callbackContext.performed)
        {
            return;
        }
        if (IsShowing)
            Hide();
        else
            Show();
        
    }

    private void Show()
    {
        if (OpenedWindowManager.Instance.CanOpen(this) == false)
            return;
        OpenedWindowManager.Instance.MarkAsOpened(this);
        IsShowing = true;
        menu.SetActive(true);
    }

    public void Hide()
    {
        if (sceneLoaded == false)
            return;
        
        menu.SetActive(false);
        OpenedWindowManager.Instance.RemoveMarkAsOpened(this);
        IsShowing = false;
    }
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(transform);
            OpenedWindowManager.Instance.MarkAsOpened(this);
        }
    }
}
