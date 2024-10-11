using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] public int targetFrameRate = 60;

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFrameRate;
    }

    private void Update()
    {
        if (Application.targetFrameRate != targetFrameRate)
            Application.targetFrameRate = targetFrameRate;
    }
}