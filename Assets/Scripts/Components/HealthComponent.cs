using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class HealthComponent : MonoBehaviour, IScalable
    {
        [SerializeField] private bool isTaunted;
        [SerializeField] private float _maxHealth;
        
        private float _currentHealth;

        public float CurrentValue => _currentHealth;
        public float MaxValue => _maxHealth;
        public bool IsTaunted => isTaunted;

        public event Action<float> OnValueChanged;
        public event Action OnDeath;

        private void Awake()
        {
            _currentHealth = _maxHealth;
            OnValueChanged?.Invoke(_currentHealth);
        }

        public float IncreaseHealth(float value)
        {
            _currentHealth += value;
            return _currentHealth;
        }

        public float DecreaseHealth(float value)
        {
            _currentHealth -= value;
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
            return _currentHealth;
        }

        public void EnableTaunt(bool state)
        {
            isTaunted = state;
        }

        public void FullHeal() => _currentHealth = _maxHealth;
    }
}
