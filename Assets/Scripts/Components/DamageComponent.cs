using System;
using DefaultNamespace;
using UnityEngine;

public class DamageComponent : MonoBehaviour, IDamageble
{
    [SerializeField] private float damage;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float IncreaseDamage(float value)
    {
        damage += value;
        return damage;
    }

    public float DecreaseDamage(float value)
    {
        damage -= value;
        return damage;
    }
    
    public float DealDamage()
    {
        return damage;
    }
}