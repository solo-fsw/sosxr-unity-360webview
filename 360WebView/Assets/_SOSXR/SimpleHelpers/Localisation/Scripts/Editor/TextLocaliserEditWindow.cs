using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


namespace _mrstruijk.Localisation.Editor
{
    public class TextLocaliserEditWindow : EditorWindow
    {
        [FormerlySerializedAs("key")] public string Key;
        [FormerlySerializedAs("value")] public string Value;


        public void OnGUI()
        {
            Key = EditorGUILayout.TextField("Key:", Key);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(LanguageSelector.Language + " text:", GUILayout.MaxWidth(60));
            EditorStyles.textArea.wordWrap = true;

            Value = EditorGUILayout.TextArea(Value, EditorStyles.textArea, GUILayout.Height(100),
                GUILayout.Width(5000));

            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Save to system"))
            {
                if (StringLocalisationSystem.GetLocalisedValue(Key) == null)
                {
                    StringLocalisationSystem.Add(Key, Value);
                    Close();
                }
                else
                {
                    if (EditorUtility.DisplayDialog("Replace" + Key + "?",
                            "Key '" + Key + "' already known. Replacing will also remove other languages", "Do it",
                            "Cancel"))
                    {
                        if (true)
                        {
                            StringLocalisationSystem.Replace(Key, Value);
                            Close();
                        }
                    }
                }
            }

            minSize = new Vector2(460, 250);
        }


        public static void Open(string key)
        {
            if (LanguageSelector.Language != Languages.NL)
            {
                EditorUtility.DisplayDialog("NL Only", "Enkel Nederlands kan via de Editor worden ingevoerd", "Ok");

                return;
            }

            // var window = new TextLocaliserEditWindow();
            var window = CreateInstance<TextLocaliserEditWindow>();
            window.titleContent = new GUIContent("Localiser Window");
            window.ShowUtility();
            window.Key = key;
        }
    }
}