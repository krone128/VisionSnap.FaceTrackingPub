using System;
using UnityEngine;
using VisionSnap.FaceTracking.Interface;

namespace VisionSnap.FaceTracking.Examples
{
    public class FaceBlendshapesDemoController : MonoBehaviour
    {
        [SerializeField] private BlendshapeVisualizer _blendshapeVisualizer;

        [SerializeField] private bool _initializeOnAwake;
        
        private IFaceDetectionPlugin _plugin;
        
        public void Awake()
        {
            _plugin = PluginWrapper.GetInstance();
            _blendshapeVisualizer.Plugin = _plugin;
            
            _plugin.FaceDetectionSetup += OnFaceDetectionSetup;
            _plugin.Initialize(new FaceDetectionConfig()
            {
                OutputTransformationMatrices = true
            });
        }

        private void OnFaceDetectionSetup(FaceDetectionResultBuffers result)
        {
            if (_initializeOnAwake) ResumeDetection();
        }

        public virtual int DetectionFPS => _plugin?.DetectionFPS ?? default;
        
        public virtual void PauseDetection() => _plugin?.Stop();

        public virtual void ResumeDetection() => _plugin?.Start();

        public virtual void DisposeDetection() => _plugin?.Dispose();

        public void ReinitializeDetector(float minDet, float minTrack, float minPres, int inferenceDelegate)
        {
            _plugin.Initialize(new FaceDetectionConfig
            {
                MinFaceDetectionConfidence = minDet,
                MinFacePresenceConfidence = minPres,
                MinFaceTrackingConfidence = minTrack,
                InferenceDelegate = inferenceDelegate,
                OutputTransformationMatrices = true
            });
        }

        private void OnDestroy()
        {
            DisposeDetection();
        }

        public void SetGenerateFrames(bool isOn)
        {
            
        }

        public void UseFacePose(bool isOn)
        {
            _blendshapeVisualizer.UseFacePose = isOn;
        }
    }
}