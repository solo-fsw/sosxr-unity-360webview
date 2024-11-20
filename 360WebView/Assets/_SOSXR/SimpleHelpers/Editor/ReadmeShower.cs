using System.IO;
using UnityEditor;
using UnityEngine;


[InitializeOnLoad]
public class ReadmeShower : Editor
{
    private static readonly string kShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";


    static ReadmeShower()
    {
        EditorApplication.delayCall += SelectReadmeAutomatically; // Show on Editor Start
    }


    private static void SelectReadmeAutomatically()
    {
        if (SessionState.GetBool(kShowedReadmeSessionStateName, false))
        {
            return;
        }

        var readmeContent = SelectReadme();
        SessionState.SetBool(kShowedReadmeSessionStateName, true);
    }


    [MenuItem("mrstruijk/Readme")]
    private static string SelectReadme()
    {
        var mdFilePaths = Directory.GetFiles(Application.dataPath, "*.md", SearchOption.AllDirectories);

        if (mdFilePaths.Length >= 1)
        {
            var mdFilePath =
                mdFilePaths[0]; // Select the first .md file found (you can change the logic here if needed)

            var readmeContent = File.ReadAllText(mdFilePath);

            // Convert absolute path to relative path
            var relativePath = "Assets" + mdFilePath.Replace(Application.dataPath, "").Replace('\\', '/');
            var asset = AssetDatabase.LoadAssetAtPath<Object>(relativePath);

            if (asset != null)
            {
                Selection.activeObject = asset;
            }
            else
            {
                Debug.LogWarning(
                    "The .md file was not found as an asset in the AssetDatabase. It might not be imported as an asset.");
            }

            return readmeContent;
        }

        Debug.Log("Couldn't find a readme file with .md extension");

        return null;
    }
}


/*
README

Finally found a way in which this README could be useful.
Previously this was always kind of hidden inside Github only, and not accessible from Unity.
Therefore it wasn't too easy to actually use from Unity.
Now I have a symbolic link from the README that's rendered in Github to the one that's here in the Assets folder.
That was possible by doing:

  # From your repository root
  mkdir .github
  cd .github
  ln -s ../PROJECT_NAME/Assets/README.md README.md

And to get Markdown rendering in Unity: https://github.com/gwaredd/UnityMarkdownViewer
*/