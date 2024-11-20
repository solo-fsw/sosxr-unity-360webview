#if UNITY_EDITOR
using SOSXR.EnhancedLogger;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;


public class BuildHelpersInCorrectOrder : IPreprocessBuildWithReport
{
    public int callbackOrder { get; }


    /// <summary>
    ///     Even though with Preprocess also unsuccessful builds are numbered, this approach causes fewer problems than the
    ///     PostProcess if you also want to display the build info in the build that it's happening in.
    /// </summary>
    /// <param name="report"></param>
    public void OnPreprocessBuild(BuildReport report)
    {
        IncreaseSemVer.IncrementSemVer();
        IncreaseAndroidBundleVersionCode.IncrementBundleCode();
        StoreBuildInfo.UpdateBuildInfo();

        Log.Debug(nameof(BuildHelpersInCorrectOrder), "Incremented build numbers and logged in .csv");
    }
}
#endif