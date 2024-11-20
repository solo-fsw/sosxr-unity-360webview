#if UNITY_EDITOR

using System.IO;
using UnityEditor;


namespace mrstruijk.SimpleHelpers
{
    /// <summary>
    ///     From: https://forum.unity.com/threads/how-can-i-create-text-files.1425135/
    /// </summary>
    public static class CreateTextFile
    {
        [MenuItem("Assets/Create/SOSXR/New Text File", priority = 100)]
        private static void CreateNewTextFile()
        {
            var folderGUID = Selection.assetGUIDs[0];
            var projectFolderPath = AssetDatabase.GUIDToAssetPath(folderGUID);
            var folderDirectory = Path.GetFullPath(projectFolderPath);

            using (var sw = File.CreateText(folderDirectory + "/NewTextFile.md"))
            {
                sw.WriteLine("# New Text File");
            }

            AssetDatabase.Refresh();
        }
    }
}
#endif