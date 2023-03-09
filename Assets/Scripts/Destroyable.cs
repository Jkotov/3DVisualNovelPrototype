using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    
    public string Guid => guid;
    [HideInInspector] [SerializeField] private string guid;

    private void Awake()
    {
        if (DestroyableObjectsManager.Instance.DestroyedObjects.Contains(Guid))
            Destroy(gameObject);
    }

    public void GenerateGuid()
    {
        guid = System.Guid.NewGuid().ToString();
    }
}