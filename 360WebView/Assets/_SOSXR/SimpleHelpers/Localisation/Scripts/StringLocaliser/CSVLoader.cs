using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;


namespace _mrstruijk.Localisation
{
    /// <summary>
    ///     From: GameDevGuide
    ///     https://youtu.be/c-dzg4M20wY
    ///     https://youtu.be/E-PR0d0Jb5A
    /// </summary>
    public class CSVLoader
    {
        private readonly string[] _fieldSeperator = {"\",\""};

        private bool _csvLoaded;


        public CSVLoader()
        {
            LoadCSV();
        }


        public TextAsset CSVFile { get; private set; }
        private const char Surround = '"';
        private const string FileName = "localisation";
        private const string FilePath = "Assets/_mrstruijk/Components/Localisation/Resources/";
        private const string ParserPattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";
        private const char LineSeperator = '\n';


        private void LoadCSV()
        {
            CSVFile = Resources.Load<TextAsset>(FileName);
            _csvLoaded = true;
        }


        public Dictionary<string, string> GetDictionaryValues(string selectedLanguage)
        {
            if (_csvLoaded == false)
            {
                LoadCSV();
            }

            var dictionary = new Dictionary<string, string>();

            var lines = CSVFile.text.Split(LineSeperator);

            var languageIndex = -1;

            var headers = lines[0].Split(_fieldSeperator, StringSplitOptions.None);

            for (var i = 0; i < headers.Length; i++)
            {
                if (!headers[i].Contains(selectedLanguage))
                {
                    continue;
                }

                languageIndex = i;

                break;
            }

            var csvParser = new Regex(ParserPattern);

            for (var i = 1; i < lines.Length; i++)
            {
                var line = lines[i];

                var fields = csvParser.Split(line);

                for (var f = 0; f < fields.Length; f++)
                {
                    fields[f] = fields[f].TrimStart(' ', Surround);
                    fields[f] = fields[f].TrimEnd(Surround);
                }

                if (fields.Length <= languageIndex)
                {
                    continue;
                }

                var key = fields[0];

                if (dictionary.ContainsKey(key))
                {
                    continue;
                }

                var value = fields[languageIndex];

                dictionary.Add(key, value);
            }

            return dictionary;
        }


        #if UNITY_EDITOR
        public void Add(string key, string value)
        {
            LoadCSV();

            var appended = $"\n\"{key}\",\"{value}\",\"\"";

            File.AppendAllText(FilePath + FileName + ".csv", appended);

            AssetDatabase.Refresh();

            LoadCSV();
        }


        public void Remove(string key)
        {
            LoadCSV();

            var lines = CSVFile.text.Split(LineSeperator);
            var keys = new string[lines.Length];

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                keys[i] = line.Split(_fieldSeperator, StringSplitOptions.None)[0];
            }

            var index = -1;

            for (var i = 0; i < keys.Length; i++)
            {
                if (!keys[i].Contains(key))
                {
                    continue;
                }

                index = i;

                break;
            }

            if (index > -1)
            {
                var newLines = lines.Where(w => w != lines[index]).ToArray();

                var replaced = string.Join(LineSeperator.ToString(), newLines);

                File.WriteAllText(FilePath + FileName + ".csv", replaced);
            }

            AssetDatabase.Refresh();

            LoadCSV();
        }
        #endif
    }
}