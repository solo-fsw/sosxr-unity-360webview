#if UNITY_EDITOR
using SOSXR.EnhancedLogger;
using UnityEditor;
using UnityEditor.Build;


public static class IncreaseSemVer
{
    private static IPreprocessBuildWithReport _preprocessBuildWithReportImplementation;
    private static string _oldVersion;
    private static string _newVersion;
    private static bool _isDevelopmentBuild;


    public static void IncrementSemVer()
    {
        _isDevelopmentBuild = EditorUserBuildSettings.development;
        _oldVersion = PlayerSettings.bundleVersion;
        _newVersion = IncrementVersionNumber(_oldVersion);
        PlayerSettings.bundleVersion = _newVersion;
        Log.Warning(nameof(IncreaseSemVer), "Incremented SemVer from", _oldVersion, "to", _newVersion);
    }


    private static string IncrementVersionNumber(string version)
    {
        // Remove any existing build indicator if present
        if (version.StartsWith("D") || version.StartsWith("P"))
        {
            version = version.Substring(1);
        }

        // Split the version string into parts
        var parts = version.Split('_');

        // Extract the last part (current number to increment)
        var lastNumberIndex = parts.Length - 1;
        var currentNumber = int.Parse(parts[lastNumberIndex]);
        currentNumber++;

        // Replace the last part with the incremented number
        parts[lastNumberIndex] = currentNumber.ToString();

        // Add build type indicator at the start
        var buildIndicator = EditorUserBuildSettings.development ? "D" : "P";
        Log.Warning(nameof(IncreaseSemVer), EditorUserBuildSettings.development);

        // Join all parts back into a single string and prepend the build indicator
        return buildIndicator + "" + string.Join("_", parts);
    }
}
#endif