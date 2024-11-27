using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CandleBehaviour : HealthComponent
{
    private ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emissionModule;
    private ParticleSystem.MinMaxCurve minMaxCurve;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        emissionModule = particleSystem.emission;
    }

    void Update()
    {
        minMaxCurve = new ParticleSystem.MinMaxCurve(200f * Health / MaxHealth, 300f * Health / MaxHealth);
        emissionModule.rateOverTime = minMaxCurve;
    }
}