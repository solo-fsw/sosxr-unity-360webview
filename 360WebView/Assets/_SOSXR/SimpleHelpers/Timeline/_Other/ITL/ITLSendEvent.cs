using UnityEngine;


public abstract class ITLSendEvent : MonoBehaviour, ITLActivate
{
    public void TLActivate()
    {
        SendEvent();
    }


    public abstract void SendEvent();
}