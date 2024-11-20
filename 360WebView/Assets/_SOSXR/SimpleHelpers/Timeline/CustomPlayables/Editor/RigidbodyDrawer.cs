using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.Playables;


[CustomPropertyDrawer(typeof(RigidbodyBehaviour))]
public class RigidbodyDrawer : PropertyDrawer
{
    private SerializedProperty _exposedReference;
    private const string ClipDisplayname = "m_Clip.m_DisplayName";
    private const string ItemDisplayname = "m_Item.m_DisplayName";


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as RigidbodyClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.Template;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        clipTemplate.IsKinematic = EditorGUILayout.Toggle(nameof(clipTemplate.IsKinematic), clipTemplate.IsKinematic);
        clipTemplate.UseGravity = EditorGUILayout.Toggle(nameof(clipTemplate.UseGravity), clipTemplate.UseGravity);
        EditorGUILayout.EndVertical();

        GUILayout.Space(10f);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Space(5f);
        EditorGUILayout.HelpBox("Add force to thing?", MessageType.Info);
        clipTemplate.AddForce = EditorGUILayout.Toggle(nameof(clipTemplate.AddForce), clipTemplate.AddForce);

        if (clipTemplate.AddForce)
        {
            var director = TimelineEditor.inspectedDirector;
            _exposedReference ??= property.FindPropertyRelative(nameof(clipTemplate.TargetRef));
            EditorGUILayout.PropertyField(_exposedReference, new GUIContent("Target Ref"));
            clipTemplate.Target = clipTemplate.TargetRef.Resolve(director);

            clipTemplate.Amount = EditorGUILayout.FloatField(nameof(clipTemplate.Amount), clipTemplate.Amount);

            if (clipTemplate.Amount == 0)
            {
                EditorGUILayout.HelpBox("You need to add some force", MessageType.Warning);
            }

            clipTemplate.ForceMode =
                (ForceMode) EditorGUILayout.EnumPopup(nameof(clipTemplate.ForceMode), clipTemplate.ForceMode);
        }

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
        var clipTemplate = (RigidbodyBehaviour) template;

        var title = editorGUI.FindProperty(ClipDisplayname) ?? editorGUI.FindProperty(ItemDisplayname);

        if (title == null)
        {
            return;
        }

        title.stringValue = "K: " + clipTemplate.IsKinematic + " - G: " + clipTemplate.UseGravity;

        if (!clipTemplate.AddForce)
        {
            return;
        }

        if (clipTemplate.Target != null)
        {
            title.stringValue += " - T: " + clipTemplate.Target.name;
        }

        title.stringValue += " - Amount: " + clipTemplate.Amount + " - Mode: " + clipTemplate.ForceMode;
    }
}