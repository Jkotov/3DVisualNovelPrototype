using UnityEngine;
using UnityEngine.InputSystem;

namespace SaveSystem
{
    public class QuickSave : MonoBehaviour
    {
        public const string Key = "QuickSave";
        private static QuickSave instance;
        
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(transform);
            }
        }
        
        public void Save(InputAction.CallbackContext callbackContext)
        {
            if (MainMenuController.Instance.sceneLoaded == false)
                return;
            if (callbackContext.performed)
            {
                StartCoroutine(SaveLoad.Save(Key));
            }
        }

        public void Load(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                StartCoroutine(SaveLoad.TryLoad(Key));
            }
        }

        public void Delete(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                SaveLoad.Delete(Key);
            }
        }
    }
}