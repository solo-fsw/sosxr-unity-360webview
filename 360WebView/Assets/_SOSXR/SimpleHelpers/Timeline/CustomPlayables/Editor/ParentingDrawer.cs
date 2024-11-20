using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


[CustomPropertyDrawer(typeof(ParentingBehaviour))]
public class ParentingDrawer : PropertyDrawer
{
    private const string ClipDisplayname = "m_Clip.m_DisplayName";
    private const string ItemDisplayname = "m_Item.m_DisplayName";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as ParentingClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        EditorGUILayout.BeginVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        clipTemplate.ZeroInOnParent = EditorGUILayout.Toggle("Zero in on Parent", clipTemplate.ZeroInOnParent);
        EditorGUILayout.EndVertical();

        GUILayout.Space(15f);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(5f);

        EditorGUILayout.HelpBox(
            "It's better to position / rotate the ParentToObject transform than to use the Local Offsets below",
            MessageType.Info);

        GUILayout.Space(5f);

        clipTemplate.LocalPositionOffset =
            EditorGUILayout.Vector3Field("Local Position Offset", clipTemplate.LocalPositionOffset);

        clipTemplate.LocalRotationOffset =
            EditorGUILayout.Vector3Field("Local Rotation Offset", clipTemplate.LocalRotationOffset);

        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        // Assume that the currently selected object is the internal class UnityEditor.Timeline.EditorClip
        // this gives you access to the clip start, duration etc.
        var editorGUI = new SerializedObject(Selection.activeObject);

        SetTitle(editorGUI, clipTemplate);

        editorGUI.ApplyModifiedProperties();
    }


    private static void SetTitle(SerializedObject editorGUI, PlayableBehaviour template)
    {
        var clipTemplate = (ParentingBehaviour) template;

        var title = editorGUI.FindProperty(ClipDisplayname) ?? editorGUI.FindProperty(ItemDisplayname);

        if (title != null)
        {
            if (clipTemplate.ParentToObject == null)
            {
                return;
            }

            title.stringValue = clipTemplate.ParentToObject.name + " - ZeroIn: " + clipTemplate.ZeroInOnParent;
        }
    }
}