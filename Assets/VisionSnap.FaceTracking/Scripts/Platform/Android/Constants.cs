namespace VisionSnap.FaceTracking.Android
{
    public static class Constants
    {
        public const string ANDROID_PLUGIN_PACKAGE_NAME = "com.visionsnap.facetracking";
        public static readonly string ANDROID_PLUGIN_CLASS_NAME = $"{ANDROID_PLUGIN_PACKAGE_NAME}.MLFaceLandmarksPlugin";
        public static readonly string JAVA_PROXY_CLASS_NAME = $"{ANDROID_PLUGIN_PACKAGE_NAME}.ManagedBridge";
    }
}