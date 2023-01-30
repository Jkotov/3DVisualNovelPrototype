using Dialog;
using UnityEngine;

[RequireComponent(typeof(DialogStarter))]
public class DialogTrigger : EventTrigger
{
    private DialogStarter dialogStarter;
    protected override void StartEvent()
    {
        dialogStarter.StartDialog();
    }
    
    private void Awake()
    {
        dialogStarter = GetComponent<DialogStarter>();
    }
}
