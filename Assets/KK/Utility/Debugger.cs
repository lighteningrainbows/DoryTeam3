using System.Diagnostics;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    [Conditional("UNITY_EDITOR")]
    static public void Log(object text)
    {
        UnityEngine.Debug.Log(text);
    }

    [Conditional("UNITY_EDITOR")]
    static public void LogError(object text)
    {
        UnityEngine.Debug.LogError(text);
    }

    [Conditional("UNITY_EDITOR")]
    static public void LogWarning(object text)
    {
        UnityEngine.Debug.LogWarning(text);
    }
}
