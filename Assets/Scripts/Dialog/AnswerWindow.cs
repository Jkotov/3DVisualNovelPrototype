using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Dialog
{
    public class AnswerWindow : TextWindow, IPointerClickHandler
    {
        [HideInInspector] public Answer answer;
        [HideInInspector] public UnityEvent<Answer> answerPressed;

        public void UpdateAnswer(Answer newAnswer)
        {
            answer = newAnswer;
            UpdateText(newAnswer.text);
        }

        private void OnMouseUpAsButton()
        {
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Pressed");
            answerPressed?.Invoke(answer);
        }
    }
}