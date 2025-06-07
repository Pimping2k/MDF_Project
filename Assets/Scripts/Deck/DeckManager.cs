using System;
using System.Collections.Generic;
using Deck;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance;
    
    [SerializeField] private GameObject[] activePrefabs;
    [SerializeField] private GameObject cardsParent;
    
    private List<GameObject> playerCards = new List<GameObject>();

    public GameObject[] ActivePrefabs => activePrefabs;
    public List<GameObject> PlayerCards
    {
        get => playerCards;
        set => playerCards = value;
    }

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

    public void AddCard(GameObject card)
    {
        var cardInstance = Instantiate(card, cardsParent.transform);
        playerCards.Add(cardInstance);
    }
}