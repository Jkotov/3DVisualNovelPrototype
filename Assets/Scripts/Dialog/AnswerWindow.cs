using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialog
{
    public class AnswerWindow : MonoBehaviour
    {
        public Answer answer;
        public UnityEvent<Answer> answerPressed;

        private void OnMouseUpAsButton()
        {
            answerPressed?.Invoke(answer);
        }
    }
}