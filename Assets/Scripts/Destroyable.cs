using System;
using System.Linq;
using UnityEngine;

public class Destroyable : Id
{
    private void Awake()
    {
        if (DestroyableObjectsManager.Instance.DestroyedObjects.Contains(Guid))
            Destroy(gameObject);
    }
}