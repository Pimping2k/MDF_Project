using System;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    private List<GameObject> playerCards = new List<GameObject>();
    
    private void Awake()
        {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ClaimCard(GameObject card)
    {
        playerCards.Add(card);
    }

    public void RemoveCard(GameObject card)
    {
        
    }
}
