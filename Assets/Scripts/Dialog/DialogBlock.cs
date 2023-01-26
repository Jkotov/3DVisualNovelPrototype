using System.Collections.Generic;

namespace Dialog
{
    public class DialogBlock
    {
        public string blockText;
        public Actor actor;
        public List<Answer> answers;
    }
    
    public class Answer
    {
        public string text;
        public DialogBlock nextDialogBlock;
    }
}