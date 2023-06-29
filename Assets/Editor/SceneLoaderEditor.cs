using PlayerActions;
using UnityEditor;

[CustomEditor(typeof(SceneLoadData), true)]
public class SceneLoaderEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var sceneLoader = target as SceneLoadData;
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(sceneLoader.Scene);

        serializedObject.Update();

        EditorGUI.BeginChangeCheck();
        var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

        if (EditorGUI.EndChangeCheck())
        {
            var newPath = AssetDatabase.GetAssetPath(newScene);
            var scenePathProperty = serializedObject.FindProperty("scene");
            scenePathProperty.stringValue = newPath;
        }
        serializedObject.ApplyModifiedProperties();
    }
}