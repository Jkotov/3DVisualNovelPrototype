namespace Dialog
{
    public class DialogManager
    {
        public static DialogManager Instance { get; } = new DialogManager();

        public void StartDialog(DialogBlock firstDialog)
        {
            SubscribeToAnswerWindows();
            DialogWindow.Instance.ShowDialogWindow();
            ShowDialogBlock(firstDialog);
        }

        public void FinishDialog()
        {
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
            DialogWindow.Instance.ShowDialogBlock(dialogBlock);
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
        }
    }
}