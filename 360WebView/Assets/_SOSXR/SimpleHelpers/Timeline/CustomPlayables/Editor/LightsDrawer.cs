using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


[CustomPropertyDrawer(typeof(LightsBehaviour))]
public class LightsDrawer : PropertyDrawer
{
    private const string ClipDisplayname = "m_Clip.m_DisplayName";
    private const string ItemDisplayname = "m_Item.m_DisplayName";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as LightsClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        clipTemplate.ClipName = SetClipName(clipTemplate);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(15f);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        clipTemplate.Color = EditorGUILayout.ColorField("Color", clipTemplate.Color);
        clipTemplate.Intensity = EditorGUILayout.Slider("Intensity", clipTemplate.Intensity, 0, 50);
        clipTemplate.Range = EditorGUILayout.Slider("Range", clipTemplate.Range, 0, 10);

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
        var clipTemplate = (LightsBehaviour) template;

        var title = editorGUI.FindProperty(ClipDisplayname) ?? editorGUI.FindProperty(ItemDisplayname);

        if (title != null)
        {
            title.stringValue = clipTemplate.ClipName;
        }
    }


    private static string SetClipName(PlayableBehaviour template)
    {
        var clipTemplate = (LightsBehaviour) template;

        return clipTemplate.TrackBinding + " Intensity " + clipTemplate.Intensity + " Range " + clipTemplate.Range;
    }
}