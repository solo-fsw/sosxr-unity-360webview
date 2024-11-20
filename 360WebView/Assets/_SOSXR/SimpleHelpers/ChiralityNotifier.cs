using System;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class ChiralityNotifier : MonoBehaviour
{
    public void GrabbedWithHand(Chirality chirality)
    {
        this.Info("Grabbed with", chirality);
        EventsSystem.GrabbedWithHand?.Invoke(chirality);
    }


    public void GrabbedWithHand(int chirality)
    {
        if (chirality >= 0 && chirality < Enum.GetValues(typeof(Chirality)).Length)
        {
            this.Info("Grabbed with", chirality, "which maps to", (Chirality) chirality);
            GrabbedWithHand((Chirality) chirality);
        }
        else
        {
            this.Warning("Invalid chirality value: " + chirality);
        }
    }


    public void ReleasedWithHand(Chirality chirality)
    {
        this.Info("Released from", chirality);
        EventsSystem.ReleasedWithHand?.Invoke(chirality);
    }


    public void ReleasedWithHand(int chirality)
    {
        if (chirality >= 0 && chirality < Enum.GetValues(typeof(Chirality)).Length)
        {
            this.Info("Released with", chirality, "which maps to", (Chirality) chirality);
            ReleasedWithHand((Chirality) chirality);
        }
        else
        {
            this.Warning("Invalid chirality value: " + chirality);
        }
    }
}