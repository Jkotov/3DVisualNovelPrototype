using UnityEngine;
using UnityEngine.InputSystem;

namespace SaveSystem
{
    public class QuickSave : MonoBehaviour
    {
        private const string Key = "QuickSave";

        public void Save(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                SaveLoad.Save(Key);
            }
        }

        public void Load(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                SaveLoad.TryLoad(Key);
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