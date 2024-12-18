using System;
using System.Collections;
using System.Collections.Generic;
using Containers;
using DefaultNamespace;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace CardScripts
{
    public class CardItemModel : MonoBehaviour, IHittable, IStepable
    {
        [SerializeField] private CardItemView cardView;
        [SerializeField] private HealthComponent HealthComponent;
        [SerializeField] private DamageComponent DamageComponent;
        public int currentSlotId = -1;

        private Coroutine stepCoroutine;
        private bool isMoving = false;
        private Vector3 originalPosition;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            
        }

        public void Hit()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, transform.up, out var hitInfo,
                    Quaternion.identity, maxDistance: 10f))
            {
                if (hitInfo.collider.CompareTag(TagsContainer.ENEMYCARD))
                {
                    var enemyHealth = hitInfo.collider.GetComponent<HealthComponent>();

                    originalPosition = this.transform.localPosition;

                    this.transform.DOMove(hitInfo.collider.transform.position, 1f).SetEase(Ease.OutCubic).OnComplete((
                        () =>
                        {
                            enemyHealth.DecreaseHealth(DamageComponent.Damage);
                            this.transform.DOMove(originalPosition, 1f).SetEase(Ease.InCubic).SetDelay(0.2f);
                        }));
                }
            }
        }

        public IEnumerator Step()
        {
            if (Physics.BoxCast(transform.position, transform.localScale * 0.1f, this.transform.up,
                    out var hitInfo, Quaternion.identity, maxDistance: 10f))
            {
                Debug.DrawRay(transform.position, transform.up, Color.red, 10f);
                if (hitInfo.collider.CompareTag(TagsContainer.PLAYERCARDSLOT))
                {
                    var interactionSlot = hitInfo.collider.GetComponent<Slot>();
                    var interactionTransform = hitInfo.collider.transform;

                    if (interactionSlot.IsOccupied)
                        yield break;

                    var currentSlot = this.GetComponentInParent<Slot>();
                    if (currentSlot != null)
                    {
                        currentSlot.ClearCard();
                    }

                    yield return transform.DOMove(interactionTransform.parent.position, 0.15f).OnComplete(() =>
                    {
                        currentSlotId = interactionSlot.ID;
                        transform.parent = interactionTransform;
                        transform.localPosition = Vector3.zero;
                        interactionSlot.AssignCard(this.gameObject);
                    });
                }
                else
                {
                    Hit();
                }
            }
        }
    }
}