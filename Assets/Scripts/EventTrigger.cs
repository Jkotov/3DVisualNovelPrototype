using System;
using System.Collections;
using Controller;
using Dialog;
using UnityEngine;

public abstract class EventTrigger : MonoBehaviour
{
    public KeyCode keyCode = KeyCode.B;
    private bool keyPressed;
    private Coroutine coroutine;

    protected abstract void StartEvent();
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMove c))
            coroutine = StartCoroutine(WaitForKeyPressed());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMove c))
            StopCoroutine(coroutine);
    }

    private IEnumerator WaitForKeyPressed()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(keyCode))
            {
                StartEvent();
            }
        }
    }
}
