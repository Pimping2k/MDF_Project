using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private bool isTaunted;
    
    public bool IsTaunted => isTaunted;
    public float Health => health;
    public float MaxHealth => maxHealth;

    public event Action OnDeath;
    
    private void Awake()
    {
        health = maxHealth;
    }

    public float IncreaseHealth(float value)
    {
        health += value;
        return health;
    }

    public float DecreaseHealth(float value)
    {
        health -= value;
        if (health <= 0)
        {
            OnDeath?.Invoke();
        }
        return health;
    }

    public void EnableTaunt(bool state)
    {
        isTaunted = state;
    }
}
