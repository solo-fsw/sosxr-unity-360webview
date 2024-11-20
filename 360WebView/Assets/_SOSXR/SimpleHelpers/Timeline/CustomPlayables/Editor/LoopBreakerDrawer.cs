using UnityEditor;
using UnityEngine;


//[CustomEditor(typeof(LoopBreaker), true)] // That boolean allows us to use this Custom Editor for child classes too
public class LoopBreakerDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var loopBreaker = (LoopBreaker) target;

        GUILayout.Space(10f);
        GUILayout.BeginVertical(EditorStyles.helpBox);

        if (GUILayout.Button(nameof(loopBreaker.BreakLoop)))
        {
            loopBreaker.BreakLoop();
        }

        GUILayout.EndVertical();
    }
}