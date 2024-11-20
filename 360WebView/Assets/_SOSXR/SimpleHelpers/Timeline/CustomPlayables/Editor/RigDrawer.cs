using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


[CustomPropertyDrawer(typeof(RigBehaviour))]
public class RigDrawer : PropertyDrawer
{
    private const string ClipDisplayname = "m_Clip.m_DisplayName";
    private const string ItemDisplayname = "m_Item.m_DisplayName";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as RigClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        if (!clipTemplate.Reset)
        {
            clipTemplate.Reset = GUILayout.Toggle(clipTemplate.Reset,
                new GUIContent("Reset", "Reset RigWeight once clip is finished"));
        }
        else if (clipTemplate.Reset)
        {
            EditorGUILayout.BeginHorizontal();

            clipTemplate.Reset = GUILayout.Toggle(clipTemplate.Reset,
                new GUIContent("Reset to value", "Reset RigWeight once clip is finished"));

            clipTemplate.ResetToValue = EditorGUILayout.Slider(clipTemplate.ResetToValue, 0, 1);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(5);
        clipTemplate.RigWeight ??= new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
        clipTemplate.RigWeight = EditorGUILayout.CurveField(nameof(clipTemplate.RigWeight), clipTemplate.RigWeight);

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        // Assume that the currently selected object is the internal class UnityEditor.Timeline.EditorClip
        // this gives you access to the clip start, duration etc.
        var editorGUI = new SerializedObject(Selection.activeObject);

        SetTitle(editorGUI, clipTemplate);

        editorGUI.ApplyModifiedProperties();
    }


    private static void SetTitle(SerializedObject editorGUI, PlayableBehaviour template)
    {
        var title = editorGUI.FindProperty(ClipDisplayname) ?? editorGUI.FindProperty(ItemDisplayname);

        if (title == null)
        {
            return;
        }

        var clipTemplate = (RigBehaviour) template;

        if (clipTemplate.RigTarget != null && clipTemplate.WorldPosTarget != null)
        {
            title.stringValue = clipTemplate.RigTarget.name + " to " + clipTemplate.WorldPosTarget.name;
        }
    }
}