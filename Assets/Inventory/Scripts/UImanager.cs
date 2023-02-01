using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject Canvas;

    private void Start()
    {
        Canvas.gameObject.SetActive(true);
        inventoryMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        InventoryControl();
    }

    private void InventoryControl()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.instance.isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Resume()
    {
        inventoryMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.instance.isPaused = false;
    }

    private void Pause()
    {
        inventoryMenu.gameObject.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.instance.isPaused = true;
    }

}
