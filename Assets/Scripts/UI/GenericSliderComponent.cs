using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GenericSliderComponent<T> : MonoBehaviour where T : IScalable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _statUI;
        private T stat;

        public void Initialize(T scalable)
        {
            stat = scalable;
            _slider.maxValue = stat.MaxValue;
            _slider.value = stat.CurrentValue;
            stat.OnValueChanged += OnValueChanged;
            _statUI.text = $"{stat.CurrentValue}/{stat.MaxValue}";
        }

        private void OnValueChanged(float newValue)
        {
            _slider.value = newValue;
            _statUI.text = $"{stat.CurrentValue}/{stat.MaxValue}";
        }

        private void OnDestroy()
        {
            if (stat != null)
                stat.OnValueChanged -= OnValueChanged;
        }
    }
}