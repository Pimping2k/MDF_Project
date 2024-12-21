using System;
using System.Collections.Generic;
using CoreMechanic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class EnemySpawnCardsBehaviour : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemyCards;
        [SerializeField] private TableCardBehaviour tableCardBehaviour;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnEnemyCards();
            }
        }

        private void SpawnEnemyCards()
        {
            foreach (var card in enemyCards)
            {
                GameObject chosenSlot = null;
                Slot slotComponent = null;
                bool isSlotFound = false;

                while (!isSlotFound)
                {
                    chosenSlot = ChooseAvailableSlot();
                    slotComponent = chosenSlot.GetComponent<Slot>();

                    if (!slotComponent.IsOccupied)
                        isSlotFound = true;
                }
                
                var enemyCardInstance = Instantiate(card, chosenSlot.transform);
                TableCardManager.Instance.enemyCardsInstance.Add(enemyCardInstance);
                slotComponent.AssignCard(enemyCardInstance);
            }
        }

        private GameObject ChooseAvailableSlot()
        {
            GameObject chosenSlot;
            return chosenSlot = tableCardBehaviour.rows[Random.Range(0, 3)][Random.Range(0, 3)];
        }

        public void OnSpawnedEnemyCard()
        {
            //TableCardManager.Instance.enemyCardsInstance.Add(enemyCards);
        }
    }
}