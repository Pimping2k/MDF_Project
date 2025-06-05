using System;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class GenericSliderComponent<T> : MonoBehaviour where T : IScalable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _stat;
        private T stat;

        public void Initialize(T scalable)
        {
            stat = scalable;
            _slider.maxValue = stat.MaxValue;
            _slider.value = stat.CurrentValue;
            stat.OnValueChanged += OnValueChanged;
            _stat.text = $"{stat.CurrentValue}/{stat.MaxValue}";
        }

        private void OnValueChanged(float newValue)
        {
            _slider.value = newValue;
            _stat.text = $"{stat.CurrentValue}/{stat.MaxValue}";
        }

        private void OnDestroy()
        {
            if (stat != null)
                stat.OnValueChanged -= OnValueChanged;
        }
    }
}