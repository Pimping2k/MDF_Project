using Components;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CandleBehaviour : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
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
        //minMaxCurve = new ParticleSystem.MinMaxCurve(200f * healthComponent.CurrentValue / healthComponent.MaxValue, 300f * healthComponent.CurrentValue / healthComponent.MaxValue);
        //emissionModule.rateOverTime = minMaxCurve;
    }
}