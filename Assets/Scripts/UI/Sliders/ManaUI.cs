using System;
using Components;
using UnityEngine;

namespace UI.Sliders
{
    public class ManaUI : MonoBehaviour
    {
        [SerializeField] private GenericSliderComponent<ManaComponent> _sliderComponent;
        [SerializeField] private ManaComponent _manaComponent;

        private void Start()
        {
            _sliderComponent.Initialize(_manaComponent);
        }
    }
}