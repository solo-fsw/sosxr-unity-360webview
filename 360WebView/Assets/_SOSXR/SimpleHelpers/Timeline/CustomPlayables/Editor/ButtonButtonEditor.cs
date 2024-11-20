using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(ButtonButton), true)]
[CanEditMultipleObjects]
public class ButtonButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var buttonButton = (ButtonButton) target;

        if (GUILayout.Button(nameof(buttonButton.Click)))
        {
            buttonButton.Click();
        }
    }
}