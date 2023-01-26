using System;
using UnityEngine;
using UnityEngine.Events;

namespace Dialog
{
    public class AnswerWindow : MonoBehaviour
    {
        public Answer answer;
        public UnityEvent<Answer> answerPressed;
        
        
    }
}