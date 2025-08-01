﻿using System;
using Components;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private CanvasGroup winUi;
        
        private GameObject currentEnemy;
        private HealthComponent healthComponent;
        
        public HealthComponent HealthComponent => healthComponent;
        
        public static EnemyManager Instance;

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

        private void Start()
        {
            healthComponent.OnDeath += OnDeath;
        }

        private async void OnDeath()
        {
            winUi.DOFade(1f, .5f);
            await UniTask.WaitForSeconds(1f);
            SceneManager.LoadScene(0);
        }

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
            healthComponent = currentEnemy.GetComponent<HealthComponent>();
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