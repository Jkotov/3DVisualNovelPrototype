using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DefaultNamespace
{
    public class DestroyableObjectsManager
    {
        public static DestroyableObjectsManager Instance { get; } = new DestroyableObjectsManager();
        public IReadOnlyCollection<string> DestroyedObjects => destroyedObjects;
        private readonly HashSet<string> destroyedObjects = new HashSet<string>();

        public void MarkObjectAsDestroyed(Destroyable destroyable)
        {
            destroyedObjects.Add(destroyable.Guid);
        }
        
        static DestroyableObjectsManager()
        {
        }

        private DestroyableObjectsManager()
        {
        }
    }
}