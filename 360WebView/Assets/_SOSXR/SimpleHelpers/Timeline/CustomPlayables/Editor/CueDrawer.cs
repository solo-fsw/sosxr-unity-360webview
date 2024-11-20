using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(CueBehaviour))]
public class CueDrawer : PropertyDrawer
{
    private SerializedProperty _exposedReference;
    private const string WarningMessage = "The 'Cue at End' is also triggered when the behaviour is paused!";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as CueClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        if (clipTemplate == null)
        {
            return;
        }


        GUILayout.BeginVertical(EditorStyles.helpBox);
        _exposedReference ??= property.FindPropertyRelative(nameof(clipTemplate.Reference));
        EditorGUILayout.PropertyField(_exposedReference, new GUIContent("Cue"));
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);
        clipTemplate.CueAtStart = EditorGUILayout.ToggleLeft("Trigger Cue on clip start", clipTemplate.CueAtStart);
        clipTemplate.CueAtEnd = EditorGUILayout.ToggleLeft("Trigger Cue on clip end", clipTemplate.CueAtEnd);
        GUILayout.EndVertical();

        if (clipTemplate.CueAtEnd)
        {
            EditorGUILayout.Space(15f);
            EditorGUILayout.HelpBox(WarningMessage, MessageType.Warning, true);
        }
    }
}