using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.QuestBook
{
    public class OpenBookButton : MonoBehaviour, IPointerClickHandler
    {
        [HideInInspector] public UnityEvent pressed;
        public void OnPointerClick(PointerEventData eventData)
        {
            pressed?.Invoke();
        }
    }
}