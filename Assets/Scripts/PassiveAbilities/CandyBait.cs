using System;
using Containers;
using Interfaces;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace PassiveAbilities
{
    public class CandyBait : MonoBehaviour, IPassivable
    {
        [SerializeField] private Vector3 horizontalBoxSize;
        [SerializeField] private Vector3 verticalBoxSize;

        private DamageComponent damageComponent;

        private void Awake()
        {
            damageComponent = GetComponent<DamageComponent>();
        }

        public void ApplyPassiveEffect()
        {
            CheckForNeighbours();
        }

        public void PowerUpPassiveEffect(int value)
        {
            throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(float value)
        {
            throw new System.NotImplementedException();
        }
        
        private void CheckForNeighbours()
        {
            CheckHorizontally();
            CheckVertically();
        }

        private void CheckHorizontally()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, horizontalBoxSize, Vector3.forward,
                Quaternion.identity, 3f);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag(TagsContainer.PLAYERCARDITEMMODEL))
                {
                    damageComponent.IncreaseDamage(1);
                }
            }
        }

        private void CheckVertically()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, verticalBoxSize, Vector3.left,
                Quaternion.identity, 3f);

            foreach (var hit in hits)
            {
                if (hit.collider.CompareTag(TagsContainer.PLAYERCARDITEMMODEL))
                {
                    damageComponent.IncreaseDamage(1);
                }
            }
        }
#if UNITY_EDITOR
        [ContextMenu("Candy bait passive")]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(transform.position, verticalBoxSize);
            Gizmos.DrawWireCube(transform.position, horizontalBoxSize);
        }
#endif
    }
}