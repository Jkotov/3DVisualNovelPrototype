using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.QuestBook
{
    public class PageChangeButton : MonoBehaviour, IPointerClickHandler
    {
        public int RelativePages => relativePages;
        [HideInInspector] public UnityEvent<int> pressed;
        [SerializeField] private int relativePages;
        public void OnPointerClick(PointerEventData eventData)
        {
            pressed?.Invoke(relativePages);
        }
    }
}