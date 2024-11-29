using System;
using DefaultNamespace;
using UnityEngine;

namespace CardScripts
{
    public class GameObject : MonoBehaviour, IHittable,IStepable
    {
        [SerializeField] private CardItemView cardView;
        private float damage;
        private float health;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            damage = cardView.damageComponent.Damage;
            health = cardView.healthComponent.Health;
        }

        public void Hit()
        {
            throw new System.NotImplementedException();
        }

        public void Step()
        {
            throw new System.NotImplementedException();
        }
    }
}