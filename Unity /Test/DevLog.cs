using UnityEngine;

public static class DevLog
{
    // Chỉ log trong Editor, build mobile không log => nhẹ hơn.
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Info(string msg) => Debug.Log(msg);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Warn(string msg) => Debug.LogWarning(msg);

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Error(string msg) => Debug.LogError(msg);
}