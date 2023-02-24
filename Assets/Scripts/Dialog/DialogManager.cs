using System.Collections.Generic;
using InventorySystem;
using UnityEngine.Events;

namespace Dialog
{
    public class DialogManager
    {
        public bool IsDialogStarted { get; private set; }
        public static DialogManager Instance { get; } = new DialogManager();
        public readonly UnityEvent<List<Actor>> activeActorChanged;
        public Inventory CurrentInventory { get; private set; }

        public void StartDialog(DialogBlock firstDialog, Inventory inventory = null)
        {
            if (IsDialogStarted)
                return;
            CurrentInventory = inventory;
            IsDialogStarted = true;
            SubscribeToAnswerWindows();
            DialogWindow.Instance.ShowDialogWindow();
            ShowDialogBlock(firstDialog);
        }

        public void FinishDialog()
        {
            IsDialogStarted = false;
            DialogWindow.Instance.HideDialogWindow();
        }
        
        public void SubscribeToAnswerWindows()
        {
            foreach (var window in DialogWindow.Instance.AnswersWindows)
            {
                window.answerPressed.AddListener(AnswerPressed);
            }
        }

        private void ShowDialogBlock(DialogBlock dialogBlock)
        {
            activeActorChanged?.Invoke(dialogBlock.actors);
            DialogWindow.Instance.ShowDialogBlock(dialogBlock);
            dialogBlock.DoActions();
        }

        private void AnswerPressed(Answer answer)
        {
            if (answer.nextDialogBlock != null)
                ShowDialogBlock(answer.nextDialogBlock);
            else
                FinishDialog();
        }

        static DialogManager()
        {
        }

        private DialogManager()
        {
            activeActorChanged = new UnityEvent<List<Actor>>();
        }
    }
}