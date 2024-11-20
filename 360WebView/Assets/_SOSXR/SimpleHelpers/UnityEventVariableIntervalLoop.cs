using System.Collections;
using mrstruijk;
using mrstruijk.Extensions;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
///     Class to fire a Unity Event every x interval.
///     With options to change the interval on every time it fires.
///     With options to set a min/max allowed interval
/// </summary>
public class UnityEventVariableIntervalLoop : MonoBehaviour
{
    [SerializeField] private UnityEvent m_eventToFire;

    [Header("Settings")]
    [SerializeField] private bool m_autoStart;
    [SerializeField] private float m_initialFireInterval = 1.3f;
    [SerializeField] private float m_perIntervalChange = 0.02f;
    [SerializeField] private Vector2 m_minMax = new(0.5f, 2f);
    [Space(5)]
    [SerializeField] [DisableEditing] private float m_currentFireInterval;

    private Coroutine _coroutine;


    private void OnValidate()
    {
        m_currentFireInterval = m_initialFireInterval;
    }


    private void Start()
    {
        if (m_autoStart)
        {
            StartEventFiringLoop();
        }
    }


    public void StartEventFiringLoop()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(FireEventOnLoopCR());
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            _coroutine = StartCoroutine(FireEventOnLoopCR());
        }
    }


    private IEnumerator FireEventOnLoopCR()
    {
        m_currentFireInterval = m_initialFireInterval;

        for (;;)
        {
            m_eventToFire?.Invoke();

            yield return new WaitForSeconds(m_currentFireInterval);

            if (m_currentFireInterval + m_perIntervalChange >= m_minMax.x &&
                m_currentFireInterval + m_perIntervalChange <= m_minMax.y)
            {
                m_currentFireInterval += m_perIntervalChange;
                m_currentFireInterval = m_currentFireInterval.RoundCorrectly(3);
            }
        }
    }


    public void StopFiringLoop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }


    private void OnDisable()
    {
        StopAllCoroutines();
    }
}