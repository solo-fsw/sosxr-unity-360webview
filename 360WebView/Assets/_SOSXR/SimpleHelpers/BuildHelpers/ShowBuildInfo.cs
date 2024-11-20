using System;
using System.IO;
using TMPro;
using UnityEngine;


public class ShowBuildInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_buildInfoText;

    private string _currentTime;


    private void Start()
    {
        SetBuildText();
    }


    private void SetBuildText()
    {
        m_buildInfoText.enabled = true;
        enabled = true;

        var textAsset = Resources.Load<TextAsset>("build_info");

        if (textAsset == null)
        {
            m_buildInfoText.text = "No build information available.";

            return;
        }

        var fileContent = textAsset.text;
        var lastLine = ReadLastLineOfFile(fileContent);

        if (lastLine != null)
        {
            m_buildInfoText.text = FormatBuildInfo(lastLine);
        }
        else
        {
            m_buildInfoText.text = "No build information available.";
        }
    }


    private string ReadLastLineOfFile(string fileContent)
    {
        string lastLine = null;

        using var sr = new StringReader(fileContent);

        while (sr.ReadLine() is { } line)
        {
            lastLine = line;
        }

        return lastLine;
    }


    private void Update()
    {
        _currentTime = DateTime.Now.ToString("HH:mm");
    }


    private string FormatBuildInfo(string line)
    {
        // Assuming the format is "SemVer,BundleCode,Build Date,Build Time"
        var parts = line.Split(',');

        if (parts.Length == 5)
        {
            var devBuild = parts[0];
            var semVer = parts[1];
            var bundleCode = parts[2];
            var buildDate = parts[3];
            var buildTime = parts[4];

            var buildDateTime = DateTime.ParseExact($"{buildDate} {buildTime}", "yyyy-MM-dd HH:mm", null);
            var now = DateTime.Now;
            _currentTime = now.ToString("HH:mm");

            var relativeDate = GetRelativeDateDescription(buildDateTime.Date, now.Date);

            return
                $"DevBuild: {devBuild}\nSemVer: {semVer}\nBundleCode: {bundleCode}\nBuild Date: {relativeDate} at {buildTime}\nCurrent Time: {_currentTime}";
        }

        return "Invalid build information format.";
    }


    private string GetRelativeDateDescription(DateTime buildDate, DateTime currentDate)
    {
        var daysDifference = (currentDate - buildDate).Days;

        return daysDifference switch
               {
                   0 => "today",
                   1 => "yesterday",
                   _ => $"{daysDifference} days ago"
               };
    }
}