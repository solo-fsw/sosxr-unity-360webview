using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;


[RequireComponent(typeof(ChiralXRInteractor))]
public class XRHaptics : MonoBehaviour
{
    [FormerlySerializedAs("_amplitude")] [Range(0.01f, 0.5f)] [SerializeField] private float m_amplitude = 0.1f;
    [FormerlySerializedAs("_duration")] [Range(0.01f, 1.5f)] [SerializeField] private float m_duration = 1.5f;
    private InputDeviceCharacteristics _chirality;
    private List<InputDevice> _foundControllers = new();

    private bool _isVibrating;
    private const uint Channel = 0;


    private void Awake()
    {
        switch (GetComponent<ChiralXRInteractor>().Chirality)
        {
            case Chirality.Left:
                _chirality = InputDeviceCharacteristics.Left;

                break;
            case Chirality.Right:
                _chirality = InputDeviceCharacteristics.Right;

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private List<InputDevice> FindHapticsControllers()
    {
        var trackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice |
                                      _chirality;

        var foundControllers = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(trackedControllerFilter, foundControllers);

        return foundControllers;
    }


    public void SendHapticsImpulse()
    {
        _foundControllers ??= FindHapticsControllers();

        foreach (var controller in _foundControllers)
        {
            if (!controller.TryGetHapticCapabilities(out var capabilities))
            {
                continue;
            }

            if (!capabilities.supportsImpulse)
            {
                continue;
            }

            controller.SendHapticImpulse(Channel, m_amplitude, m_duration);
        }

        _isVibrating = true;
    }


    public void StopHapticsImpulse()
    {
        if (_isVibrating == false)
        {
            return;
        }

        _foundControllers ??= FindHapticsControllers();

        foreach (var controller in _foundControllers)
        {
            if (!controller.TryGetHapticCapabilities(out var capabilities))
            {
                continue;
            }

            if (!capabilities.supportsImpulse)
            {
                continue;
            }

            controller.StopHaptics();
        }

        _isVibrating = false;
    }


    public void ClearHapticsControllers()
    {
        if (_foundControllers != null)
        {
            _foundControllers = new List<InputDevice>();
        }
    }
}