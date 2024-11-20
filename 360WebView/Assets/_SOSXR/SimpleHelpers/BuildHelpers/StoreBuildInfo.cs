#if UNITY_EDITOR

using System;
using System.IO;
using SOSXR.EnhancedLogger;
using UnityEditor;
using UnityEngine;


/// <summary>
///     Automatically logs build information including Unity version
/// </summary>
public static class StoreBuildInfo
{
    public static void UpdateBuildInfo()
    {
        var unityVersion = Application.unityVersion; // Get Unity version
        var isProductionBuild = !EditorUserBuildSettings.development; //Debug.isDebugBuild; is not always reliable at this point
        var semVer = PlayerSettings.bundleVersion;
        var bundleCode = PlayerSettings.Android.bundleVersionCode.ToString();
        var buildDate = DateTime.Now.ToString("yyyy-MM-dd");
        var buildTime = DateTime.Now.ToString("HH:mm");

        if (!File.Exists(BuildInfo.FilePath))
        {
            using var sw = File.CreateText(BuildInfo.FilePath);

            sw.WriteLine("UnityVersion,ProductionBuild,SemVer,BundleCode,BuildDate,BuildTime");

            Log.Debug(nameof(StoreBuildInfo), "Created new build_info.csv with headers at", BuildInfo.FilePath);
        }

        using (var sw = File.AppendText(BuildInfo.FilePath))
        {
            sw.WriteLine($"{unityVersion},{isProductionBuild},{semVer},{bundleCode},{buildDate},{buildTime}");

            Log.Debug(nameof(StoreBuildInfo), "Appended new build information to build_info.csv at",
                BuildInfo.FilePath);
        }
    }
}
#endif