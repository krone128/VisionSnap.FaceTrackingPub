using System;
using VisionSnap.FaceTracking.Interface;

namespace VisionSnap.FaceTracking
{
    public class PluginWrapper : IFaceDetectionPlugin
    {
        public event Action<FaceDetectionResultBuffers> FaceDetectionSetup;
        public event Action<FaceDetectionResultBuffers> FaceDetectionResult;
        public event Action<string> FaceDetectionError;
        
        public static IFaceDetectionPlugin GetInstance() => new PluginWrapper();
        
        public bool IsInitialized => _pluginInstance?.IsInitialized ?? false;
        
        public int DetectionFPS => _pluginInstance?.DetectionFPS ?? 0;

        private IFaceDetectionPlugin _pluginInstance;

        private PluginWrapper()
        {
#if !UNITY_EDITOR && UNITY_ANDROID
            _pluginInstance = new Android.PluginAdapter();
// #elif !UNITY_EDITOR && UNITY_IOS
//             _pluginInstance = new IOS.PluginAdapter();
#else
            throw new NotSupportedException("Not supported on this platform.");
#endif

            _pluginInstance.FaceDetectionResult += OnBlendshapeResult;
            _pluginInstance.FaceDetectionSetup += OnFaceDetectionSetup;
            _pluginInstance.FaceDetectionError += OnFaceDetectionError;
        }
        
        private void OnBlendshapeResult(FaceDetectionResultBuffers result)
        {
            // duplicate eye look blendshapes from left to right to prevent eyes
            // looking opposite directions when making lips O or U
            // 11 - eyeLookDownLe
            // 12 - eyeLookDownRight
            // 13 - eyeLookInLe
            // 14 - eyeLookInRight
            // 15 - eyeLookOutLe
            // 16 - eyeLookOutRight
            // 17 - eyeLookUpLe
            // 18 - eyeLookUpRight

            for (var i = 0; i < result.FacesCount; i++)
            {
                result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 12] = result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 11];
                result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 14] = result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 15];
                result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 16] = result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 13];
                result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 18] = result.Blendshapes[Constants.BLENDSHAPE_COUNT * i + 17];
            }

            FaceDetectionResult?.Invoke(result);
        }

        public void Initialize(FaceDetectionConfig config)
        {
            _pluginInstance.Initialize(config);
        }

        private void OnFaceDetectionSetup(FaceDetectionResultBuffers result)
        {
            FaceDetectionSetup?.Invoke(result);
        }

        private void OnFaceDetectionError(string message) => FaceDetectionError?.Invoke(message);

        public void Start() => _pluginInstance?.Start();

        public void Stop() => _pluginInstance?.Stop();

        public void Dispose()
        {
            if(_pluginInstance == null) return;
            _pluginInstance.FaceDetectionResult -= OnBlendshapeResult;
            _pluginInstance.FaceDetectionSetup -= OnFaceDetectionSetup;
            _pluginInstance.FaceDetectionError -= OnFaceDetectionError;
            _pluginInstance.Dispose();
        }
    }
}