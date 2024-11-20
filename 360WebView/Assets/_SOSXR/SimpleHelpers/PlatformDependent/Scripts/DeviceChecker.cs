using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;


#if UNITY_EDITOR
// using ParrelSync;
#endif


public class DeviceChecker : MonoBehaviour
{
    [SerializeField] private CurrentDevice m_platform;

    [SerializeField] private bool m_checkOnStart = true;
    [SerializeField] private bool m_notifyOnStart = true;
    [SerializeField] private bool m_parralHMD = true;

    [SerializeField] private UnityEvent m_deviceHasBeenSet;
    [SerializeField] private UnityEvent m_isHMD;
    [SerializeField] private UnityEvent m_isTablet;
    [SerializeField] private UnityEvent m_isEditor;


    private void Awake()
    {
        if (!Application.isEditor)
        {
            m_platform.Current = Device.None;
            m_checkOnStart = true;
        }

        CheckIfParrelSync();
    }


    private void CheckIfParrelSync()
    {
        // #if UNITY_EDITOR
        // if (ClonesManager.IsClone() && m_parralHMD)
        // {
        //     m_platform.Current = Device.HMD;
        //     m_checkOnStart = false;
        //     this.Success("Parral HMD activated");
        //     m_isHMD?.Invoke();
        // }
        // #endif
    }


    private void Start()
    {
        if (m_checkOnStart == false)
        {
            if (m_notifyOnStart)
            {
                NotifyOfCurrentDevice();
            }

            return;
        }

        CheckDevice();
    }


    private void CheckDevice()
    {
        if (!Application.isEditor)
        {
            if (XRSettings.isDeviceActive)
            {
                m_platform.Current = Device.HMD;
                m_isHMD?.Invoke();
            }
            else
            {
                m_platform.Current = Device.Tablet;
                m_isTablet?.Invoke();
            }
        }
        else
        {
            m_platform.Current = Device.Editor;
            m_isEditor?.Invoke();
        }

        m_platform.DeviceName = SystemInfo.deviceName;

        NotifyOfCurrentDevice();
    }


    private void NotifyOfCurrentDevice()
    {
        this.Info("Running on", m_platform.DeviceName, "with Current Device", m_platform.Current);

        m_deviceHasBeenSet?.Invoke();
        EventsSystem.DeviceHasBeenSet?.Invoke();
    }


    private void DebugHMD()
    {
        m_platform.Current = Device.HMD;
    }


    private void DebugTablet()
    {
        m_platform.Current = Device.Tablet;
    }


    private void DebugEditor()
    {
        m_platform.Current = Device.Editor;
    }
}