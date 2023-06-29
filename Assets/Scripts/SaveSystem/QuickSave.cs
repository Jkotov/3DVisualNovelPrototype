using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SaveSystem
{
    public class QuickSave : MonoBehaviour
    {
        public const string Key = "QuickSave";
        public static QuickSave Instance { get; private set; }
        
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
                StartCoroutine(SaveLoad.Delete(Key));
            }
        }
        
        public void Save(string Key)
        {
            StartCoroutine(SaveLoad.Save(Key));
        }

        public void Load(string Key)
        {
            StartCoroutine(SaveLoad.TryLoad(Key));
        }

        public void Delete(string Key)
        {
            StartCoroutine(SaveLoad.Delete(Key));
        }
    }
}