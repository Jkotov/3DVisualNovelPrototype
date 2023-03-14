using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PlayerActions
{
    public class SceneChangeAction : MonoBehaviour
    {
        private readonly List<string> scenesForLoad = new List<string>();
    
        public void TryLoadScene(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.performed)
                return;
            if (scenesForLoad.Count > 0)
                SceneManager.LoadScene(scenesForLoad[0]);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneLoader sceneLoader))
            {
                scenesForLoad.Add(sceneLoader.Scene);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneLoader sceneLoader))
            {
                scenesForLoad.RemoveAll(scene => scene == sceneLoader.Scene);
            }
        }
    }
}
