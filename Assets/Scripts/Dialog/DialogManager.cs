using System.Collections.Generic;
using InventorySystem;
using UI;
using UnityEngine.Events;

namespace Dialog
{
    public class DialogManager
    {
        public bool IsDialogStarted { get; private set; }
        public static DialogManager Instance { get; } = new DialogManager();
        public readonly UnityEvent<List<Actor>> activeActorChanged;
        public Inventory CurrentInventory { get; private set; }
        private DialogBlock currentDialogBlock;

        public void StartDialog(DialogBlock firstDialog, Inventory inventory = null)
        {
            if (OpenedWindowManager.Instance.CanOpen(DialogWindow.Instance) == false)
                return;
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
            activeActorChanged.Invoke(new List<Actor>());
            currentDialogBlock = null;
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
            if (dialogBlock == currentDialogBlock)
                return;
            currentDialogBlock = dialogBlock;
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