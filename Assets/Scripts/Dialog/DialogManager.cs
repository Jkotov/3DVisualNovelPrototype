namespace Dialog
{
    public class DialogManager
    {
        public static DialogManager Instance { get; } = new DialogManager();
        public Dialog CurrentDialog { get; private set; }

        public void StartDialog(Dialog dialog)
        {
            CurrentDialog = dialog;
            DialogWindow.instance.ShowDialogWindow();
            DialogWindow.instance.ShowDialogBlock(dialog.dialogBlocks[0]);
        }
        
        static DialogManager()
        {
        }

        private DialogManager()
        {
        }
    }
}