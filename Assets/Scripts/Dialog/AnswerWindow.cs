using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialog
{
    public class AnswerWindow : TextWindow
    {
        public Answer answer;
        public UnityEvent<Answer> answerPressed;

        public void UpdateAnswer(Answer newAnswer)
        {
            answer = newAnswer;
            UpdateText(newAnswer.text);
        }

        private void OnMouseUpAsButton()
        {
            answerPressed?.Invoke(answer);
        }
    }
}