using UnityEngine;


namespace mrstruijk
{
    public class DecimalsAttribute : PropertyAttribute
    {
        public readonly int Decimals;


        public DecimalsAttribute(int decimals)
        {
            Decimals = decimals;
        }
    }
}