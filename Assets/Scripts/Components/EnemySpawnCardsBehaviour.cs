using System;
using System.Collections;
using System.Collections.Generic;
using CoreMechanic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Components
{
    public class EnemySpawnCardsBehaviour : MonoBehaviour
    {
        [SerializeField] private TableCardBehaviour tableCardBehaviour;
        [SerializeField] [Range(1, 10)] private float spawnDuration;
        [SerializeField] [Range(0.1f, 2f)] private float spawnDelay = 0.2f;

        private EnemyCardsContainer enemyCardsContainer;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(SpawnEnemyCards());
            }
        }

        private void OnEnable()
        {
            EnemyManager.OnEnemyChanged += UpdateEnemyCards;
        }

        private void OnDisable()
        {
            EnemyManager.OnEnemyChanged -= UpdateEnemyCards;
        }

        private void UpdateEnemyCards(EnemyCardsContainer newEnemyCardsContainer)
        {
            enemyCardsContainer = newEnemyCardsContainer;
        }

        private IEnumerator SpawnEnemyCards()
        {
            foreach (var card in enemyCardsContainer.cards)
            {
                yield return new WaitForSeconds(spawnDelay);

                GameObject chosenSlot = FindAvailableSlot();
                var slotComponent = chosenSlot.GetComponent<Slot>();

                var enemyCardInstance = Instantiate(card, chosenSlot.transform);
                AnimateCardSpawn(enemyCardInstance);
                TableCardManager.Instance.enemyCardsInstance.Add(enemyCardInstance);
                slotComponent.AssignCard(enemyCardInstance);
            }
        }

        private GameObject FindAvailableSlot()
        {
            GameObject chosenSlot = null;
            Slot slotComponent = null;

            while (true)
            {
                chosenSlot = ChooseAvailableSlot();
                slotComponent = chosenSlot.GetComponent<Slot>();

                if (!slotComponent.IsOccupied)
                    break;
            }

            return chosenSlot;
        }

        private GameObject ChooseAvailableSlot()
        {
            GameObject chosenSlot;
            return chosenSlot = tableCardBehaviour.rows[Random.Range(0, 3)][Random.Range(0, 3)];
        }

        private void AnimateCardSpawn(GameObject card)
        {
            var spriteRenderer = card.GetComponent<SpriteRenderer>();
            var material = spriteRenderer.material;
            StartCoroutine(DissolveEffect(material));
        }

        private IEnumerator DissolveEffect(Material material)
        {
            float elapsedTime = 0.0f;
            float amount = 1.0f;
            float speed = 1;

            while (elapsedTime < spawnDuration)
            {
                amount = 1.0f - (elapsedTime / spawnDuration);
                material.SetFloat("_DissolveAmount", amount);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            material.SetFloat("_DissolveAmount", 0.0f);
        }


        public void OnSpawnedEnemyCard()
        {
            //TableCardManager.Instance.enemyCardsInstance.Add(enemyCards);
        }
    }
}