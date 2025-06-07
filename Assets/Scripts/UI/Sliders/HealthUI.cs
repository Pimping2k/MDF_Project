using Components;
using UnityEngine;

namespace UI.Sliders
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private GenericSliderComponent<HealthComponent> _sliderComponent;
        [SerializeField] private HealthComponent _entityHealth;

        private void Start()
        {
            _sliderComponent.Initialize(_entityHealth);
        }
    }
}