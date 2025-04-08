using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace VisionSnap.FaceTracking
{
    public class FaceDetectionResultBuffers
    {
        const int LANDMARK_MEMBERS_COUNT = 5;

        public int FacesCount;
        
        public NativeArray<float> Landmarks;
        public NativeArray<float> Blendshapes;
        public NativeArray<float> TransformationMatrices;

        public FaceDetectionResultBuffers(
            int facesCount,
            int landmarkArrayLength,
            long landmarkArrayAddress,
            int blendshapeArrayLength,
            long blendshapeArrayAddress,
            int transformMatrixArrayLength,
            long transformMatrixArrayAddress)
        {
            FacesCount = facesCount;

            if (FacesCount == 0) return;

            if (landmarkArrayLength > 0)
            {
                unsafe
                {
                    Landmarks = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(
                        new IntPtr(landmarkArrayAddress).ToPointer(), landmarkArrayLength, Allocator.None);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref Landmarks,
                        AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
#endif
                }
            }

            if (blendshapeArrayLength > 0)
            {
                unsafe
                {
                    Blendshapes = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(
                        new IntPtr(blendshapeArrayAddress).ToPointer(), blendshapeArrayLength, Allocator.None);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref Blendshapes,
                        AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
#endif
                }
            }

            if (transformMatrixArrayLength > 0)
            {
                unsafe
                {
                    TransformationMatrices = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<float>(
                        new IntPtr(transformMatrixArrayAddress).ToPointer(), transformMatrixArrayLength,
                        Allocator.None);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                    NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref TransformationMatrices,
                        AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
#endif
                }
            }
        }
    }
}