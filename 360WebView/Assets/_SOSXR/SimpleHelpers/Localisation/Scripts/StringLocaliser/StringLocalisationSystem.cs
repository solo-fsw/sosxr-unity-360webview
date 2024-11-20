using System;
using System.Collections.Generic;
using UnityEditor;


namespace _mrstruijk.Localisation
{
    /// <summary>
    ///     From: GameDevGuide
    ///     https://youtu.be/c-dzg4M20wY
    ///     https://youtu.be/E-PR0d0Jb5A
    /// </summary>
    public static class StringLocalisationSystem
    {
        private static Dictionary<string, string> _localisedNL;
        private static Dictionary<string, string> _localisedEN; // Add other languages here

        private static bool _isInit;

        private static CSVLoader _csvLoader;


        public static void Init()
        {
            _csvLoader = new CSVLoader();

            UpdateDictionaries();

            _isInit = true;
        }


        public static Dictionary<string, string> GetDictionaryForEditor()
        {
            if (!_isInit)
            {
                Init();
            }

            switch (LanguageSelector.Language)
            {
                case Languages.NL:
                    return _localisedNL;
                case Languages.EN:
                    return _localisedEN; // Add other languages here
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private static void UpdateDictionaries()
        {
            _localisedNL = _csvLoader.GetDictionaryValues("nl");
            _localisedEN = _csvLoader.GetDictionaryValues("en"); // Add other languages here

            RefreshEditorAssetDatabase();
        }


        private static void RefreshEditorAssetDatabase()
        {
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }


        public static string GetLocalisedValue(string key)
        {
            if (!_isInit)
            {
                Init();
            }

            string value;

            switch (LanguageSelector.Language)
            {
                case Languages.NL:
                    _localisedNL.TryGetValue(key, out value);

                    break;
                case Languages.EN:
                    _localisedEN.TryGetValue(key, out value);

                    break; // Add other languages here
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return value;
        }


        #if UNITY_EDITOR
        public static void Add(string key, string value)
        {
            if (GetLocalisedValue(key) != null)
            {
                return;
            }

            CheckForQuotationmarks(value);

            CheckForCsvLoader();

            _csvLoader.Add(key, value);

            UpdateDictionaries();
        }


        public static void Replace(string key, string value)
        {
            Remove(key);
            Add(key, value);
        }


        public static void Remove(string key)
        {
            CheckForCsvLoader();

            _csvLoader.Remove(key);

            UpdateDictionaries();
        }


        private static void CheckForQuotationmarks(string value)
        {
            if (value.Contains("\""))
            {
                value.Replace('"', '\"');
            }
        }


        private static void CheckForCsvLoader()
        {
            _csvLoader ??= new CSVLoader();
        }
        #endif
    }
}