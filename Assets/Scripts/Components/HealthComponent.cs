using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health;

    public float Health
    {
        get => health;
        set => health = value;
    }

    public float IncreaseHealth(float value)
    {
        health += value;
        return health;
    }

    public float DecreaseHealth(float value)
    {
        health -= value;
        return health;
    }
}
