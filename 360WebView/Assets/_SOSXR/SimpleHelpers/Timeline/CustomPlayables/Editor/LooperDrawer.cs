using UnityEditor;
using UnityEngine;


//[CustomPropertyDrawer(typeof(LooperBehaviour))]
public class LooperDrawer : PropertyDrawer
{
    private SerializedProperty _exposedReference;


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var clip = property.serializedObject.targetObject as LooperClip;

        if (!clip)
        {
            return;
        }

        var clipTemplate = clip.behaviour;

        _exposedReference ??= property.FindPropertyRelative(nameof(clipTemplate.loopBreakerReference));

        DrawLooperVariables(clipTemplate);

        DrawLoopBreaker(clipTemplate);

        clipTemplate.ClipName = SetDisplayName(clipTemplate);
    }


    private static void DrawLooperVariables(LooperBehaviour clipTemplate)
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);

        clipTemplate.startLooperState =
            (LooperState) EditorGUILayout.EnumPopup("Looper state at start", clipTemplate.startLooperState);

        using (new EditorGUI.DisabledScope(true))
        {
            EditorGUILayout.TextField("Current looper state", clipTemplate.runningLooperState.ToString());
        }

        GUILayout.EndVertical();
    }


    private void DrawLoopBreaker(LooperBehaviour clipTemplate)
    {
        GUILayout.BeginVertical(EditorStyles.helpBox);

        clipTemplate.handControlTo =
            EditorGUILayout.ToggleLeft("Hand control to other class", clipTemplate.handControlTo);

        if (clipTemplate.handControlTo)
        {
            EditorGUILayout.PropertyField(_exposedReference, new GUIContent("Looper Breaker"));
        }

        GUILayout.EndVertical();
    }


    /// <summary>
    ///     The displayname of the clip in Timeline will be set using this method.
    ///     Amended from: https://forum.unity.com/threads/change-clip-name-with-custom-playable.499311/
    /// </summary>
    private static string SetDisplayName(LooperBehaviour looperBehaviour)
    {
        var displayName = looperBehaviour.runningLooperState switch
                          {
                              LooperState.Looping => "↩︎ Loop clip",
                              LooperState.GoToStart => "↩︎ go to clip start",
                              LooperState.DoNotLoop => "● do not loop",
                              LooperState.GoToEnd => "→ Go to clip end",
                              _ => ""
                          };

        if (looperBehaviour.handControlTo && looperBehaviour.LoopBreakerBase != null)
        {
            displayName += " || Breaker: " + looperBehaviour.LoopBreakerBase.gameObject.name;
        }

        displayName = CustomPlayableClipHelper.SetDisplayNameIfStillEmpty(displayName, "New Looper Clip");

        return displayName;
    }
}