using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneVillage : MonoBehaviour
{
    [SerializeField] private Scene scene;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene(scene.name);
    }
}
