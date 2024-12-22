using System;
using CoreMechanic;
using UnityEngine;

namespace GameScripts
{
    public class GameManager : MonoBehaviour
    {
        public GameManager Instance;

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
    }
}