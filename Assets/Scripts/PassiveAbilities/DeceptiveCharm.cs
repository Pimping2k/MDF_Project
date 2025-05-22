using CardScripts;
using Components;
using Interfaces;
using UnityEngine;
using Containers;
using CoreMechanic;

namespace PassiveAbilities
{
    public class DeceptiveCharm : MonoBehaviour, IPassivable
    {
        public GameObject cardPrefab;
        
        public void ApplyPassiveEffect()
        {
            CheckForwardSlot();
        }
        
        public void PowerUpPassiveEffect(int value)
        {
            throw new System.NotImplementedException();
        }

        public void PowerUpPassiveEffect(float value)
        {
            throw new System.NotImplementedException();
        }

        private void CheckForwardSlot()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, transform.up, out var hit,
                    Quaternion.identity, maxDistance: 10f))
            {
                if (hit.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
                {
                    var slotComponent = hit.collider.GetComponent<Slot>();

                    if (!slotComponent.IsOccupied)
                    {
                        var cardModelIstance = Instantiate(cardPrefab, hit.transform);
                        slotComponent.AssignCard(cardModelIstance);

                        var cardItemModelComponent = cardModelIstance.GetComponent<CardItemModel>();
                        var cardModelGrabComponent = cardModelIstance.GetComponent<CardModelGrabComponent>();

                        int slotID = slotComponent.ID;
                        cardItemModelComponent.currentSlotId = slotID;
                        cardModelGrabComponent.CurrentSlot = slotComponent;

                        TableCardManager.Instance.playerCardsInstance.Add(cardModelIstance);
                        var passiveCardComponent = GetComponent<PassiveCardComponent>();
                        passiveCardComponent.summonedEntities.Add(cardModelIstance);
                    }
                }
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Deceptive charm passive")]
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(0.1f, 1f, 3f));
        }
#endif
    }
}
