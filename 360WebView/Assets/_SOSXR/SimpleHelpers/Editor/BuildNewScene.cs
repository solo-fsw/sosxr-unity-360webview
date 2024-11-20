using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace mrstruijk.Tools
{
    public static class BuildNewScene
    {
        public static void CreateNewScene()
        {
            SceneCreator();
            CleanScene();
        }


        private static void SceneCreator()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects);
            }
            else if (Application.isEditor && Application.isPlaying)
            {
                SceneManager.CreateScene("New Scene");
            }
        }


        private static void CleanScene()
        {
            var allObjects = Object.FindObjectsOfType<GameObject>();

            foreach (var go in allObjects)
            {
                Object.DestroyImmediate(go);
            }
        }
    }
}