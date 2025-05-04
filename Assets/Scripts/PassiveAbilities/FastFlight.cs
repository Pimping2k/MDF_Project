using System;
using Containers;
using Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace PassiveAbilities
{
    public class FastFlight : MonoBehaviour, IPassivable
    {
        private DamageComponent damageComponent;

        private void Awake()
        {
            damageComponent = GetComponent<DamageComponent>();
        }

        public void ApplyPassiveEffect()
        {
            CheckForwardSlots();
        }

        public void PowerUpPassiveEffect(int value)
        {
            throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(float value)
        {
            throw new System.NotImplementedException();
        }

        private void CheckForwardSlots()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(0.1f, 1f, 3f), Vector3.forward,
                Quaternion.identity, maxDistance: 5f);

            foreach (var hit in hits)
            {
                if (hit.collider.TryGetComponent(out Slot hitSlot))
                {
                    if (!hitSlot.IsOccupied)
                    {
                        damageComponent.IncreaseDamage(1);
                    }
                }
            }
        }
        
#if UNITY_EDITOR
        [ContextMenu("Fast flight passive")]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, 1f, 3f));
        }
#endif
    }
}