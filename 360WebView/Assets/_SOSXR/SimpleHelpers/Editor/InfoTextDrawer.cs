using UnityEditor;
using UnityEngine;


namespace mrstruijk.Tools
{
    [CustomPropertyDrawer(typeof(InfoTextAttribute))]
    public class InfoTextDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is not InfoTextAttribute infoTextAttribute)
            {
                return;
            }

            EditorGUI.HelpBox(position, infoTextAttribute.InfoText, MessageType.Info);
        }
    }
}