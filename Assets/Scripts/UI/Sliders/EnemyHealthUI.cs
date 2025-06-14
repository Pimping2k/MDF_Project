using System;
using Components;
using CoreMechanic;
using UnityEngine;

namespace UI.Sliders
{
    public class EnemyHealthUI : MonoBehaviour
    {
        [SerializeField] private GenericSliderComponent<HealthComponent> _sliderComponent;
        [SerializeField] private HealthComponent _entityHealth;

        private void Start()
        {
            _entityHealth = EnemyManager.Instance.HealthComponent;
            _sliderComponent.Initialize(_entityHealth);
        }
    }
}