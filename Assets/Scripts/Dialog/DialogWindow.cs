using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class DialogWindow : MonoBehaviour
    {
        public static DialogWindow instance;
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private float showAnimationTime;
        [SerializeField] private float hideAnimationTime;
        
        public void ShowDialogWindow()
        {
            
        }
        
        public void ShowDialogBlock(DialogBlock block)
        {
            
        }

        public void HideDialogWindow()
        {
            
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}