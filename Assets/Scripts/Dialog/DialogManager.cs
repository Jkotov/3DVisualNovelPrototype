using InventorySystem;
using UnityEngine.Events;

namespace Dialog
{
    public class DialogManager
    {
        public bool IsDialogStarted => isDialogStarted;
        public static DialogManager Instance { get; } = new DialogManager();
        public readonly UnityEvent<Actor> activeActorChanged;
        private bool isDialogStarted;
        public Inventory CurrentInventory { get; private set; }

        public void StartDialog(DialogBlock firstDialog, Inventory inventory = null)
        {
            if (isDialogStarted)
                return;
            CurrentInventory = inventory;
            isDialogStarted = true;
            SubscribeToAnswerWindows();
            DialogWindow.Instance.ShowDialogWindow();
            ShowDialogBlock(firstDialog);
        }

        public void FinishDialog()
        {
            isDialogStarted = false;
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
            activeActorChanged?.Invoke(dialogBlock.actor);
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
            activeActorChanged = new UnityEvent<Actor>();
        }
    }
}