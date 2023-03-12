using System;
using System.Collections.Generic;
using System.Linq;
using QuestSystem;
using TMPro;
using UnityEngine;

namespace UI.QuestBook
{
    public class QuestBookWindow : MonoBehaviour
    {
        [SerializeField] private GameObject questNamePrefab;
        [SerializeField] private GameObject thoughtPrefab;
        [SerializeField] private List<RectTransform> pages;
        [SerializeField] private QuestPage questPage;
        [SerializeField] private OpenBookButton openBookButton;
        [SerializeField] private OpenBookButton openActiveQuests;
        [SerializeField] private OpenBookButton openFinishedQuests;
        [SerializeField] private OpenBookButton openThoughts;
        [SerializeField] private List<PageChangeButton> pageChangeButtons;
        private List<QuestDescriptionButton> activeQuestLinkRects = new List<QuestDescriptionButton>();
        private List<QuestDescriptionButton> finishedQuestLinkRects = new List<QuestDescriptionButton>();
        private List<QuestBookTextElement> thoughtTextElements = new List<QuestBookTextElement>();
        private List<RectPageIndex> activeLinkRects;
        private List<RectPageIndex> finishedLinkRects;
        private List<RectPageIndex> thoughtRects;
        private List<RectPageIndex> currentList;
        private int currentPage;
        private bool isOpened;

        private void Awake()
        {
            openBookButton.pressed.AddListener(BookButtonListener);
            openActiveQuests.pressed.AddListener(OpenActiveQuestLinks);
            openFinishedQuests.pressed.AddListener(OpenFinishedQuestLinks);
            openThoughts.pressed.AddListener(OpenThoughts);
            foreach (var pageChangeButton in pageChangeButtons)
            {
                pageChangeButton.pressed.AddListener(TryChangePage);
            }
            DontDestroyOnLoad(gameObject);
        }

        private void BookButtonListener()
        {
            if (isOpened)
            {
                CloseBook();
            }
            else
            {
                OpenBook();
            }
        }

        private void CloseBook()
        {
            OpenedWindowManager.Instance.RemoveMarkAsOpened(this);
            isOpened = false;
            foreach (var button in pageChangeButtons)
            {
                button.gameObject.SetActive(false);
            }

            foreach (var page in pages)
            {
                page.gameObject.SetActive(false);
            }
            questPage.gameObject.SetActive(false);
            if (currentList != null)
            {
                foreach (var rectPageIndex in currentList)
                {
                    rectPageIndex.rectTransform.gameObject.SetActive(false);
                }
            }
            openActiveQuests.gameObject.SetActive(false);
            openFinishedQuests.gameObject.SetActive(false);
            openThoughts.gameObject.SetActive(false);
        }
        
        public void ShowPageChangeButtons()
        {
            foreach (var button in pageChangeButtons)
            {
                button.gameObject.SetActive(false);
                foreach (var rectPageIndex in currentList)
                {
                    if (rectPageIndex.page == button.RelativePages + currentPage)
                    {
                        button.gameObject.SetActive(true);
                        break;
                    }
                }
            }
        }
        
        public void OpenBook()
        {
            if (OpenedWindowManager.Instance.CanOpen(this) == false)
                return;
            OpenedWindowManager.Instance.MarkAsOpened(this);
            isOpened = true;
            UpdateTextElements();
            activeLinkRects = CalcRects(activeQuestLinkRects.Select(a =>  a.RectTransform).ToList());
            finishedLinkRects = CalcRects(finishedQuestLinkRects.Select(a =>  a.RectTransform).ToList());
            thoughtRects = CalcRects(thoughtTextElements.Select(a =>  a.RectTransform).ToList());
            foreach (var page in pages)
            {
                page.gameObject.SetActive(true);
            }
            OpenActiveQuestLinks();
            
            openActiveQuests.gameObject.SetActive(true);
            openFinishedQuests.gameObject.SetActive(true);
            openThoughts.gameObject.SetActive(true);
        }

        public void OpenActiveQuestLinks()
        {
            questPage.gameObject.SetActive(false);
            if (currentList != null)
                foreach (var element in currentList)
                {
                    element.rectTransform.gameObject.SetActive(false);
                }  
            currentList = activeLinkRects;
            currentPage = 0;
            OpenPage(currentList, currentPage);
        }

        public void OpenFinishedQuestLinks()
        {
            questPage.gameObject.SetActive(false);
            if (currentList != null)
              foreach (var element in currentList)
              {
                    element.rectTransform.gameObject.SetActive(false);
              }  
            currentList = finishedLinkRects;
            currentPage = 0;
            OpenPage(currentList, currentPage);
        }

        public void OpenThoughts()
        {
            questPage.gameObject.SetActive(false);
            if (currentList != null)
                foreach (var element in currentList)
                {
                    element.rectTransform.gameObject.SetActive(false);
                }  
            currentList = thoughtRects;
            currentPage = 0;
            OpenPage(currentList, currentPage);
        }

