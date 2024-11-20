using SOSXR.EnhancedLogger;
using UnityEngine;


namespace GPInteraction._Scripts
{
    /// <summary>
    ///     This turns a value in a Project-wide asset, instead of a value localised on an instance of a class.
    ///     Based on Ryan Hipple's 2017 talk: https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=0s
    ///     Refined with Odin: https://www.youtube.com/watch?v=RZJWwn40T8E&t=11s
    ///     By Maarten R. Struijk Wilbrink
    /// </summary>
    public abstract class SOValueAsset<T> : ScriptableObject
    {
        protected T storedValue;

        public T Value
        {
            get => storedValue;
            set
            {
                this.Info("Old value was", storedValue, "Setting now to", value);
                storedValue = value;
            }
        }


        public void InitializeValue(T value = default)
        {
            this.Info("Initializing value to", value);
            Value = value;
        }


        public void ResetValue()
        {
            this.Info("Resetting value to", default);
            Value = default;
        }
    }
}