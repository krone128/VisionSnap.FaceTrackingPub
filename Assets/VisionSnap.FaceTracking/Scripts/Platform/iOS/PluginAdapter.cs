using System;
using System.Collections.Generic;
using AOT;
using VisionSnap.FaceTracking.Interface;

namespace VisionSnap.FaceTracking.iOS
{
    public class PluginAdapter : IFaceDetectionPlugin
    {
        private delegate void OnBlendshapeSetupDelegate(int faceCount, float[] blendshapes, float inferenceTime);
        private delegate void OnBlendshapeResultDelegate(int faceCount, float[] blendshapes, float inferenceTime);

        private delegate void OnBlendshapeErrorDelegate(string errorMessage);

        public event Action<FaceDetectionResultBuffers> FaceDetectionSetup;
        public event Action<FaceDetectionResultBuffers> FaceDetectionResult;
        public event Action<string> FaceDetectionError;

        private static PluginAdapter Instance { get; set; }

        [MonoPInvokeCallback(typeof(OnBlendshapeSetupDelegate))]
        private static void OnFaceDetectionSetup(string message) => Instance?.FaceDetectionSetup?.Invoke(null);
        
        [MonoPInvokeCallback(typeof(OnBlendshapeErrorDelegate))]
        private static void OnFaceDetectionError(string message) => Instance?.FaceDetectionError?.Invoke(message);

        [MonoPInvokeCallback(typeof(OnBlendshapeResultDelegate))]
        private static void OnBlendshapeResult(int facesCount, float[] blendshapes, long inferenceTime) =>
            Instance?.FaceDetectionResult?.Invoke(null);

        public bool IsInitialized { get; private set; }
        public int DetectionFPS { get; private set; }

        public void Initialize(FaceDetectionConfig config)
        {
            Instance = new PluginAdapter();
            IsInitialized = true;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void StartBlendshapeRecord()
        {
            throw new NotImplementedException();
        }

        public void StopBlendshapeRecord()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<float> GenerateFrame()
        {
            throw new NotImplementedException();
        }
    }
}
    