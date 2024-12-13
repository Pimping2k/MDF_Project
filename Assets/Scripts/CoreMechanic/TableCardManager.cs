using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CardScripts;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

namespace CoreMechanic
{
    public class TableCardManager : MonoBehaviour
    {
        public static TableCardManager Instance;

        public List<GameObject> playerCardsInstance;
        public List<GameObject> enemyCardsInstance;

        private Queue<GameObject> cardsQueue = new Queue<GameObject>();
        
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

            ClearTable();
        }

        private void ClearTable()
        {
            playerCardsInstance.ForEach(Destroy);
            enemyCardsInstance.ForEach(Destroy);
            
            playerCardsInstance.Clear();
            enemyCardsInstance.Clear();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(ReorganizeCards());
            }
        }

        private IEnumerator ReorganizeCards()
        {
            var playerCardsInstanceModels = playerCardsInstance.Select(c => c.GetComponent<CardItemModel>()).ToArray();

            var sortedCardsModels = playerCardsInstanceModels.OrderBy(c => c.currentSlotId).ToArray();

            for (int i = 0; i < sortedCardsModels.Length; i++)
            {
                var card = sortedCardsModels[i];

                if (card.currentSlotId == i)
                    continue;

                if (!card.IsMoving)
                {
                    card.IsMoving = true;
                    yield return StartCoroutine(card.Step());
                    card.IsMoving = false;
                }
            }
        }
    }
}