using InventorySystem;
using UnityEngine;

namespace Dialog
{
    public class DialogStarter : MonoBehaviour
    {
        [SerializeField] private DialogBlock firstDialogBlock;

        [ContextMenu("StartDialog")]
        public void StartDialog(Inventory inventory = null)
        {
            DialogManager.Instance.StartDialog(firstDialogBlock, inventory);
        }
    }
}