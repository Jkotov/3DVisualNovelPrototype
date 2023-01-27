using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public KeyCode onOffKey = KeyCode.Space;
    public GameObject targetObject;

    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(onOffKey))
        {
            targetObject.SetActive(!targetObject.activeSelf);
        }
        
    }
}
