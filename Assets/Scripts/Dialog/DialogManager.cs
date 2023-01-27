namespace Dialog
{
    public class DialogManager
    {
        public static DialogManager Instance { get; } = new DialogManager();

        public void StartDialog(DialogBlock firstDialog)
        {
            DialogWindow.Instance.ShowDialogWindow();
            ShowDialogBlock(firstDialog);
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