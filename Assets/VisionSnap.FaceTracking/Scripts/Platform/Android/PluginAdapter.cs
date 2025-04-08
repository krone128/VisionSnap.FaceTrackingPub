using System;
using JetBrains.Annotations;
using UnityEngine;
using VisionSnap.FaceTracking.Interface;

namespace VisionSnap.FaceTracking.Android
{
    public class PluginAdapter : AndroidJavaProxy, IFaceDetectionPlugin
    {
        public PluginAdapter() : base(Constants.JAVA_PROXY_CLASS_NAME) { }

        public event Action<FaceDetectionResultBuffers> FaceDetectionSetup;
        public event Action<FaceDetectionResultBuffers> FaceDetectionResult;
        public event Action<string> FaceDetectionError;

        private AndroidJavaObject _pluginInstance;

        private FaceDetectionResultBuffers _cachedResult;

        private int _fpsCounter;
        private int _fpsAverageRangeCounter;
        private int _fpsAverageRange = 10;
        
        public bool IsInitialized { get; private set; }
        public int DetectionFPS { get; private set; }

        public void Initialize(FaceDetectionConfig faceDetectionConfig)
        {
            using var pluginClass = new AndroidJavaClass(Constants.ANDROID_PLUGIN_CLASS_NAME);

            _pluginInstance ??= pluginClass
                .GetStatic<AndroidJavaObject>("Companion")
                .Call<AndroidJavaObject>("getInstance", this);

            if (_pluginInstance == null)
            {
                Debug.LogError("PluginAdapter.Android: Failed to initialize");
                return;
            }
            
            faceDetectionConfig ??= new FaceDetectionConfig();
        
            _pluginInstance?.Call("setupDetector",
                faceDetectionConfig.MaxFacesTrackedNumber,
                faceDetectionConfig.OutputLandmarks,
                faceDetectionConfig.OutputBlendshapes,
                faceDetectionConfig.OutputTransformationMatrices,
                faceDetectionConfig.MinFaceDetectionConfidence, 
                faceDetectionConfig.MinFaceTrackingConfidence,
                faceDetectionConfig.MinFacePresenceConfidence, 
                faceDetectionConfig.InferenceDelegate,
                faceDetectionConfig.LimitDetectionToFramerate);

            IsInitialized = true;
        }
        
        public void StartBlendshapeRecord() => _pluginInstance?.Call("startRecording");

        public void StopBlendshapeRecord() => _pluginInstance?.Call("stopRecording");

        public void Start() => _pluginInstance?.Call("resume");

        public void Stop() => _pluginInstance?.Call("pause");
        
        public void Dispose()
        {
            if (_pluginInstance == null) return;
            _pluginInstance.Call("dispose");
            _pluginInstance.Dispose();
            _pluginInstance = null;
            IsInitialized = false;
        }
        
        private void CountFps(long inferenceTime)
        {
            _fpsCounter += (int) inferenceTime;
            _fpsAverageRangeCounter++;

            if (_fpsAverageRangeCounter < _fpsAverageRange) return;
            DetectionFPS = 1000 / (_fpsCounter / _fpsAverageRange);
            _fpsAverageRangeCounter = 0;
            _fpsCounter = 0;
        }
        
        [UsedImplicitly]
        void faceDetectionSetup(
            int facesCount,
            int landmarksArrayLength,
            long landmarksAddress,
            int blendshapesArrayLength,
            long blendshapesAddress,
            int transformationMatricesArrayLength,
            long transformationMatricesAddress)
        {
            _cachedResult = new FaceDetectionResultBuffers(
                facesCount, 
                landmarksArrayLength, 
                landmarksAddress, 
                blendshapesArrayLength,
                blendshapesAddress,
                transformationMatricesArrayLength,
                transformationMatricesAddress);
            
            FaceDetectionSetup?.Invoke(_cachedResult);
        }
        
        [UsedImplicitly]
        void faceDetectionResult(
            int facesCount,
            long inferenceTime)
        {
            _cachedResult.FacesCount = facesCount;
            CountFps(inferenceTime);
            FaceDetectionResult?.Invoke(_cachedResult);
        }

        [UsedImplicitly]
        void faceDetectionError(string message) => FaceDetectionError?.Invoke(message);
    }
}