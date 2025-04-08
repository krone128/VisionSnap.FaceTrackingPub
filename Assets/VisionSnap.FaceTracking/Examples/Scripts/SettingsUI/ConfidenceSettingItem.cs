using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfidenceSettingItem : MonoBehaviour
{
    public event Action<float> ValueChanged;
    
    [SerializeField] private Button _incButton;
    [SerializeField] private Button _decButton;
    [SerializeField] private TMP_Text _valueText;
    
    [SerializeField] private float _value = 0.5f;
    
    [SerializeField] private float _minValue = 0.2f;
    [SerializeField] private float _maxValue = 1f;
    
    [SerializeField] private float _step = 0.05f;

    public float Value => _value;
    
    void Awake()
    {
        _valueText.text = _value.ToString("F2");
        _incButton.onClick.AddListener(OnIncButton);
        _decButton.onClick.AddListener(OnDecButton);
    }

    private void OnDecButton()
    {
        if(Math.Abs(_value - _maxValue) < 0.0001f) return;
        _value = Mathf.Clamp(_value + _step, _minValue, _maxValue);
        _valueText.text = _value.ToString("F2");
        ValueChanged?.Invoke(_value);
    }

    private void OnIncButton()
    {
        if(Math.Abs(_value - _minValue) < 0.0001f) return;
        _value = Mathf.Clamp(_value - _step, _minValue, _maxValue);
        _valueText.text = _value.ToString("F2");
        ValueChanged?.Invoke(_value);
    }
}
