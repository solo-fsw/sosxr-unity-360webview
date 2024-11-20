using System;
using System.Collections;
using System.Collections.Generic;
using mrstruijk.Events;
using UnityEngine;


namespace _mrstruijk.Localisation
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioLocaliser : MonoBehaviour
    {
        public List<AudioClip> DutchClips;
        public List<AudioClip> EnglishClips; // Add extra languages here

        private List<AudioClip> _clips;

        private Coroutine _playClip;
        private Coroutine _playMultipleClips;

        private AudioSource _source;


        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }


        private void Start()
        {
            ChangeLanguage();
        }


        private void OnEnable()
        {
            EventsSystem.LanguageHasBeenChanged += ChangeLanguage;
        }


        private void ChangeLanguage()
        {
            StartCoroutine(ChangeLanguageCR());
        }


        private IEnumerator ChangeLanguageCR()
        {
            while (_playMultipleClips != null)
            {
                yield return null;
            }

            switch (LanguageSelector.Language)
            {
                case Languages.NL:
                    _clips = DutchClips;

                    break;

                case Languages.EN:
                    _clips = EnglishClips;

                    break; // Add extra languages here

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public void PlayMultipleClips()
        {
            _playMultipleClips ??= StartCoroutine(PlayMultipleClipsCR());
        }


        private IEnumerator PlayMultipleClipsCR()
        {
            foreach (var clip in _clips)
            {
                _playClip = StartCoroutine(PlayClipCR(0.5f, 0.5f, clip, _source));

                yield return _playClip;
            }

            _playMultipleClips = null;
        }


        private static IEnumerator PlayClipCR(float preDelay, float postDelay, AudioClip audio, AudioSource source)
        {
            yield return new WaitForSeconds(preDelay);
            source.clip = audio;
            source.Play();

            yield return new WaitForSeconds(audio.length + postDelay);
        }


        private void OnDisable()
        {
            EventsSystem.LanguageHasBeenChanged -= ChangeLanguage;
            StopAllCoroutines();
        }
    }
}