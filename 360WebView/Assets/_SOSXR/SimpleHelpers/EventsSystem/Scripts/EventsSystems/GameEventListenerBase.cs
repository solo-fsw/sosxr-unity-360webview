using UnityEngine;


public abstract class GameEventListenerBase : MonoBehaviour
{
    public GameEventSO GameEvent;


    private void Awake()
    {
        GameEvent.RegisterListener(this);
    }


    private void OnDestroy()
    {
        GameEvent.UnregisterListener(this);
    }


    public abstract void OnEventRaised();
}