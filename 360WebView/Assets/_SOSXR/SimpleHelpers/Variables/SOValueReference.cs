using UnityEngine;


namespace GPInteraction._Scripts
{
    /// <summary>
    ///     Based on Ryan Hipple's 2017 talk: https://www.youtube.com/watch?v=raQ3iHhE_Kk&t=0s
    ///     Refined with Odin: https://www.youtube.com/watch?v=RZJWwn40T8E&t=11s
    ///     By Maarten R. Struijk Wilbrink
    /// </summary>
    public abstract class SOValueReference<TValue, TAsset> where TAsset : SOValueAsset<TValue>
    {
        [SerializeField]
        protected TAsset _assetReference;

        [SerializeField] protected TValue _value;


        [SerializeField]
        protected TAsset assetReference;

        [Tooltip("Directly edit the referenced Scriptable Object in Project folder")]
        [SerializeField]
        protected bool editSOAsset = false;
        [SerializeField]
        protected bool useValue = false;


        public TValue Value
        {
            get
            {
                if (assetReference == null || useValue)
                {
                    return _value;
                }

                return assetReference.Value;
            }
            set
            {
                if (assetReference == null || useValue)
                {
                    _value = value;
                }
                else
                {
                    assetReference.Value = value;
                }
            }
        }


        protected void UpdateAsset()
        {
            _assetReference = assetReference;
        }


        /// <summary>
        /// Implicit cast from ValueReference to TValue or value held by valueReference.
        /// This means we don't need to use the .value accessor, since valueReference will implicitly cast to value
        /// </summary>
        /// <returns></returns>
        // public static implicit operator TValue(SOValueReference<TValue, TAsset> valueRef)
        // {
        //  	return valueRef.value; // this doesn't work
        // }
    }
}