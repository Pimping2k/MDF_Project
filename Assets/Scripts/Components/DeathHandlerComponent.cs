using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DeathHandlerComponent : MonoBehaviour
{
    [SerializeField] private float DeathDuration;
    [SerializeField] private HealthComponent HealthComponent;

    private SpriteRenderer _renderer;
    private Material _material;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer != null)
        {
            _material = _renderer.material;
        }
    }
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
        StartCoroutine(PerformDeath());
    }

    private IEnumerator PerformDeath()
    {
        float elapsedTime = 0.0f;
        float amount = 0.0f;
        float speed = 1;

        while (elapsedTime < DeathDuration)
        {
            amount = elapsedTime / DeathDuration;
            _material.SetFloat("_DissolveAmount", amount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _material.SetFloat("_DissolveAmount", 1.0f);
        
        Destroy(gameObject);
    }
}