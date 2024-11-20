using System;


namespace _mrstruijk.Localisation
{
    [Serializable]
    public struct StringLocalised
    {
        public string Key;


        public StringLocalised(string key)
        {
            Key = key;
        }


        public string Value => StringLocalisationSystem.GetLocalisedValue(Key);


        public static implicit operator StringLocalised(string key)
        {
            return new StringLocalised(key);
        }
    }
}