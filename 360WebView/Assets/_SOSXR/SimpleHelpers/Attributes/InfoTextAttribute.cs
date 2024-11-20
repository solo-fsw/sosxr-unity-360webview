using System;
using System.Diagnostics;
using UnityEngine;


namespace mrstruijk.Tools
{
    /// <summary>
    ///     Use this over a dummy variable to display some text
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class InfoTextAttribute : PropertyAttribute
    {
        public readonly string InfoText;


        public InfoTextAttribute(string infoText)
        {
            InfoText = infoText;
        }
    }
}