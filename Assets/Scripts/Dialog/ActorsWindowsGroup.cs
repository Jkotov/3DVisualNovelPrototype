using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    public class ActorsWindowsGroup : MonoBehaviour
    {
        [SerializeField] private GameObject actorWindowPrefab;
        private readonly List<ActorWindow> actorWindows = new List<ActorWindow>();
        public void UpdateActorWindows(List<Actor> actors)
        {
            int newWindowsCount = actors.Count - actorWindows.Count;
            if (newWindowsCount > 0)
            {
                actorWindows.Capacity = actors.Count;
            }
            
            for (int i = actorWindows.Count; i < actors.Count; i++)
            {
                actorWindows.Add(Instantiate(actorWindowPrefab, transform).GetComponent<ActorWindow>());
            }

            for (int i = 0; i < actors.Count; i++)
            {
                actorWindows[i].gameObject.SetActive(true);
                actorWindows[i].Actor = actors[i];
            }

            for (int i = actors.Count; i < actorWindows.Count; i++)
            {
                actorWindows[i].gameObject.SetActive(false);
            }
        }
    }
}