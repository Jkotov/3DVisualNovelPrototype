using System;
using System.Collections;
using Dialog;
using UnityEngine;

[RequireComponent(typeof(DialogStarter))]
public class DialogTrigger : MonoBehaviour
{
    private DialogStarter dialogStarter;
    private bool keyPressed;
    private Coroutine coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SimpleCharacterController c))
            coroutine = StartCoroutine(WaitForKeyPressed());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out SimpleCharacterController c))
            StopCoroutine(coroutine);
    }

    private IEnumerator WaitForKeyPressed()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.B))
            {
                dialogStarter.StartDialog();
            }
        }
    }

    private void Awake()
    {
        dialogStarter = GetComponent<DialogStarter>();
    }
}
