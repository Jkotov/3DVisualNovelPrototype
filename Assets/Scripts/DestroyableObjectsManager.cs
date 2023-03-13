using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class DestroyableObjectsManager
{
    public static DestroyableObjectsManager Instance { get; } = new DestroyableObjectsManager();
    public IReadOnlyCollection<string> DestroyedObjects => destroyedObjects;
    private HashSet<string> destroyedObjects = new HashSet<string>();

    public void Load(HashSet<string> destroyed)
    {
        destroyedObjects = destroyed;
    }
        
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