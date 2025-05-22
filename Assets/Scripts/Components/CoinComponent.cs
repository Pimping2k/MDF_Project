using UnityEngine;

public class CoinComponent : MonoBehaviour
{
    public static CoinComponent Instance;
    
    [SerializeField] private int coins;
    
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
    
    public int Coins
    {
        get => coins;
        set => coins = value;
    }

    public void DecreaseCoins(int value)
    {
        Coins -= value;
    }

    public void IncreaseCoins(int value)
    {
        Coins += value;
    }
}
