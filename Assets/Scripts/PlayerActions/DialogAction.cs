using System.Collections.Generic;
using Dialog;
using UnityEngine;

namespace PlayerActions
{
    public class DialogAction : MonoBehaviour
    {
        private readonly List<DialogStarter> dialogStarters = new List<DialogStarter>();

        public void TryStartDialog()
        {
            if (dialogStarters.Count > 0)
                dialogStarters[0].StartDialog();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out DialogStarter dialogStarter))
            {
                dialogStarters.Add(dialogStarter);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out DialogStarter dialogStarter))
            {
                dialogStarters.RemoveAll(starter => starter == dialogStarter);
            }
        }
    }
}