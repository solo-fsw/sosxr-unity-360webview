using mrstruijk.Extensions;
using SOSXR.EnhancedLogger;
using TMPro;
using UnityEngine;


public class TMPFaderOnLooking : MonoBehaviour
{
    [SerializeField] [TagSelector] private string m_objectToLookFromTag = "MainCamera";

    [Tooltip("For instance the camera")]
    [SerializeField] private Transform m_objectToLookFrom;
    [SerializeField] private Transform m_objectToLookAt;

    [SerializeField] private AnimationCurve m_curve = AnimationCurve.EaseInOut(0.9f, 0, 1, 1);

    [SerializeField] private TMPFadeTypes[] m_fadeTexts;

    [SerializeField] private bool m_startAutomatically = false;

    private bool _changeAlphas = false;
    private TMPDefaultAlphas _defaultAlphas;
    private float _desiredFadeInAlpha;


    private void Start()
    {
        StoreDefaultAlphas();

        if (m_startAutomatically)
        {
            StartChangingTMPAlphas();
        }
    }


    private void StoreDefaultAlphas()
    {
        _defaultAlphas = new TMPDefaultAlphas
        {
            Texts = new TextMeshProUGUI[m_fadeTexts.Length],
            Alphas = new float[m_fadeTexts.Length]
        };

        for (var i = 0; i < m_fadeTexts.Length; i++)
        {
            _defaultAlphas.Texts[i] = m_fadeTexts[i].TMP;
            _defaultAlphas.Alphas[i] = m_fadeTexts[i].TMP.alpha;
        }
    }


    public void StartChangingTMPAlphas()
    {
        if (_changeAlphas)
        {
            this.Warning("Already changing alphas");
        }

        _changeAlphas = true;
    }


    private void StopChangingTMPAlphas()
    {
        if (!_changeAlphas)
        {
            this.Warning("You are not changing the alphas anyway");
        }

        _changeAlphas = false;
    }


    private void ResetAlphasToDefault()
    {
        for (var i = 0; i < _defaultAlphas.Texts.Length; i++)
        {
            _defaultAlphas.Texts[i].alpha = _defaultAlphas.Alphas[i];
        }
    }


    public void StopChangingAlphasAndResetToDefault()
    {
        StopChangingTMPAlphas();
        ResetAlphasToDefault();
    }


    private void Update()
    {
        SetAlphasBasedOnLookAt();
    }


    private void SetAlphasBasedOnLookAt()
    {
        if (!_changeAlphas)
        {
            return;
        }

        if (m_objectToLookFrom == null)
        {
            if (GameObject.FindWithTag(m_objectToLookFromTag) != null)
            {
                m_objectToLookFrom = GameObject.FindWithTag(m_objectToLookFromTag).transform;
            }
        }

        if (m_objectToLookFrom == null)
        {
            this.Warning("No object to look from");

            return;
        }

        var remappedDot = m_objectToLookFrom.CalculateLookAtDotProduct(m_objectToLookAt, true);

        _desiredFadeInAlpha = m_curve.Evaluate(remappedDot);

        foreach (var fadeText in m_fadeTexts)
        {
            if (fadeText.FadeType == FadeType.FadeInOnLooking)
            {
                fadeText.TMP.alpha = _desiredFadeInAlpha;
            }
            else if (fadeText.FadeType == FadeType.FadeOutOnLooking)
            {
                fadeText.TMP.alpha = 1 - _desiredFadeInAlpha;
            }
        }
    }


    private void OnDrawGizmos()
    {
        var color = new Color(1 - _desiredFadeInAlpha, _desiredFadeInAlpha, 0,
            1); // red to green, based on the evaluated value

        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}