        public void TryChangePage(int relativePageIndex)
        {
            foreach (var rectPage in currentList)
            {
                if (rectPage.page == currentPage + relativePageIndex)
                {
                    OpenPage(currentList, currentPage + relativePageIndex);
                    break;
                }
            }
        }

        public void OpenQuestPage(Quest quest)
        {
            foreach (var page in currentList)
            {
                page.rectTransform.gameObject.SetActive(false);
            }
            foreach (var pageChangeButton in pageChangeButtons)
            {
                pageChangeButton.gameObject.SetActive(false);
            }
            currentList = null;
            questPage.gameObject.SetActive(true);
            questPage.SetQuest(quest);
        }
        
        private void OpenPage(List<RectPageIndex> rectPageIndices, int page)
        {
            var pageNumbers = GetNearPages(page);
            foreach (var rectPageIndex in rectPageIndices)
            {
                rectPageIndex.rectTransform.gameObject.SetActive(pageNumbers.Contains(rectPageIndex.page));
            }
            currentPage = page;
            ShowPageChangeButtons();
        }

        private void UpdateTextElements()
        {
            var questNames = new List<string>(QuestManager.Instance.Thoughts.Count);
            UpdateQuestButtons(QuestManager.Instance.ActiveQuests, questNamePrefab, ref activeQuestLinkRects);
            UpdateQuestButtons(QuestManager.Instance.FinishedQuests, questNamePrefab, ref finishedQuestLinkRects);
            foreach (var thought in QuestManager.Instance.Thoughts)
            {
                questNames.Add(thought.description);
            }
            UpdateTextElementList(questNames, thoughtPrefab, ref thoughtTextElements);
        }

        private void UpdateTextElementList(List<string> stringList, GameObject textElementPrefab, ref List<QuestBookTextElement> textElements)
        {
            if (textElements.Capacity < stringList.Count)
                textElements.AddRange(new List<QuestBookTextElement>(stringList.Count - textElements.Capacity)); 
            for (int i = 0; i < stringList.Count; i++)
            {
                if (textElements.Count < i + 1)
                    textElements.Add(null);
                textElements[i] = textElements[i] == null ?
                    Instantiate(textElementPrefab, transform).GetComponent<QuestBookTextElement>() :
                    textElements[i];
                textElements[i].Text = stringList[i]; 
                textElements[i].gameObject.SetActive(false);
            }
            for (int i = stringList.Count; i < textElements.Count; i++)
            {
                Destroy(textElements[i].gameObject);
            }
        }
        
        
        private void UpdateQuestButtons(IList<Quest> questList, GameObject textElementPrefab, ref List<QuestDescriptionButton> textElements)
        {
            if (textElements.Capacity < questList.Count)
                textElements.AddRange(new List<QuestDescriptionButton>(questList.Count - textElements.Capacity)); 
            for (int i = 0; i < questList.Count; i++)
            {
                if (textElements.Count < i + 1)
                    textElements.Add(null);
                textElements[i] = textElements[i] == null ?
                    Instantiate(textElementPrefab, transform).GetComponent<QuestDescriptionButton>() :
                    textElements[i]; 
                textElements[i].gameObject.SetActive(false);
                textElements[i].Quest = questList[i];
                textElements[i].pressed.RemoveAllListeners();
                textElements[i].pressed.AddListener(OpenQuestPage);
            }
            for (int i = questList.Count; i < textElements.Count; i++)
            {
                Destroy(textElements[i].gameObject);
            }
        }
        
        private List<RectPageIndex> CalcRects(List<RectTransform> rects)
        {
            List<RectPageIndex> rectPageIndices = new List<RectPageIndex>(rects.Count);
            int page = 0;
            float currentHeight = 0;
            foreach (var rect in rects)
            {
                if (CheckRectSize(rect) == false)
                    continue;
                while (rect.sizeDelta.y + currentHeight > pages[page % pages.Count].sizeDelta.y)
                {
                    page++;
                    currentHeight = 0;
                }
                currentHeight += rect.sizeDelta.y;
                var anchoredPosition = pages[page % pages.Count].anchoredPosition;
                anchoredPosition.y -= currentHeight;
                rect.anchoredPosition = anchoredPosition;
                rectPageIndices.Add(new RectPageIndex(rect, page));
            }
            return rectPageIndices;
        }

        private bool CheckRectSize(RectTransform rect)
        {
            foreach (var page in pages)
            {
                var diff = page.sizeDelta - rect.sizeDelta;
                if (diff.x >= 0 && diff.y >= 0)
                {
                    return true;
                }
            }
            Debug.LogWarning($"Rect {rect.gameObject.name} is too big");
            return false;
        }

        private List<int> GetNearPages(int page)
        {
            var res = new List<int>(pages.Count);
            for (int i = 0; i < pages.Count; i++)
            {
                res.Add(page - (page % pages.Count) + i);
            }
            return res;
        }
        
        
    }
}