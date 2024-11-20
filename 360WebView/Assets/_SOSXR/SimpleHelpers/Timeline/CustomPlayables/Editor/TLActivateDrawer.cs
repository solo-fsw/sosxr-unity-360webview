using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(TLActivateBehaviour))]
public class TLActivateDrawer : PropertyDrawer
{
    private SerializedProperty _exposedReference;
    private const string WarningMessage = "The 'Activate at End' is also triggered when the behaviour is paused!";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as TLActivateClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.template;

        if (clipTemplate == null)
        {
            return;
        }

        _exposedReference ??= property.FindPropertyRelative(nameof(clipTemplate.activateReference));

        GUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(_exposedReference, new GUIContent("Activate"));
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        clipTemplate.activateAtStart =
            EditorGUILayout.ToggleLeft("Trigger Activate clip start", clipTemplate.activateAtStart);

        clipTemplate.activateAtEnd =
            EditorGUILayout.ToggleLeft("Trigger Activate clip end", clipTemplate.activateAtEnd);

        GUILayout.EndVertical();

        if (clipTemplate.activateAtEnd)
        {
            EditorGUILayout.Space(15f);
            EditorGUILayout.HelpBox(WarningMessage, MessageType.Warning, true);
        }
    }
}