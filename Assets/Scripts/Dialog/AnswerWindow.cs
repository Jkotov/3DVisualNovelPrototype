using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

        public void OnPointerClick(PointerEventData eventData)
        {
            answerPressed?.Invoke(answer);
        }
    }
}