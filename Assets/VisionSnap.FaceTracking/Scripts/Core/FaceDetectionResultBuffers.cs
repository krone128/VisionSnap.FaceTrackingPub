using System;
using System.Collections;
using System.Collections.Generic;

namespace VisionSnap.FaceTracking
{
    public class FaceDetectionResultBuffers
    {
        const int LANDMARK_MEMBERS_COUNT = 5;

        public int FacesCount;
        
        // public NativeArray<float> Landmarks;
        // public NativeArray<float> Blendshapes;
        // public NativeArray<float> TransformationMatrices;
        
        public IReadOnlyList<float> Landmarks {get; private set;}
        public IReadOnlyList<float> Blendshapes {get; private set;}
        public IReadOnlyList<float> TransformationMatrices {get; private set;}

        public FaceDetectionResultBuffers(
            int facesCount,
            int landmarkArrayLength,
            long landmarkArrayAddress,
            int blendshapeArrayLength,
            long blendshapeArrayAddress,
            int transformMatrixArrayLength,
            long transformMatrixArrayAddress)
        {
            facesCount = Math.Max(facesCount, 1);
            Landmarks = new PointerBuffer<float>(landmarkArrayAddress, landmarkArrayLength);
            Blendshapes = new PointerBuffer<float>(blendshapeArrayAddress, blendshapeArrayLength);
            TransformationMatrices = new PointerBuffer<float>(transformMatrixArrayAddress, transformMatrixArrayLength);
        }
     }

    public unsafe class PointerBuffer<T> : IReadOnlyList<float> where T : unmanaged
    {
        private readonly T* _pointer;
        
        public PointerBuffer(long address, int length)
        {
            Count = length;
            _pointer = (T*)new IntPtr(address).ToPointer();
        }

        public T this[int index] => *(_pointer + index);
        
        public IEnumerator<float> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get; private set; }

        float IReadOnlyList<float>.this[int index] => throw new NotImplementedException();
    }

    public class PointerBufferEnumerator<T> : IEnumerator<T> where T : unmanaged
    {
        private int _currentIndex;
        
        private readonly PointerBuffer<T> _collection;

        public PointerBufferEnumerator(PointerBuffer<T> collection)
        {
            _collection = collection;
        }
         
        public bool MoveNext()
        {
            return ++_currentIndex >= _collection.Count;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public T Current => _collection[_currentIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }
    }
}