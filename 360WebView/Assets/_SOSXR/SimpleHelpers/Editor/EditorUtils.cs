using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.AssetDatabase;


public static class EditorUtils
{
    public static List<T> GetAssetWithScript<T>(string path, string filter = "t:Prefab") where T : MonoBehaviour
    {
        var assetList = new List<T>();
        var guids = FindAssets(filter, new[] {path});

        AddPrefabsWithScriptToList(guids, assetList);

        return assetList;
    }


    public static List<T> GetScriptableObject<T>(string path, string filter = "t:ScriptObj") where T : ScriptableObject
    {
        var assetList = new List<T>();
        var guids = FindAssets(filter, new[] {path});

        AddPrefabsWithScriptToList(guids, assetList);

        return assetList;
    }


    private static void AddPrefabsWithScriptToList<T>(IEnumerable<string> guids, List<T> assetList)
    {
        assetList.AddRange(guids.Select(GUIDToAssetPath)
                                .Select(assetPath => LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject)
                                .Select(prefab => prefab.GetComponent<T>()).Where(script => script != null));
    }


    public static List<T> GetListFromEnum<T>()
    {
        var enums = Enum.GetValues(typeof(T));

        return enums.Cast<T>().ToList();
    }
}