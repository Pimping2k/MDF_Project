using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Deck
{
    public class Deck : MonoBehaviour
    {
        [SerializeField] private List<GameObject> initialCards = new List<GameObject>();
        [SerializeField] private RectTransform deckTransformParent;
        [SerializeField] private float offsetX = 50f;
        public List<GameObject> InitialCards
        {
            get => InitialCards;
            set => InitialCards = value;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var startPosition = deckTransformParent.position;

            for (int i = 0; i < initialCards.Count; i++)
            {
                var newPosition = new Vector3(startPosition.x + i * offsetX, startPosition.y, startPosition.z);

                var cardInstance = Instantiate(initialCards[i], newPosition, Quaternion.identity, deckTransformParent);

                DeckManager.Instance.CardInstanceList.Add(cardInstance);
            }
        }
    }
}