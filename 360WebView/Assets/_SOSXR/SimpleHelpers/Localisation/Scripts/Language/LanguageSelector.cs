using mrstruijk.Events;
using UnityEngine;


namespace _mrstruijk.Localisation
{
    public static class LanguageSelector
    {
        public static Languages Language
        {
            get => _language;
            set
            {
                _language = value;
                InvokeLanguageChangedAction();
            }
        }

        private static Languages _language = Languages.NL;


        private static void InvokeLanguageChangedAction()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            EventsSystem.LanguageHasBeenChanged?.Invoke();
        }
    }


    public enum Languages
    {
        NL,
        EN // Add other languages here
    }
}