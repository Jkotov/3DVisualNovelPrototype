using InventorySystem;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Destroyable), true)]
public class DestroyableCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var obj = target as Destroyable;
        if (GUILayout.Button("Generate ID"))
        {
            obj.GenerateGuid();
            EditorUtility.SetDirty(obj);
        }
        GUILayout.Label(obj.Guid);
    }
}