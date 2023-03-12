using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UI
{
    public class OpenedWindowManager
    {
        public static OpenedWindowManager Instance { get; } = new OpenedWindowManager();
        public IReadOnlyCollection<object> Opened => opened;
        private readonly HashSet<object> opened = new HashSet<object>();
        public bool CanOpen(object window) => opened.All(item => item == window);
        public void MarkAsOpened(object window)
        {
            opened.Add(window);
        }

        public void RemoveMarkAsOpened(object window)
        {
            opened.Remove(window);
        }
        private OpenedWindowManager()
        {
        }

        static OpenedWindowManager()
        {
        }
    }
}