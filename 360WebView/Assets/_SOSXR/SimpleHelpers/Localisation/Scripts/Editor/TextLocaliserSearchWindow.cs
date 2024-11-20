using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


namespace _mrstruijk.Localisation.Editor
{
    public class TextLocaliserSearchWindow : EditorWindow
    {
        [FormerlySerializedAs("value")] public string Value;
        [FormerlySerializedAs("scroll")] public Vector2 Scroll;

        private Dictionary<string, string> _dictionary;


        private void OnEnable()
        {
            _dictionary = StringLocalisationSystem.GetDictionaryForEditor();
        }


        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);

            Value = EditorGUILayout.TextField(Value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }


        public static void Open()
        {
            //var window = new TextLocaliserSearchWindow();
            var window = CreateInstance<TextLocaliserSearchWindow>();

            window.titleContent = new GUIContent("Search in Localiser");

            var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            var rect = new Rect(mousePosition.x - 450, mousePosition.y + 10, 10, 10);
            window.ShowAsDropDown(rect, new Vector2(500, 300));
        }


        private void GetSearchResults()
        {
            Value ??= "";

            EditorGUILayout.BeginVertical();
            Scroll = EditorGUILayout.BeginScrollView(Scroll);

            foreach (var (key, s) in _dictionary)
            {
                if (!key.ToLower().Contains(Value.ToLower()) && !s.ToLower().Contains(Value.ToLower()))
                {
                    continue;
                }

                EditorGUILayout.BeginHorizontal("box");
                var removeIcon = (Texture) Resources.Load("removeIcon");
                var guiStyle = new GUIStyle();
                var removeButton = new GUIContent(removeIcon);

                if (GUILayout.Button(removeButton, guiStyle, GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                {
                    if (EditorUtility.DisplayDialog("Remove " + key + "?",
                            "This will remove '" + key + "' from localisation system, are you sure?", "Do it", "NOPE!"))
                    {
                        StringLocalisationSystem.Remove(key);
                        AssetDatabase.Refresh();
                        StringLocalisationSystem.Init();
                        _dictionary = StringLocalisationSystem.GetDictionaryForEditor();
                    }
                }

                EditorGUILayout.TextField(key);
                EditorGUILayout.LabelField(s);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}