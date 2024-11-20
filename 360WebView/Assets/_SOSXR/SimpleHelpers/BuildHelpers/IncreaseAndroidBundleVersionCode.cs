#if UNITY_EDITOR

using SOSXR.EnhancedLogger;
using UnityEditor;


/// <summary>
///     Automatically increase the BundleVersionCode that is required by Android
/// </summary>
public static class IncreaseAndroidBundleVersionCode
{
    private static int _oldBundleVersionCode;
    private static int _newBundleVersionCode;


    public static void IncrementBundleCode()
    {
        _oldBundleVersionCode = PlayerSettings.Android.bundleVersionCode;
        _newBundleVersionCode = PlayerSettings.Android.bundleVersionCode + 1;
        PlayerSettings.Android.bundleVersionCode = _newBundleVersionCode;

        Log.Warning(nameof(IncreaseAndroidBundleVersionCode),
            "Incremented the Android BundleVersionCode from", _oldBundleVersionCode, "to", _newBundleVersionCode);
    }
}
#endif