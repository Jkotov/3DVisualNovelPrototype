using UnityEngine;

namespace UI.QuestBook
{
    public struct RectPageIndex
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