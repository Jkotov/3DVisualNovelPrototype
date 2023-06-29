using System;
using System.Collections;
using System.Collections.Generic;
using Moveables;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace PlayerActions
{
    [RequireComponent(typeof(Moveable))]
    public class SceneChangeAction : MonoBehaviour
    {
        private readonly List<SceneLoadData> scenesForLoad = new List<SceneLoadData>();
        private Moveable moveable;
        
        private void Awake()
        {
            moveable = GetComponent<Moveable>();
        }

        public void TryLoadScene(InputAction.CallbackContext callbackContext)
        {
            if (!callbackContext.performed)
                return;
            if (scenesForLoad.Count > 0)
            {
                if (TryGetComponent(out CharacterController controller))
                    controller.enabled = false;
                transform.position = scenesForLoad[0].PositionRotation.pos;
                transform.rotation = scenesForLoad[0].PositionRotation.rot;
                SceneManager.LoadScene(scenesForLoad[0].Scene);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out SceneLoadData sceneLoader))
            {
                scenesForLoad.Add(sceneLoader);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out SceneLoadData sceneLoader))
            {
                scenesForLoad.RemoveAll(scene => scene == sceneLoader);
            }
        }
    }
}
