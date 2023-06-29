using InventorySystem;
using UnityEditor;
using UnityEngine;
using Utils;

[CustomEditor(typeof(Id), true)]
public class IdCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var obj = target as Id;
        if (GUILayout.Button("Generate ID"))
        {
            obj.GenerateGuid();
            EditorUtility.SetDirty(obj);
        }
        GUILayout.Label(obj.Guid);
    }
}