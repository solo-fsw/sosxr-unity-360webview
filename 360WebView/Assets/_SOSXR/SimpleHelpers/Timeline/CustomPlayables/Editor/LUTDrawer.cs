using UnityEditor;
using UnityEngine;
using UnityEngine.Playables;


[CustomPropertyDrawer(typeof(LUTBehaviour))]
public class LUTDrawer : PropertyDrawer
{
    private const string ClipDisplayname = "m_Clip.m_DisplayName";
    private const string ItemDisplayname = "m_Item.m_DisplayName";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as LUTClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        GUILayout.Space(5f);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        var previewHeight = GUILayout.Height(200f);

        clipTemplate.LUTTexture = (Texture) EditorGUILayout.ObjectField("LUT Texture", clipTemplate.LUTTexture,
            typeof(Texture), false, previewHeight);

        EditorGUILayout.EndVertical();

        GUILayout.Space(5f);
        clipTemplate.Contribution = EditorGUILayout.Slider("LUT Contribution", clipTemplate.Contribution, 0, 1);
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
        var clipTemplate = (LUTBehaviour) template;

        var title = editorGUI.FindProperty(ClipDisplayname) ?? editorGUI.FindProperty(ItemDisplayname);

        if (title != null)
        {
            title.stringValue = clipTemplate.LUTTexture.name + " - Contribution: " + clipTemplate.Contribution;
        }
    }
}