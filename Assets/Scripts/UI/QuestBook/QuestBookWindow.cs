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
        private List<QuestBookTextElement> activeQuestLinkRects = new List<QuestBookTextElement>();
        private List<QuestBookTextElement> finishedQuestLinkRects = new List<QuestBookTextElement>();
        private List<QuestBookTextElement> thoughtTextElements = new List<QuestBookTextElement>();
        private List<RectPageIndex> activeLinkRects;
        private List<RectPageIndex> finishedLinkRects;
        private List<RectPageIndex> thoughtRects;
        private List<RectPageIndex> currentList;
        private int currentPage;

        public void OpenBook()
        {
            UpdateTextElements();

            activeLinkRects = CalcRects(activeQuestLinkRects.Select(a =>  a.RectTransform).ToList());
            finishedLinkRects = CalcRects(finishedQuestLinkRects.Select(a =>  a.RectTransform).ToList());
            thoughtRects = CalcRects(thoughtTextElements.Select(a =>  a.RectTransform).ToList());
            OpenActiveQuestLinks();
        }

        public void OpenActiveQuestLinks()
        {
            questPage.gameObject.SetActive(false);
            currentList = activeLinkRects;
            currentPage = 0;
        }

        public void OpenFinishedQuestLinks()
        {
            questPage.gameObject.SetActive(false);
            currentList = finishedLinkRects;
            currentPage = 0;
        }

        public void OpenThoughts()
        {
            questPage.gameObject.SetActive(false);
            currentList = thoughtRects;
            currentPage = 0;
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
            currentList = null;
            questPage.gameObject.SetActive(true);
            foreach (var page in currentList)
            {
                page.rectTransform.gameObject.SetActive(false);
            }
            questPage.SetQuest(quest);
        }
        
        private void OpenPage(List<RectPageIndex> rectPageIndices, int page)
        {
            foreach (var rectPageIndex in rectPageIndices)
            {
                rectPageIndex.rectTransform.gameObject.SetActive(rectPageIndex.page == page);
            }
        }

        private void UpdateTextElements()
        {
            var questNames = new List<string>(Math.Max(QuestManager.Instance.ActiveQuests.Count,
                QuestManager.Instance.FinishedQuests.Count));
            var questDescriptions = new List<string>(Math.Max(Math.Max
                    (QuestManager.Instance.ActiveQuests.Count, QuestManager.Instance.FinishedQuests.Count),
                QuestManager.Instance.Thoughts.Count));
            foreach (var quest in QuestManager.Instance.ActiveQuests)
            {
                questNames.Add(quest.name);
            }
            UpdateTextElementList(questNames, questNamePrefab, ref activeQuestLinkRects);
            questNames.Clear();
            questDescriptions.Clear();
            foreach (var quest in QuestManager.Instance.FinishedQuests)
            {
                questNames.Add(quest.name);
            }
            UpdateTextElementList(questNames, questNamePrefab, ref finishedQuestLinkRects);
            questDescriptions.Clear();
            foreach (var thought in QuestManager.Instance.Thoughts)
            {
                questDescriptions.Add(thought.description);
            }
            UpdateTextElementList(questDescriptions, thoughtPrefab, ref thoughtTextElements);
        }
        
        private void UpdateTextElementList(List<string> stringList, GameObject textElementPrefab, ref List<QuestBookTextElement> textElements)
        {
            if (textElements.Capacity < stringList.Count)
                textElements.AddRange(new List<QuestBookTextElement>(stringList.Count - textElements.Capacity)); 
            for (int i = 0; i < stringList.Count; i++)
            {
                 textElements[i] = textElements[i] == null ?
                    Instantiate(textElementPrefab).GetComponent<QuestBookTextElement>() :
                    textElements[i];
                 textElements[i].gameObject.SetActive(false);
            }
            for (int i = stringList.Count; i < textElements.Count; i++)
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
        
        private struct RectPageIndex
        {
            public RectTransform rectTransform;
            public int page;

            public RectPageIndex(RectTransform rect, int page)
            {
                rectTransform = rect;
                this.page = page;
            }
        }
    }
}