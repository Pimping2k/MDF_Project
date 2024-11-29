using System;
using System.Collections.Generic;
using CardScripts;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using GameObject = CardScripts.GameObject;

namespace CoreMechanic
{
    public class TableCardManager : MonoBehaviour
    {
        public static TableCardManager Instance;

        public List<GameObject> playerCardsInstance;
        public List<GameObject> enemyCardsInstance;

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
    }
}