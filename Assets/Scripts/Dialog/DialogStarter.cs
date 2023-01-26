using UnityEngine;

namespace Dialog
{
    public class DialogStarter : MonoBehaviour
    {
        [SerializeField] private Dialog dialog;

        [ContextMenu("StartDialog")]
        public void StartDialog()
        {
            
        }
    }
}