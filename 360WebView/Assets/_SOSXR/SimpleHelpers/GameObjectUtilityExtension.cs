using UnityEditor;
using UnityEngine;


/// <summary>
///     From ChatGPT
/// </summary>
public static class GameObjectUtilityExtension
{
    #if UNITY_EDITOR
    [MenuItem("Tools/DANGER/Remove Missing Scripts")]
    public static void RemoveMissingScripts()
    {
        var gameObjects = Selection.gameObjects;
        var count = 0;

        foreach (var gameObject in gameObjects)
        {
            if (RemoveMissingScripts(gameObject))
            {
                count++;
            }
        }

        Debug.Log($"Removed missing scripts from {count} GameObject(s).");
    }


    private static bool RemoveMissingScripts(GameObject gameObject)
    {
        var components = gameObject.GetComponents<Component>();
        var serializedObject = new SerializedObject(gameObject);
        var prop = serializedObject.FindProperty("m_Component");
        var r = 0;

        for (var j = 0; j < components.Length; j++)
        {
            if (components[j] == null)
            {
                prop.DeleteArrayElementAtIndex(j - r);
                r++;
            }
        }

        if (r > 0)
        {
            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();

            return true;
        }

        return false;
    }
    #endif
}