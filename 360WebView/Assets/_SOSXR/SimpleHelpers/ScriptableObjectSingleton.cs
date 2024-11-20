using SOSXR.EnhancedLogger;
using UnityEngine;


public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            var results = Resources.FindObjectsOfTypeAll<T>();

            if (results.Length == 0)
            {
                Log.Error("ScriptableObjectSingleton",
                    "SingletonScriptableObject -> Instance -> results length is 0 for type ", typeof(T));

                return null;
            }

            if (results.Length > 1)
            {
                Log.Error("ScriptableObjectSingleton",
                    "SingletonScriptableObject -> Instance -> results length is greater than 1 for type ", typeof(T));

                return null;
            }

            _instance = results[0];

            return _instance;
        }
    }
}