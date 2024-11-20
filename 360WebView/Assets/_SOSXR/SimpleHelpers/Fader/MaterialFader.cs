using System;
using System.Collections;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;


// // using DG.Tweening;


namespace mrstruijk.SimpleHelpers
{
    public class MaterialFader : MonoBehaviour
    {
        [SerializeField] private Renderer m_fadeObjectRenderer;

        [SerializeField] private Material m_fadeMaterial;
        [SerializeField] [Range(0, 1)] private int m_startAlpha = 0;
        [SerializeField] private bool m_waitForFinish = false;

        [SerializeField] private bool m_fadeOnStart = true;
        [SerializeField] private Color m_fadeColor = Color.black;
        [SerializeField] [Range(0f, 10f)] private float m_preFadeInDuration = 2f;
        [SerializeField] [Range(0f, 10f)] private float m_fadeInDuration = 5f;
        [SerializeField] [Range(0f, 10f)] private float m_preFadeOutDuration = 0f;
        [SerializeField] [Range(0f, 10f)] private float m_fadeOutDuration = 5f;
        [SerializeField] private AnimationCurve m_fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;


        private void Start()
        {
            InitialiseFadeObject(m_startAlpha);

            if (m_fadeOnStart)
            {
                FadeIn();
            }
        }


        private void OnEnable()
        {
            EventsSystem.StartFadeOut += FadeOut;
            EventsSystem.StartFadeIn += FadeIn;
            EventsSystem.StartFadeRound += FadeRound;
        }


        /// <summary>
        ///     Fades out first, then fades back in
        /// </summary>
        public void FadeRound()
        {
            FadeOut();
            FadeIn();
        }


        private void FadeIn()
        {
            ClearCRIfNeeded();

            if (_fadeInCoroutine != null)
            {
                return;
            }

            _fadeInCoroutine = StartCoroutine(FadeInCR());
        }


        private void FadeOut()
        {
            ClearCRIfNeeded();

            if (_fadeOutCoroutine != null)
            {
                return;
            }

            _fadeOutCoroutine = StartCoroutine(FadeOutCR());
        }


        private void ClearCRIfNeeded()
        {
            if (!m_waitForFinish)
            {
                StopAllCoroutines();
                _fadeOutCoroutine = null;
                _fadeInCoroutine = null;
            }
        }


        private IEnumerator FadeInCR()
        {
            while (_fadeOutCoroutine != null)
            {
                this.Info("Fade out is still running, waiting for it to finish");

                yield return null;
            }

            if (Math.Abs(m_fadeObjectRenderer.sharedMaterial.color.a - 0) < 0.01f)
            {
                this.Info("Fade in is already finished, not doing anything");

                yield break;
            }

            //DOTween.KillAll();

            yield return new WaitForSeconds(m_preFadeInDuration);

            InitialiseFadeObject(1);

            // m_fadeObjectRenderer.sharedMaterial.DOFade(0, m_fadeInDuration).SetEase(m_fadeCurve).onComplete += FadeInFinished;
        }


        private void FadeInFinished()
        {
            _fadeInCoroutine = null;
        }


        private IEnumerator FadeOutCR()
        {
            while (_fadeInCoroutine != null)
            {
                this.Info("Fade in is still running, waiting for it to finish");

                yield return null;
            }

            if (Math.Abs(m_fadeObjectRenderer.sharedMaterial.color.a - 1) < 0.01f)
            {
                this.Info("Fade out is already finished, not doing anything");

                yield break;
            }

            // DOTween.KillAll();

            yield return new WaitForSeconds(m_preFadeOutDuration);

            InitialiseFadeObject(0);

            //m_fadeObjectRenderer.sharedMaterial.DOFade(1, m_fadeOutDuration).SetEase(m_fadeCurve).onComplete += FadeOutFinished;
        }


        private void FadeOutFinished()
        {
            _fadeOutCoroutine = null;
        }


        private void InitialiseFadeObject(float initialAlpha)
        {
            m_fadeColor.a = initialAlpha;
            m_fadeMaterial.color = m_fadeColor;

            m_fadeObjectRenderer.sharedMaterial = m_fadeMaterial;
        }


        private void OnDisable()
        {
            EventsSystem.StartFadeOut -= FadeOut;
            EventsSystem.StartFadeIn -= FadeIn;
            EventsSystem.StartFadeRound -= FadeRound;
        }
    }
}