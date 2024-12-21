using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cards = new List<GameObject>();
        [SerializeField] private GameObject cardsParent;

        public List<GameObject> Cards
        {
            get => cards;
            set => cards = value;
        }

        private void Start()
        {
            InitializePlayerDeck();
        }

        private void InitializePlayerDeck()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                var cardInstance = Instantiate(cards[i], cardsParent.transform);
                DeckManager.Instance.PlayerCards.Add(cardInstance);
            }
        }
    }
}