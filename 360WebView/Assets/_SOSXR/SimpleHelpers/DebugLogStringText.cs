using SOSXR.EnhancedLogger;
using UnityEngine;


public class DebugLogStringText : MonoBehaviour
{
    public void DebugLogThis(string text)
    {
        this.Info(text);
    }
}