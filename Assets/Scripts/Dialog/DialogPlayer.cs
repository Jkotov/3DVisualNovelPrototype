using UnityEngine;

namespace Dialog
{
    public class DialogPlayer : MonoBehaviour
    {
        public Dialog dialog;

        [ContextMenu("Dialog")]
        public void StartDialog()
        {
            DialogWindow.Instance.ShowDialogWindow();
        }
    }
}