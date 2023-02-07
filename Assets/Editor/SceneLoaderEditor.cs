using UnityEditor;

[CustomEditor(typeof(SceneLoader), true)]
public class SceneLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var sceneLoader = target as SceneLoader;
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