using System;
using System.Collections;
using mrstruijk.Events;
using SOSXR.EnhancedLogger;
using UnityEngine;
using UnityEngine.UI;


//// using DG.Tweening;


namespace mrstruijk.SimpleHelpers
{
    public class ImageFader : MonoBehaviour
    {
        [SerializeField] private Image m_fadeImage;
        [SerializeField] private bool m_fadeOnStart = true;
        [SerializeField] private Color m_fadeColor = Color.black;
        [SerializeField] [Range(0f, 10f)] private float m_preFadeInDuration = 2f;
        [SerializeField] [Range(0f, 10f)] private float m_fadeInDuration = 5f;
        [SerializeField] [Range(0f, 10f)] private float m_preFadeOutDuration = 0f;
        [SerializeField] [Range(0f, 10f)] private float m_fadeOutDuration = 5f;
        [SerializeField] private AnimationCurve m_fadeCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        private float _backupTimer;

        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;


        private void Start()
        {
            if (!m_fadeOnStart)
            {
                return;
            }

            FadeIn();
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


        public void FadeIn()
        {
            if (_fadeInCoroutine != null)
            {
                return;
            }

            _fadeInCoroutine = StartCoroutine(FadeInCR());
        }


        public void FadeOut()
        {
            if (_fadeOutCoroutine != null)
            {
                return;
            }

            m_fadeImage.enabled = true;

            _fadeOutCoroutine = StartCoroutine(FadeOutCR());
        }


        private IEnumerator FadeInCR()
        {
            _backupTimer = 0f;

            while (_fadeOutCoroutine != null)
            {
                this.Info("Fade out is still running, waiting for it to finish");

                _backupTimer += Time.deltaTime;

                if (_backupTimer > m_fadeOutDuration)
                {
                    this.Error("Backup timer exceeded the fadeOut duration, stopping the fadeOut coroutine");
                    _fadeOutCoroutine = null;
                }

                yield return null;
            }

            if (Math.Abs(m_fadeImage.color.a - 0) < 0.01f)
            {
                this.Info("Fade in is already finished, not doing anything");

                yield break;
            }

            //DOTween.KillAll();

            yield return new WaitForSeconds(m_preFadeInDuration);

            // m_fadeImage.DOFade(0, m_fadeInDuration).SetEase(m_fadeCurve).onComplete += FadeInFinished;
        }


        private void FadeInFinished()
        {
            _fadeInCoroutine = null;

            m_fadeImage.enabled = false;
        }


        private IEnumerator FadeOutCR()
        {
            _backupTimer = 0f;

            while (_fadeInCoroutine != null)
            {
                this.Info("Fade in is still running, waiting for it to finish");

                _backupTimer += Time.deltaTime;

                if (_backupTimer > m_fadeInDuration)
                {
                    this.Error("Backup timer exceeded the fadeIn duration, stopping the fadeIn coroutine");
                    _fadeInCoroutine = null;
                }

                yield return null;
            }

            if (Math.Abs(m_fadeImage.color.a - 1) < 0.01f)
            {
                this.Info("Fade out is already finished, not doing anything");

                yield break;
            }

            //DOTween.KillAll();

            yield return new WaitForSeconds(m_preFadeOutDuration);


//            m_fadeImage.DOFade(1, m_fadeOutDuration).SetEase(m_fadeCurve).onComplete += FadeOutFinished;
        }


        private void FadeOutFinished()
        {
            _fadeOutCoroutine = null;
        }


        private void OnDisable()
        {
            EventsSystem.StartFadeOut -= FadeOut;
            EventsSystem.StartFadeIn -= FadeIn;
            EventsSystem.StartFadeRound -= FadeRound;
        }
    }
}