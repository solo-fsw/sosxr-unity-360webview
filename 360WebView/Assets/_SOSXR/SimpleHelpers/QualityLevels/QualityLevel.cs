using System;
using System.Linq;
using System.Text.RegularExpressions;
using mrstruijk;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class QualityLevel : MonoBehaviour
{
    [SerializeField] private bool m_debugSimulateTabletInEditor = true;
    [DisableEditing] [SerializeField] private string[] m_availableQualityLevels;
    [DisableEditing] [SerializeField] private string m_currentQualityLevel;
    [SerializeField] private CurrentDevice m_platform;
    [DisableEditing] [SerializeField] private string m_defaultQualityLevel = "Quest 2";


    private void Awake()
    {
        m_availableQualityLevels = QualitySettings.names;
    }


    private void OnEnable()
    {
        EventsSystem.DeviceHasBeenSet += ChangeQualityLevel;
    }


    private void Start()
    {
        SetDefaultQualityLevel();
    }


    private void SetDefaultQualityLevel()
    {
        var index = Array.IndexOf(m_availableQualityLevels, m_defaultQualityLevel);
        QualitySettings.SetQualityLevel(index);
        m_currentQualityLevel = "DEFAULT: " + m_defaultQualityLevel;
    }


    private void ChangeQualityLevel()
    {
        if (m_debugSimulateTabletInEditor && m_platform.DeviceName == "MBP")
        {
            SetQualityLevel("Galaxy Tab A7",
                "We're debugging in the editor, and assuming we're simulating the tablet: ");
        }
        else if (IsSMT500Device(m_platform.DeviceName))
        {
            SetQualityLevel("SM-T500", "We set the Quality Settings to SM-T500");
        }
        else if (m_availableQualityLevels.Contains(m_platform.DeviceName))
        {
            SetQualityLevel(m_platform.DeviceName, "We set the Quality Settings to " + m_platform.DeviceName);
        }
    }


    private bool IsSMT500Device(string deviceName)
    {
        return Regex.IsMatch(deviceName, @"^SM-T500 \d+$");
    }


    private void SetQualityLevel(string qualityLevel, string successMessage)
    {
        var index = Array.IndexOf(m_availableQualityLevels, qualityLevel);

        if (index == -1)
        {
            return;
        }

        QualitySettings.SetQualityLevel(index);
        this.Success(successMessage, qualityLevel);
        m_currentQualityLevel = m_availableQualityLevels[index];
    }


    private void OnDisable()
    {
        EventsSystem.DeviceHasBeenSet -= ChangeQualityLevel;
    }
}