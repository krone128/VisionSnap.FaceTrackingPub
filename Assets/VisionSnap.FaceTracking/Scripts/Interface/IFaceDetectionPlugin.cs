using System;

namespace VisionSnap.FaceTracking.Interface
{
    public interface IFaceDetectionPlugin : IFaceDetectionCallbacks, IFaceDetectionPluginControls
    {
        
    }

    public interface IFaceDetectionPluginControls
    {
        public bool IsInitialized { get; }

        public int DetectionFPS { get; }

        void Initialize(FaceDetectionConfig config);
        void Start();
        void Stop();
        void Dispose();
    }

    public interface IFaceDetectionCallbacks
    {
        event Action<FaceDetectionResultBuffers> FaceDetectionSetup; 
        event Action<FaceDetectionResultBuffers> FaceDetectionResult; 
        event Action<string> FaceDetectionError;
    }
}