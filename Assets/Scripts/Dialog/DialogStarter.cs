using UnityEngine;

namespace Dialog
{
    public class DialogStarter : MonoBehaviour
    {
        [SerializeField] private DialogBlock firstDialogBlock;

        [ContextMenu("StartDialog")]
        public void StartDialog()
        {
            DialogManager.Instance.StartDialog(firstDialogBlock);
        }
    }
}