using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public event Action OnDeath;
    
    private void Awake()
    {
        health = maxHealth;
    }

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float MaxHealth
    {
        get => maxHealth;
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
}
