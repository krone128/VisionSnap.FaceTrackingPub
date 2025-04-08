using System;
using VisionSnap.FaceTracking;
using VisionSnap.FaceTracking.Examples;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private ConfidenceSettingItem _minFaceDetectionConfidence;
    [SerializeField] private ConfidenceSettingItem _minFaceTrackingConfidence;
    [SerializeField] private ConfidenceSettingItem _minFacePresenceConfidence;
    [SerializeField] private Toggle _inferenceDelegateToggle;
    [SerializeField] private Toggle _generateFramesToggle;
    [SerializeField] private TMP_Text _inferenceDelegateValueText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    
    [SerializeField] private Button _startRecordButton;
    [SerializeField] private Button _stopRecordButton;
    
    [SerializeField] private FaceBlendshapesDemoController _blendshapesDemoController;
    
    // Start is called before the first frame update
    void Awake()
    {
        _inferenceDelegateValueText.text =  _inferenceDelegateToggle.isOn ? "1: GPU" : "0: CPU";
        
        _minFaceDetectionConfidence.ValueChanged += ConfidenceSettingChanged;
        _minFaceTrackingConfidence.ValueChanged += ConfidenceSettingChanged;
        _minFacePresenceConfidence.ValueChanged += ConfidenceSettingChanged;
        _inferenceDelegateToggle.onValueChanged.AddListener(InferenceDelegateToggleChanged);
        _generateFramesToggle.onValueChanged.AddListener(FacePoseToggleChanged);
        
        _pauseButton.onClick.AddListener(OnPauseButton);
        _resumeButton.onClick.AddListener(OnResumeButton);
        
        _pauseButton.onClick.AddListener(OnStartRecordButton);
        _resumeButton.onClick.AddListener(OnStopRecordButton);
    }

    private void OnStopRecordButton()
    {
        
    }

    private void OnStartRecordButton()
    {
        
    }

    private void FacePoseToggleChanged(bool isOn)
    {
        _blendshapesDemoController.UseFacePose(isOn);
    }

    private void OnResumeButton()
    {
        _blendshapesDemoController.ResumeDetection();
    }

    private void OnPauseButton()
    {
        _blendshapesDemoController.PauseDetection();
    }

    private void InferenceDelegateToggleChanged(bool arg0)
    {
        _inferenceDelegateValueText.text =  _inferenceDelegateToggle.isOn ?  "1: GPU" : "0: CPU";
        
        _blendshapesDemoController.ReinitializeDetector(_minFaceDetectionConfidence.Value,
            _minFaceDetectionConfidence.Value,
            _minFaceDetectionConfidence.Value,
            _inferenceDelegateToggle.isOn ? Constants.GPU_DELEGATE : Constants.CPU_DELEGATE);
    }

    private void ConfidenceSettingChanged(float obj)
    {
        _blendshapesDemoController.ReinitializeDetector(_minFaceDetectionConfidence.Value,
            _minFaceDetectionConfidence.Value,
            _minFaceDetectionConfidence.Value,
            _inferenceDelegateToggle.isOn ? Constants.GPU_DELEGATE : Constants.CPU_DELEGATE);
    }
}
