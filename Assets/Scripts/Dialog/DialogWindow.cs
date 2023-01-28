using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;

namespace Dialog
{
    public class DialogWindow : MonoBehaviour
    {
        public static DialogWindow Instance { get; private set; }
        public ReadOnlyCollection<AnswerWindow> AnswersWindows => answerWindows.AsReadOnly();
        [SerializeField] private MainTextDialogWindow mainText;
        [SerializeField] private float showAnimationTime;
        [SerializeField] private float hideAnimationTime;
        [SerializeField] private List<AnswerWindow> answerWindows;
        
        public void ShowDialogWindow()
        {
            mainText.gameObject.SetActive(true);
        }
        
        public void HideDialogWindow()
        {
            mainText.gameObject.SetActive(false);
            foreach (var answersWindow in answerWindows)
            {
                answersWindow.gameObject.SetActive(false);
            }
        }

        public void ShowDialogBlock(DialogBlock block)
        {
            ShowMainText(block.blockText);
            ShowAnswerWindows(block.answers);
        }

        private void ShowMainText(string text)
        {
            mainText.UpdateText(text);
        }
        
        private void ShowAnswerWindows(IReadOnlyList<Answer> answers)
        {
            if (answers.Count > answerWindows.Count)
                throw new Exception("Not enough answer windows. Pls add more windows.");
            var windowCount = answers.Count;
            for (int i = 0; i < windowCount; i++)
            {
                answerWindows[i].gameObject.SetActive(true);
                answerWindows[i].UpdateAnswer(answers[i]);
            }
            for (int i = windowCount; i < answerWindows.Count; i++)
            {
                answerWindows[i].gameObject.SetActive(false);
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}