namespace VisionSnap.FaceTracking
{
    public class FaceDetectionConfig
    {
        public float MinFaceDetectionConfidence = 0.75f;
        public float MinFaceTrackingConfidence = 0.75f;
        public float MinFacePresenceConfidence = 0.75f;
        public int InferenceDelegate = Constants.GPU_DELEGATE;
        public int MaxFacesTrackedNumber = 1;
        public bool OutputBlendshapes = true;
        public bool OutputLandmarks;
        public bool OutputTransformationMatrices = true;
        public int LimitDetectionToFramerate = 25;
    }
}