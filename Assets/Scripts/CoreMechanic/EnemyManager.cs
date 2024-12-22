﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace CoreMechanic
{
    public class EnemyManager : MonoBehaviour
    {
        public static event Action<EnemyCardsContainer> OnEnemyChanged;

        [SerializeField] private GameObject[] enemyPrefabs;
        [SerializeField] private Transform spawnPoint;

        private GameObject currentEnemy;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SpawnEnemy(Random.Range(0, enemyPrefabs.Length)); // Спавним врага, как только сцена загружена
        }
        public void SpawnEnemy(int enemyIndex)
        {
            if (currentEnemy != null)
                Destroy(currentEnemy);

            if (enemyIndex >= 0 && enemyIndex < enemyPrefabs.Length)
            {
                currentEnemy = Instantiate(enemyPrefabs[enemyIndex], spawnPoint);
                var enemyCardsContainer = currentEnemy.GetComponent<EnemyCardsContainer>();
                
                OnEnemyChanged?.Invoke(enemyCardsContainer);
            }
        }
    }
}