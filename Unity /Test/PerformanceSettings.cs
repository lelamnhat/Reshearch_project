using UnityEngine;

public class PerformanceSettings : MonoBehaviour
{
    [Header("FPS")]
    [Tooltip("60 là an toàn. Nếu máy 120Hz và vẫn rung, thử 120.")]
    [SerializeField] private int targetFps = 60;

    private void Awake()
    {
        // Tránh Unity bị giằng co vs VSync trên mobile
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;

        // Gợi ý: nếu máy bạn 120Hz và vẫn rung, đổi targetFps = 120
        DevLog.Info($"[PerformanceSettings] targetFps = {targetFps}");
    }
}