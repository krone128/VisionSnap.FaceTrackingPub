using TMPro;
using UnityEngine;

namespace VisionSnap.FaceTracking.Examples.SettingsUI
{
    public class FpsCounter : MonoBehaviour
    {
        float FpsUpdateInterval = 1;
        
        [SerializeField] private FaceBlendshapesDemoController _blendshapesDemoController;
        [SerializeField] private TMP_Text _detectionFps;

        private float _fpsUpdateIntervalCounter;

        private void Update()
        {
            _fpsUpdateIntervalCounter += Time.deltaTime;

            if (_fpsUpdateIntervalCounter >= FpsUpdateInterval)
            {
                _fpsUpdateIntervalCounter -= FpsUpdateInterval;
                _detectionFps.text = _blendshapesDemoController.DetectionFPS.ToString();
            }
        }
    }
}