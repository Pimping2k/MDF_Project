using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class ManaComponent : MonoBehaviour, IScalable
    {
        public static ManaComponent Instance;

        [SerializeField] private float _maxMana;
        
        private float _currentMana;
        
        public float CurrentValue => _currentMana;
        public float MaxValue => _maxMana;
        public event Action<float> OnValueChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            _currentMana = _maxMana;
            OnValueChanged?.Invoke(_currentMana);
        }

        public void DecreaseMana(float value)
        {
            _currentMana -= Mathf.Clamp(value, 0, _maxMana);
            OnValueChanged?.Invoke(_currentMana);
        }

        public void IncreaseMana(float value)
        {
            _currentMana += Mathf.Clamp(value, 0, _maxMana);
            OnValueChanged?.Invoke(_currentMana);
        }
    }
}