using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneVillage : MonoBehaviour
{
    [SerializeField] private Object scene;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene(scene.name);
    }
}
