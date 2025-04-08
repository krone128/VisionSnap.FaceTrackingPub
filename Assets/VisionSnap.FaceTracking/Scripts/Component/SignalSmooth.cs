using System;
using System.Collections.Generic;
using UnityEngine;

namespace VisionSnap.FaceTracking
{

    public class SignalSmoother
    {
        private List<float> previousValues;
        private int windowSize;
        private List<float> _sortedValues = new List<float>();

        public SignalSmoother(int windowSize = 8)
        {
            if (windowSize <= 0)
            {
                throw new ArgumentException("Window size must be greater than zero.");
            }

            this.windowSize = windowSize;
            this.previousValues = new List<float>();
        }

        public float Smooth(float newValue)
        {
            if (previousValues.Count > 0)
            {
                var last = previousValues[^1];
                if (Mathf.Abs(newValue - last) > 180)
                {
                    newValue += last < newValue 
                        ? - 360f
                        : 360f;
                }
            }

            
            previousValues.Add(newValue);

            if (previousValues.Count <= windowSize)
            {
                // Not enough values to smooth, return the average of what we have.
                float sum = 0;
                foreach (float value in previousValues)
                {
                    sum += value;
                }

                return sum / previousValues.Count;
            }
            else
            {
                // Remove the oldest value if we exceed the window size.
                previousValues.RemoveAt(0);

                // Calculate the average of the current window.
                float sum = 0;
                foreach (float value in previousValues)
                {
                    sum += value;
                }

                return sum / windowSize;
            }
        }

        // A more advanced smoothing method using a weighted average.
        public float WeightedSmooth(float newValue, float alpha = 0.5f)
        {
            if (alpha < 0 || alpha > 1)
            {
                throw new ArgumentException("Alpha must be between 0 and 1.");
            }

            if (previousValues.Count == 0)
            {
                previousValues.Add(newValue);
                return newValue;
            }
            
            if (previousValues.Count > 0)
            {
                var last = previousValues[^1];
                if (Mathf.Abs(newValue - last) > 180)
                {
                    newValue += last < newValue 
                        ? - 360f
                        : 360f;
                }
            }

            if (previousValues.Count > 0 && Mathf.Abs(newValue - previousValues[^1]) > 180)
            {
                newValue = (newValue - 360f) % 360f;
            }
            
            float smoothedValue = alpha * newValue + (1f - alpha) * previousValues[previousValues.Count - 1];
            previousValues.Add(smoothedValue);
            if (previousValues.Count > windowSize)
            {
                previousValues.RemoveAt(0);
            }

            return smoothedValue;
        }

        // Another method to smooth using a simple moving median.
        public float MedianSmooth(float newValue, bool loop180 = false)
        {
            if (previousValues.Count > 0)
            {
                var last = previousValues[^1];
                if (Mathf.Abs(newValue - last) > 180)
                {
                    newValue += last < newValue 
                        ? - 360f
                        : 360f;
                }
            }

            previousValues.Add(newValue);

            if (previousValues.Count > windowSize)
            {
                // Remove the oldest value if we exceed the window size.
                previousValues.RemoveAt(0);
            }

            _sortedValues.Clear();
            _sortedValues.AddRange(previousValues);
            _sortedValues.Sort();

            if (_sortedValues.Count % 2 == 0)
            {
                // Even number of values, return the average of the two middle values.
                return (_sortedValues[_sortedValues.Count / 2 - 1] + _sortedValues[_sortedValues.Count / 2]) / 2.0f;
            }
            else
            {
                // Odd number of values, return the middle value.
                return _sortedValues[_sortedValues.Count / 2];
            }
        }
    }
}