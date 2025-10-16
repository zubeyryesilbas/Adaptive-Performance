using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class AdaptivePerformanceManager : MonoBehaviour
{
    private IAdaptivePerformance _ap;
    private float _checkTimer;
    private float _checkInterval = 2f; // Her 2 saniyede bir kontrol et
    private int _currentQuality = 2; // 0 = Low, 1 = Medium, 2 = High

    private void Start()
    {
        Application.targetFrameRate = 60; // Başlangıç FPS hedefi
        _ap = Holder.Instance;

        if (_ap == null)
        {
            Debug.LogWarning("Adaptive Performance desteklenmiyor. Varsayılan kalite kullanılacak.");
            QualitySettings.SetQualityLevel(_currentQuality, true);
            return;
        }

        Debug.Log("Adaptive Performance aktif!");
    }

    private void Update()
    {
        if (_ap == null) return;

        _checkTimer += Time.deltaTime;
        if (_checkTimer < _checkInterval) return;
        _checkTimer = 0;

        var perf = _ap.PerformanceStatus;
        var thermal = _ap.ThermalStatus;

        // 🔥 Cihaz aşırı ısınıyorsa kaliteyi düşür
        if (thermal.ThermalMetrics.WarningLevel == WarningLevel.ThrottlingImminent && _currentQuality > 0)
        {
            AdjustQuality(-1);
            return;
        }

        // ⚙️ GPU veya CPU darboğazı varsa kaliteyi düşür
        if (perf.PerformanceMetrics.PerformanceBottleneck != PerformanceBottleneck. TargetFrameRate && _currentQuality > 0)
        {
            AdjustQuality(-1);
            return;
        }

        // 🌡️ Cihaz serin, darboğaz yok → kaliteyi artır
        if (thermal.ThermalMetrics.WarningLevel== WarningLevel.NoWarning &&
            perf.PerformanceMetrics.PerformanceBottleneck == PerformanceBottleneck.TargetFrameRate &&
            _currentQuality < 2)
        {
            AdjustQuality(+1);
        }
        
    }

    private void AdjustQuality(int delta)
    {
        _currentQuality = Mathf.Clamp(_currentQuality + delta, 0, 2);
        QualitySettings.SetQualityLevel(_currentQuality, true);

        switch (_currentQuality)
        {
            case 0: // Low
                Application.targetFrameRate = 30;
                QualitySettings.lodBias = 0.5f;
                QualitySettings.shadowDistance = 20;
                break;
            case 1: // Medium
                Application.targetFrameRate = 30;
                QualitySettings.lodBias = 0.75f;
                QualitySettings.shadowDistance = 40;
                break;
            case 2: // High
                Application.targetFrameRate = 30;
                QualitySettings.lodBias = 1f;
                QualitySettings.shadowDistance = 80;
                break;
        }

        Debug.Log($"[Adaptive] Kalite: {_currentQuality} | FPS: {Application.targetFrameRate}");
    }
    
}
