using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using VisionSnap.FaceTracking.Interface;

namespace VisionSnap.FaceTracking
{
    public class BlendshapeVisualizer : MonoBehaviour
    {
        private const string DEFAULT_BLENDSHAPE_NAME_FORMAT = "{0}";

        [SerializeField] public bool UseFacePose;
        [SerializeField] public Transform FacePoseTransform;
        
        [SerializeField] public bool _mirrorBlendshapesLeftToRight = true;
        [SerializeField] protected SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] protected string _blendshapeNameFormat = DEFAULT_BLENDSHAPE_NAME_FORMAT;
        [SerializeField] protected float _blendshapeScale = 100f;
        [SerializeField] protected int _faceIndex = 0;
        
        protected NativeArray<float> Blendshapes;
        protected NativeArray<float> TransformationMatrices;
        protected IList<int> BlendShapeMapping;

        protected bool IsFrameAvailable;

        private IFaceDetectionCallbacks _plugin;
        
        private List<SignalSmoother> _rotatinSignalSmoothers = new List<SignalSmoother>() {new SignalSmoother(), new SignalSmoother(), new SignalSmoother()};
        
        public IFaceDetectionCallbacks Plugin
        {
            get => _plugin;
            set
            {
                UnsubscribeFromDetector();
                _plugin = value;
                SubscribeToDetector();
            }
            
        }

        protected virtual void Awake()
        {
            SetSkinnedMeshRenderer(_skinnedMeshRenderer, mirrorLeftToRight:_mirrorBlendshapesLeftToRight);
        }

        public virtual void SetSkinnedMeshRenderer(SkinnedMeshRenderer faceMesh, string blendshapeNameFormat = null, bool mirrorLeftToRight = true)
        {
            if (!_skinnedMeshRenderer)
            {
                Debug.LogError("Skinned mesh renderer is null.");
                return;
            }
            
            _skinnedMeshRenderer = faceMesh;
            _mirrorBlendshapesLeftToRight = mirrorLeftToRight;
            
            if (!string.IsNullOrWhiteSpace(blendshapeNameFormat)) 
                _blendshapeNameFormat = blendshapeNameFormat;
            else if (string.IsNullOrWhiteSpace(_blendshapeNameFormat)) 
                _blendshapeNameFormat = DEFAULT_BLENDSHAPE_NAME_FORMAT;

            BlendShapeMapping = BlendshapeMapping.GetMapping(_skinnedMeshRenderer.sharedMesh,
                _mirrorBlendshapesLeftToRight
                    ? BlendshapeMapping.BlendshapesMappingMirrored
                    : BlendshapeMapping.BlendshapesMapping,
                _blendshapeNameFormat
                );
        }

        protected virtual void LateUpdate()
        {
            if (!IsFrameAvailable) return;
            IsFrameAvailable = false;
            SetBlendShapes(Blendshapes);
            UpdateFacePose();
        }

        protected virtual void UpdateFacePose()
        {
            if(!FacePoseTransform)return;

            if (!UseFacePose)
            {
                FacePoseTransform.localRotation = Quaternion.identity;
                return;
            }
            
            var tm = TransformationMatrices;
            var rotation = Quaternion.LookRotation(
                new Vector3(tm[8],tm[9],tm[10]), 
                new Vector3(tm[4],tm[5],tm[6]));
            
            var rotEuler = rotation.eulerAngles;

            if (!_mirrorBlendshapesLeftToRight)
            {
                rotEuler = new Vector3(rotEuler.x, -rotEuler.y, -rotEuler.z);
            }

            rotEuler = new Vector3(
                _rotatinSignalSmoothers[0].MedianSmooth(rotEuler.x),
                _rotatinSignalSmoothers[1].MedianSmooth(rotEuler.y),
                _rotatinSignalSmoothers[2].MedianSmooth(rotEuler.z));
                
            FacePoseTransform.localRotation = Quaternion.Euler(rotEuler);
        }


        private void OnFaceDetectionSetup(FaceDetectionResultBuffers result)
        {
            
        }

        protected virtual void OnFaceDetectionResult(FaceDetectionResultBuffers result)
        {
            Blendshapes = result.Blendshapes.GetSubArray(_faceIndex * Constants.BLENDSHAPE_COUNT, Constants.BLENDSHAPE_COUNT);
            TransformationMatrices = result.TransformationMatrices.GetSubArray(_faceIndex * Constants.MATRIX_COUNT, Constants.MATRIX_COUNT);
            IsFrameAvailable = true;
        }
        
        protected virtual void OnFaceDetectionError(string message) => Debug.LogError(message);
        
        protected virtual void SetBlendShapes(NativeArray<float> blendShapesArray)
        {
            if(!_skinnedMeshRenderer) return;

            var offset = _faceIndex * Constants.BLENDSHAPE_COUNT;
            
            for (var i = 0; i < blendShapesArray.Length; i++)
            {
                if(BlendShapeMapping[i] < 0) continue;
                _skinnedMeshRenderer.SetBlendShapeWeight(BlendShapeMapping[i], blendShapesArray[offset + i] * _blendshapeScale);
            }
        }

        private void SubscribeToDetector()
        {
            if(_plugin == null) return;
            _plugin.FaceDetectionSetup += OnFaceDetectionSetup;
            _plugin.FaceDetectionResult += OnFaceDetectionResult;
            _plugin.FaceDetectionError += OnFaceDetectionError;
        }

        private void UnsubscribeFromDetector()
        {
            if(_plugin == null) return;
            _plugin.FaceDetectionSetup -= OnFaceDetectionSetup;
            _plugin.FaceDetectionResult -= OnFaceDetectionResult;
            _plugin.FaceDetectionError -= OnFaceDetectionError;
        }
    }
}