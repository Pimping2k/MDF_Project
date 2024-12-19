using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandlerComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent HealthComponent;

    private void Start()
    {
        HealthComponent.OnDeath += HealthComponentOnOnDeath;
    }

    private void OnDestroy()
    {
        HealthComponent.OnDeath -= HealthComponentOnOnDeath;
    }

    private void HealthComponentOnOnDeath()
    {
        throw new NotImplementedException();
    }
}
