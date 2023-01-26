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
        public List<AnswerWindow> AnswersWindows => answersWindows;
        [SerializeField] private MainTextDialogWindow mainText;
        [SerializeField] private float showAnimationTime;
        [SerializeField] private float hideAnimationTime;
        [SerializeField] private List<AnswerWindow> answersWindows;
        
        public void ShowDialogWindow()
        {
            
        }
        
        public void HideDialogWindow()
        {
            
        }

        public ReadOnlyCollection<AnswerWindow> ShowDialogBlock(DialogBlock block)
        {
            ShowMainText(block.blockText);
            return ShowAnswerWindows(block.answers);
        }

        private void ShowMainText(string text)
        {
            mainText.UpdateText(text);
        }
        
        private ReadOnlyCollection<AnswerWindow> ShowAnswerWindows(ICollection answers)
        {
            if (answers.Count > answersWindows.Count)
                throw new Exception("Not enough answer windows. Pls add more windows.");
            var windowCount = answers.Count;
            for (int i = 0; i < windowCount; i++)
            {
                answersWindows[i].gameObject.SetActive(true);
            }
            for (int i = windowCount; i < answersWindows.Count; i++)
            {
                answersWindows[i].gameObject.SetActive(false);
            }
            return answersWindows.GetRange(0, windowCount).AsReadOnly();
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