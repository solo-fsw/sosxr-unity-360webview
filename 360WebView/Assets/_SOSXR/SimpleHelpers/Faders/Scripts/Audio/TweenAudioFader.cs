// using DG.Tweening;

using UnityEngine;


public class TweenAudioFader : MonoBehaviour
{
    [SerializeField] private AudioSource m_source;
    [SerializeField] [Range(0f, 5f)] private float m_fadeDuration = 5f;
    [SerializeField] private AnimationCurve m_fadeCurve = AnimationCurve.EaseInOut(0, 1, 1, 0);


    private void Awake()
    {
        if (m_source == null)
        {
            m_source = GetComponent<AudioSource>();
        }
    }


    public void StartAudioFade()
    {
        // m_source.DOFade(0, m_fadeDuration).SetEase(m_fadeCurve);
    }
}