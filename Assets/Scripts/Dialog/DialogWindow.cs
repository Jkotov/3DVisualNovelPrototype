using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        [SerializeField] private RectTransform answerWindowsContentRect;
        [SerializeField] private GameObject scrollView;
        [SerializeField] private Image actorImage;
        public void ShowDialogWindow()
        {
            if (FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }
            mainText.gameObject.SetActive(true);
            scrollView.SetActive(true);
            actorImage.gameObject.SetActive(true);
        }
        
        public void HideDialogWindow()
        {
            mainText.gameObject.SetActive(false);
            foreach (var answersWindow in answerWindows)
            {
                answersWindow.gameObject.SetActive(false);
            }
            actorImage.gameObject.SetActive(false);
            scrollView.SetActive(false);
        }

        public void ShowDialogBlock(DialogBlock block)
        {
            actorImage.sprite = block.actor.sprite;
            ShowMainText(block.blockText);
            ShowAnswerWindows(block.answers);
        }

        private void ShowMainText(string text)
        {
            mainText.UpdateText(text);
        }
        
        private void ShowAnswerWindows(IReadOnlyList<Answer> answers)
        {
            SetActiveAnswerWindows(answers);
            SetNewRectPositions(answers.Count);
        }

        private void SetActiveAnswerWindows(IReadOnlyList<Answer> answers)
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

        private void SetNewRectPositions(int activeAnswerWindows)
        {
            answerWindowsContentRect.anchoredPosition = Vector2.zero;
            var contentRectHeight = 0f;
            for (int i = 0; i < activeAnswerWindows; i++)
            {
                contentRectHeight += answerWindows[i].RectTransform.rect.height;
            }
            var sizeDelta = answerWindowsContentRect.sizeDelta;
            sizeDelta = new Vector2(sizeDelta.x, contentRectHeight);
            answerWindowsContentRect.sizeDelta = sizeDelta;
            /*var answerWindowCenter = sizeDelta;
            answerWindowCenter.y /= 2;
            answerWindowCenter.y += answerWindows[0].RectTransform.sizeDelta.y / 2;
            for (int i = 0; i < activeAnswerWindows; i++)
            {
                answerWindowCenter.y -= answerWindows[i].RectTransform.sizeDelta.y;
                answerWindows[i].RectTransform.anchoredPosition = answerWindowCenter;
            }*/
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