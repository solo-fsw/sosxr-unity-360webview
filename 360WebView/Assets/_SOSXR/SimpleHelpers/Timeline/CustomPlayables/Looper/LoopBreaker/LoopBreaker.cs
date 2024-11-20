using System.Collections.Generic;
using SOSXR.EnhancedLogger;
using UnityEngine;


public class LoopBreaker : MonoBehaviour
{
    public bool RequiresControl = true;


    public LooperState StateUponBreaking = LooperState.GoToEnd;

    private readonly List<LooperBehaviour> _loopers = new();
    private LooperBehaviour _currentLooper;

    private bool _hasControl;

    private bool _initialised;


    public void LocalMethod()
    {
        this.Error("This is not being called");
        BreakLoop();
    }


    public void Init(LooperBehaviour looperBehaviour)
    {
        if (looperBehaviour == null)
        {
            return;
        }

        _loopers.Add(looperBehaviour);

        _initialised = true;
    }


    public void SetCurrentLooper(LooperBehaviour currentLooperBehaviour)
    {
        if (currentLooperBehaviour == null)
        {
            return;
        }

        if (!_loopers.Contains(currentLooperBehaviour))
        {
            this.Warning("The currentLooperBehaviour is not known in the LooperBehaviour list!");
        }

        _currentLooper = currentLooperBehaviour;

        this.Debug("Set", _currentLooper, "to", currentLooperBehaviour, "with state",
            currentLooperBehaviour.runningLooperState);
    }


    public void TakeControl()
    {
        _hasControl = true;
    }


    public void BreakLoop()
    {
        if (!IsInitialised())
        {
            this.Warning("Somehow I'm not initialised");

            return;
        }

        if (LacksRequiredControl())
        {
            this.Warning(
                "LoopBreaker has no control. Probably waiting for Timeline to set control at the right moment.");

            return;
        }

        if (CurrentLooperIsAbsent())
        {
            this.Warning("LoopBreaker is initialised, but the Looper is absent, therefore cannot break loop.");

            return;
        }

        _currentLooper.runningLooperState = StateUponBreaking;

        this.Debug("The currentLoopoerState is now", _currentLooper.runningLooperState, "becaue I broke it to",
            StateUponBreaking);

        DeInitialise();
    }


    private bool LacksRequiredControl()
    {
        return RequiresControl && _hasControl == false;
    }


    private bool CurrentLooperIsAbsent()
    {
        return _currentLooper == null;
    }


    private bool IsInitialised()
    {
        return _initialised;
    }


    private void DeInitialise()
    {
        _loopers.Remove(_currentLooper);
        _currentLooper = null;
        _initialised = false;

        this.Debug("Deinit the breaker");
    }
}