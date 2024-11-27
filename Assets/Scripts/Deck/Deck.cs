using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<GameObject> cards = new List<GameObject>();
        [SerializeField] private RectTransform deckTransformParent;

        public List<GameObject> Cards
        {
            get => cards;
            set => cards = value;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var offsetX = 100f;
            var startPosition = deckTransformParent.position;

            for (int i = 0; i < cards.Count; i++)
            {
                var newPosition = new Vector3(startPosition.x + i * offsetX, startPosition.y, startPosition.z);

                var cardInstance = Instantiate(cards[i], newPosition, Quaternion.identity, deckTransformParent);
                DeckManager.Instance.PlayerCards.Add(cardInstance);
            }
        }
    }
}