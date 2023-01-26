namespace Dialog
{
    public class DialogManager
    {
        public static DialogManager Instance { get; } = new DialogManager();
        public Dialog CurrentDialog { get; private set; }

        public void StartDialog(Dialog dialog)
        {
            CurrentDialog = dialog;
            DialogWindow.Instance.ShowDialogWindow();
            ShowDialogBlock(dialog.dialogBlocks[0]);
        }

        public void ShowDialogBlock(DialogBlock dialogBlock)
        {
            var answerWindows = DialogWindow.Instance.ShowDialogBlock(dialogBlock);
        }

        public void AnswerPressed(Answer answer)
        {
            
        }

        static DialogManager()
        {
        }

        private DialogManager()
        {
        }
    }
}